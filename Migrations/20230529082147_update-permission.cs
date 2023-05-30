using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminBlazor.Migrations
{
    public partial class updatepermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Permissions",
                table: "SysRolePermissions",
                newName: "PermissionName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PermissionName",
                table: "SysRolePermissions",
                newName: "Permissions");
        }
    }
}
