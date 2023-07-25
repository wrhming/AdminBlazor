using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminBlazor.Authentication
{
    [Index(nameof(RoleName), IsUnique = true)]
    public class SysRole
    {
        public int Id { get; set; }

        /// <summary>
        /// 显示名字
        /// </summary>
        [StringLength(50)]
        [Required]
        public string DisplayName { get; set; }

        [StringLength(50)]
        [Required]
        public string RoleName { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public List<SysRolePermissions> Permissions { get; set; }
    }
}
