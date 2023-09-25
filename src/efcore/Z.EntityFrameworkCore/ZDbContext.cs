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
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.DependencyInjection;
using Z.Ddd.Common.Entities;
using Z.Ddd.Common.Entities.Auditing;
using Z.Ddd.Common.Entities.IAuditing;
using Z.Ddd.Common.Entities.Users;
using Z.Ddd.Common.Extensions;
using Z.Ddd.Common.Helper;
using Z.Module.DependencyInjection;

namespace Z.EntityFrameworkCore
{
    public abstract class ZDbContext<TDbContext> : DbContext, ITransientDependency
        where TDbContext : DbContext 
    {
        //private IServiceScope? _serviceScope;
        //public IZLazyServiceProvider _lazyServiceProvider;
        // public IAuditPropertySetter AuditPropertySetter => _lazyServiceProvider.LazyGetRequiredService<IAuditPropertySetter>();

        public virtual DbSet<ZUserInfo> ZUsers { get; set; }

        protected ZDbContext(DbContextOptions<TDbContext> options)
        : base(options)
        {
            //_serviceScope = ServiceProviderCache.Instance.GetOrAdd(options, providerRequired: true)
            //        .GetRequiredService<IServiceScopeFactory>()
            //     .CreateScope();

            //_lazyServiceProvider = _serviceScope.ServiceProvider.GetRequiredService<IZLazyServiceProvider>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ConfigureSoftDelete(modelBuilder);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ZUserInfo>(x =>
            {
                x.ToTable("ZUsers");

            });


        }


        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            // 禁用查询跟踪
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
#if DEBUG
            // 显示更详细的异常日志
            optionsBuilder.EnableDetailedErrors();
#endif

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
}
