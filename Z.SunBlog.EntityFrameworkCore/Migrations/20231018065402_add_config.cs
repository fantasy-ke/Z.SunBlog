using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Z.SunBlog.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class add_config : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomConfig",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    IsMultiple = table.Column<bool>(type: "bit", nullable: false),
                    Json = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    AllowCreationEntity = table.Column<bool>(type: "bit", nullable: false),
                    IsGenerate = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatorId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomConfigItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    ConfigId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Json = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomConfigItem", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomConfig");

            migrationBuilder.DropTable(
                name: "CustomConfigItem");
        }
    }
}
