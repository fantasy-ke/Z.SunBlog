using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Domain.Entities.IAuditing;

namespace Z.EntityFrameworkCore
{
    public abstract class ZDbContext<TDbContext> : DbContext 
        where TDbContext : DbContext
    {

        protected ZDbContext(DbContextOptions<TDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureSoftDelete(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 禁用查询跟踪
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
#if DEBUG
            // 显示更详细的异常日志
            optionsBuilder.EnableDetailedErrors();
#endif

        }


        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
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
