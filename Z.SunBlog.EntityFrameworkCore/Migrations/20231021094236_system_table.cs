using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Z.SunBlog.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class system_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "ZUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "ZUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ZUsers",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastLoginAddress",
                table: "ZUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastLoginIp",
                table: "ZUsers",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "ZUsers",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrgId",
                table: "ZUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    RouteName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Path = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Component = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Redirect = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    IsIframe = table.Column<bool>(type: "bit", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    IsKeepAlive = table.Column<bool>(type: "bit", nullable: false),
                    IsFixed = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZOperationLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogLevel = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ControllerName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ActionName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    HttpMethod = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    TraceId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ThreadId = table.Column<int>(type: "int", nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    RequestUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    RemoteIp = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    OsDescription = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Elapsed = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    HttpStatusCode = table.Column<int>(type: "int", nullable: false),
                    Parameter = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    Response = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    Exception = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    CreatorId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZOperationLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZOrganizations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatorId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZOrganizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatorId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZSigninLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RemoteIp = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    OsDescription = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Message = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatorId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZSigninLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZUserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZUserRoles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "ZOperationLog");

            migrationBuilder.DropTable(
                name: "ZOrganizations");

            migrationBuilder.DropTable(
                name: "ZRoles");

            migrationBuilder.DropTable(
                name: "ZSigninLog");

            migrationBuilder.DropTable(
                name: "ZUserRoles");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "ZUsers");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "ZUsers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "ZUsers");

            migrationBuilder.DropColumn(
                name: "LastLoginAddress",
                table: "ZUsers");

            migrationBuilder.DropColumn(
                name: "LastLoginIp",
                table: "ZUsers");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "ZUsers");

            migrationBuilder.DropColumn(
                name: "OrgId",
                table: "ZUsers");
        }
    }
}
