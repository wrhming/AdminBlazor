﻿using System.ComponentModel.DataAnnotations;

namespace AdminBlazor.Authentication
{
    /// <summary>
    /// 角色权限表
    /// </summary>
    public class SysRolePermissions
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public SysRole Role { get; set; }

        [Required]
        [StringLength(255)]
        public string PermissionName { get; set; }
    }
}
