using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminBlazor.Migrations
{
    public partial class updaterole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SysRolePermissions_SysRole_SysRoleId",
                table: "SysRolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_SysUser_SysRole_RoleId",
                table: "SysUser");

            migrationBuilder.DropIndex(
                name: "IX_SysRolePermissions_SysRoleId",
                table: "SysRolePermissions");

            migrationBuilder.DropColumn(
                name: "SysRoleId",
                table: "SysRolePermissions");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "SysUser",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "SysRole",
                columns: new[] { "Id", "DisplayName", "RoleName" },
                values: new object[] { 1, "管理员", "Admin" });

            migrationBuilder.InsertData(
                table: "SysRole",
                columns: new[] { "Id", "DisplayName", "RoleName" },
                values: new object[] { 2, "用户", "User" });

            migrationBuilder.InsertData(
                table: "SysRolePermissions",
                columns: new[] { "Id", "Permissions", "RoleId" },
                values: new object[,]
                {
                    { 1, "Role", 2 },
                    { 2, "User", 2 }
                });

            migrationBuilder.InsertData(
                table: "SysUser",
                columns: new[] { "Id", "Password", "RoleId", "UserName" },
                values: new object[,]
                {
                    { 1, "admin", 1, "admin" },
                    { 2, "user", 2, "user" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SysRolePermissions_RoleId",
                table: "SysRolePermissions",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_SysRolePermissions_SysRole_RoleId",
                table: "SysRolePermissions",
                column: "RoleId",
                principalTable: "SysRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SysUser_SysRole_RoleId",
                table: "SysUser",
                column: "RoleId",
                principalTable: "SysRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SysRolePermissions_SysRole_RoleId",
                table: "SysRolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_SysUser_SysRole_RoleId",
                table: "SysUser");

            migrationBuilder.DropIndex(
                name: "IX_SysRolePermissions_RoleId",
                table: "SysRolePermissions");

            migrationBuilder.DeleteData(
                table: "SysRolePermissions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SysRolePermissions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SysUser",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SysUser",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SysRole",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SysRole",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "SysUser",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SysRoleId",
                table: "SysRolePermissions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SysRolePermissions_SysRoleId",
                table: "SysRolePermissions",
                column: "SysRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_SysRolePermissions_SysRole_SysRoleId",
                table: "SysRolePermissions",
                column: "SysRoleId",
                principalTable: "SysRole",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SysUser_SysRole_RoleId",
                table: "SysUser",
                column: "RoleId",
                principalTable: "SysRole",
                principalColumn: "Id");
        }
    }
}
