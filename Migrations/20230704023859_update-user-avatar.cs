using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminBlazor.Migrations
{
    public partial class updateuseravatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "SysUser",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RealName",
                table: "SysUser",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "SysUser",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SysUser_Telephone",
                table: "SysUser",
                column: "Telephone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SysUser_Telephone",
                table: "SysUser");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "SysUser");

            migrationBuilder.DropColumn(
                name: "RealName",
                table: "SysUser");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "SysUser");
        }
    }
}
