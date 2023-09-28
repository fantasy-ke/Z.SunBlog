using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.EntityFrameworkCore.Options
{
    public abstract class ZDbContextOptions : DbContextOptions
    {
        public readonly IServiceProvider? ServiceProvider;

        /// <summary>
        /// 是否启用软删过滤器
        /// </summary>
        public bool EnableSoftDelete { get; set; } = true;


        internal Type DbContextType { get; }

        public override Type ContextType => OriginOptions.ContextType;

        protected readonly DbContextOptions OriginOptions;

        private protected ZDbContextOptions(
            IServiceProvider? serviceProvider,
            bool enableSoftDelete,
            Type dbContextType,
            DbContextOptions originOptions)
        {
            ServiceProvider = serviceProvider;
            EnableSoftDelete = enableSoftDelete;
            DbContextType = dbContextType;
            OriginOptions = originOptions;
        }

        public override DbContextOptions WithExtension<TExtension>(TExtension extension) => OriginOptions.WithExtension(extension);
    }


    public class ZDbContextOptions<TDbContext> : ZDbContextOptions
    where TDbContext : DbContext
    {
        public ZDbContextOptions() : base(null, false, typeof(TDbContext), new DbContextOptions<TDbContext>())
        {
        }

        public ZDbContextOptions(
            IServiceProvider? serviceProvider,
            DbContextOptions originOptions,
            bool enableSoftDelete) : base(serviceProvider, enableSoftDelete, typeof(TDbContext), originOptions)
        {
        }

    }

}
