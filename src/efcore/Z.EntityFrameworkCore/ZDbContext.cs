using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.DependencyInjection;
using Z.Ddd.Common.Entities;
using Z.Ddd.Common.Entities.Auditing;
using Z.Ddd.Common.Entities.IAuditing;
using Z.Ddd.Common.Entities.Users;
using Z.Ddd.Common.Extensions;
using Z.Ddd.Common.Helper;
using Z.EntityFrameworkCore.Extensions;
using Z.EntityFrameworkCore.Options;
using Z.Module.DependencyInjection;

namespace Z.EntityFrameworkCore
{
    public abstract class ZDbContext : DbContext, IDisposable
    {
        protected ZDbContextOptions? Options { get; private set; }
        public virtual DbSet<ZUserInfo> ZUsers { get; set; }

        protected ZDbContext(ZDbContextOptions options)
        : base(options)
        {
            Options = options;
        }

        protected  override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ConfigureSoftDelete(modelBuilder);
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddZCoreConfigure();
            // 可选
            modelBuilder.UseCollation("Chinese_PRC_CI_AS");

            OnModelCreatingConfigureGlobalFilters(modelBuilder);
        }

        protected sealed override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Options == null)
                return;

            var masaDbContextOptionsBuilder = new ZDbContextOptionsBuilder(optionsBuilder, Options);
            base.OnConfiguring(optionsBuilder);
#if DEBUG
            // 显示更详细的异常日志
            optionsBuilder.EnableDetailedErrors();
#endif
        }


        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }



        protected virtual bool IsSoftDeleteFilterEnabled
        => Options is { EnableSoftDelete: true };



        protected virtual void OnModelCreatingConfigureGlobalFilters(ModelBuilder modelBuilder)
        {
            var methodInfo = GetType().GetMethod(nameof(ConfigureGlobalFilters), BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                methodInfo!.MakeGenericMethod(entityType.ClrType).Invoke(this, new object?[]
                {
                modelBuilder, entityType
                });
            }
        }

        protected virtual void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType mutableEntityType)
        where TEntity : class
        {
            if (mutableEntityType.BaseType == null)
            {
                var filterExpression = CreateFilterExpression<TEntity>();
                if (filterExpression != null)
                    modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
            }
        }

        protected virtual Expression<Func<TEntity, bool>>? CreateFilterExpression<TEntity>()
        where TEntity : class
        {
            Expression<Func<TEntity, bool>>? expression = null;

            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                expression = entity => !IsSoftDeleteFilterEnabled || !EF.Property<bool>(entity, nameof(ISoftDelete.IsDeleted));
            }
            return expression;
        }


        /// <summary>
        /// 过滤器增加软删除过滤
        /// </summary>
        /// <param name="builder"></param>
        private void ConfigureSoftDelete(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                //判断是否继承了软删除类
                if (!typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType)) continue;

                const string isDeleted = nameof(ISoftDelete.IsDeleted);
                builder.Entity(entityType.ClrType).Property<bool>(isDeleted);
                var parameter = Expression.Parameter(entityType.ClrType, isDeleted);

                // 添加过滤器
                var body = Expression.Equal(
                    Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(bool) }, parameter,
                        Expression.Constant(isDeleted)),
                    Expression.Constant(false));

                builder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(body, parameter));
            }
        }

    }


    public abstract class ZDbContext<TDbContext> : ZDbContext
    where TDbContext : ZDbContext<TDbContext>
    {
        protected ZDbContext() : base(new ZDbContextOptions<ZDbContext>())
        {
        }

        protected ZDbContext(ZDbContextOptions<TDbContext> options) : base(options)
        {
        }
    }
}
