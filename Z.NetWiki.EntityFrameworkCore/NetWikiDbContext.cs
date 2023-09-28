using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFrameworkCore;
using Z.EntityFrameworkCore.Attributes;
using Z.EntityFrameworkCore.Options;
using Z.Module.DependencyInjection;
using Z.NetWiki.EntityFrameworkCore.ConfigureExtensions;

namespace Z.NetWiki.EntityFrameworkCore
{
    [ConnectionStringName("App:ConnectionString:Default")]
    public class NetWikiDbContext : ZDbContext<NetWikiDbContext>
    {
        public NetWikiDbContext(ZDbContextOptions<NetWikiDbContext> options) : base(options)
        {
            //options.EnableSoftDelete = true;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigureModel();
        }
    }
}
