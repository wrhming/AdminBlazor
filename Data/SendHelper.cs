using AdminBlazor.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AdminBlazor.Data
{
    public static class SendHelper
    {
        public static void Send(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SysRole>().HasData(new SysRole
            {
                Id = 1,
                RoleName = "Admin",
                DisplayName = "管理员",
            }, new SysRole
            {
                Id = 2,
                RoleName = "User",
                DisplayName = "用户",
            });


            modelBuilder.Entity<SysRolePermissions>().HasData(new SysRolePermissions
            {
                Id = 1,
                PermissionName = "Role",
                RoleId = 2,
            }, new SysRolePermissions
            {
                Id = 2,
                PermissionName = "User",
                RoleId = 2,
            });


            modelBuilder.Entity<SysUser>().HasData(new SysUser
            {
                Id = 1,
                UserName = "admin",
                Password = "admin",
                RoleId = 1
            }, new SysUser
            {
                Id = 2,
                UserName = "user",
                Password = "user",
                RoleId = 2
            });
        }
    }
}
