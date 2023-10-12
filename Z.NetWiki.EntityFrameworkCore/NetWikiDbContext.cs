using Microsoft.EntityFrameworkCore;
using Z.EntityFrameworkCore;
using Z.EntityFrameworkCore.Attributes;
using Z.EntityFrameworkCore.Options;
using Z.NetWiki.Domain.ArticleCategoryModule;
using Z.NetWiki.Domain.ArticleModule;
using Z.NetWiki.Domain.ArticleTagModule;
using Z.NetWiki.Domain.CategoriesModule;
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

        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<ArticleTag> ArticleTag { get; set; }
        public virtual DbSet<ArticleCategory> ArticleCategory { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }

    }
}
