using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminBlazor.Migrations
{
    public partial class addindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SysUser_UserName",
                table: "SysUser",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SysRolePermissions_PermissionName",
                table: "SysRolePermissions",
                column: "PermissionName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SysRole_RoleName",
                table: "SysRole",
                column: "RoleName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SysUser_UserName",
                table: "SysUser");

            migrationBuilder.DropIndex(
                name: "IX_SysRolePermissions_PermissionName",
                table: "SysRolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_SysRole_RoleName",
                table: "SysRole");
        }
    }
}
