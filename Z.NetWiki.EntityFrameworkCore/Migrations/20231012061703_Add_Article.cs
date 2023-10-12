using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Z.NetWiki.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class Add_Article : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Cover = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsTop = table.Column<bool>(type: "bit", nullable: false),
                    Views = table.Column<int>(type: "int", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreationType = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    IsHtml = table.Column<bool>(type: "bit", nullable: false),
                    PublishTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    IsAllowComments = table.Column<bool>(type: "bit", nullable: false),
                    ExpiredTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteMark = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleTag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Cover = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedUserId = table.Column<long>(type: "bigint", nullable: false),
                    DeleteMark = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "ArticleCategory");

            migrationBuilder.DropTable(
                name: "ArticleTag");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
