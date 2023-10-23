using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Z.SunBlog.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class updateuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "ZUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ParentId",
                table: "ZOrganizations",
                type: "nvarchar(36)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ZOrganizations_ParentId",
                table: "ZOrganizations",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ZOrganizations_ZOrganizations_ParentId",
                table: "ZOrganizations",
                column: "ParentId",
                principalTable: "ZOrganizations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZOrganizations_ZOrganizations_ParentId",
                table: "ZOrganizations");

            migrationBuilder.DropIndex(
                name: "IX_ZOrganizations_ParentId",
                table: "ZOrganizations");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "ZUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ParentId",
                table: "ZOrganizations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldNullable: true);
        }
    }
}
