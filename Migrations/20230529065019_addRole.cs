using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminBlazor.Migrations
{
    public partial class addRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SysRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysRolePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Permissions = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SysRoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysRolePermissions_SysRole_SysRoleId",
                        column: x => x.SysRoleId,
                        principalTable: "SysRole",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SysUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysUser_SysRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SysRole",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SysRolePermissions_SysRoleId",
                table: "SysRolePermissions",
                column: "SysRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SysUser_RoleId",
                table: "SysUser",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SysRolePermissions");

            migrationBuilder.DropTable(
                name: "SysUser");

            migrationBuilder.DropTable(
                name: "SysRole");
        }
    }
}
