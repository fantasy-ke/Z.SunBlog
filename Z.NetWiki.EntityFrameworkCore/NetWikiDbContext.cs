using Microsoft.EntityFrameworkCore;
using Z.EntityFrameworkCore;
using Z.EntityFrameworkCore.Attributes;
using Z.EntityFrameworkCore.Options;
using Z.NetWiki.Domain.AlbumsModule;
using Z.NetWiki.Domain.ArticleCategoryModule;
using Z.NetWiki.Domain.ArticleModule;
using Z.NetWiki.Domain.ArticleTagModule;
using Z.NetWiki.Domain.AuthAccountModule;
using Z.NetWiki.Domain.CategoriesModule;
using Z.NetWiki.Domain.CommentsModule;
using Z.NetWiki.Domain.PicturesModule;
using Z.NetWiki.Domain.PraiseModule;
using Z.NetWiki.Domain.TagModule;
using Z.NetWiki.Domain.TalksModule;
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
