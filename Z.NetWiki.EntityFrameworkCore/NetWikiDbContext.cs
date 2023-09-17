using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFrameworkCore;
using Z.NetWiki.EntityFrameworkCore.ConfigureExtensions;

namespace Z.NetWiki.EntityFrameworkCore
{
    public class NetWikiDbContext : ZDbContext<NetWikiDbContext>
    {
        public NetWikiDbContext(DbContextOptions<NetWikiDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigureModel();
        }
    }
}
