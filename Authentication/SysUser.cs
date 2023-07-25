using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AdminBlazor.Authentication
{
    [Index(nameof(UserName), IsUnique = true)]
    [Index(nameof(Telephone))]
    public class SysUser
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string UserName { get; set; }

        [StringLength(100)]
        [Required]
        public string Password { get; set; }

        public SysRole Role { get; set; }

        public int RoleId { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [StringLength(50)]
        public string RealName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [StringLength(11)]
        public string Telephone { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [StringLength(50)]
        public string Avatar { get; set; }
    }
}
