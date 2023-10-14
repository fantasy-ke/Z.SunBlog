using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Z.SunBlog.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    PassWord = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    CreatorId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZUsers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZUsers");
        }
    }
}
