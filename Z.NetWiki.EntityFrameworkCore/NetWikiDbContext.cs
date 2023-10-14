using Microsoft.EntityFrameworkCore;
using Z.EntityFrameworkCore;
using Z.EntityFrameworkCore.Attributes;
using Z.EntityFrameworkCore.Options;
using Z.NetWiki.Core.AlbumsModule;
using Z.NetWiki.Core.ArticleCategoryModule;
using Z.NetWiki.Core.ArticleModule;
using Z.NetWiki.Core.ArticleTagModule;
using Z.NetWiki.Core.AuthAccountModule;
using Z.NetWiki.Core.CategoriesModule;
using Z.NetWiki.Core.CommentsModule;
using Z.NetWiki.Core.PicturesModule;
using Z.NetWiki.Core.PraiseModule;
using Z.NetWiki.Core.TagModule;
using Z.NetWiki.Core.TalksModule;
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
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Pictures> Pictures { get; set; }
        public virtual DbSet<Praise> Praise { get; set; }
        public virtual DbSet<Albums> Albums { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Talks> Talks { get; set; }
        public virtual DbSet<AuthAccount> AuthAccount { get; set; }

    }
}
