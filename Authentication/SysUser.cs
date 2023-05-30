using System.ComponentModel.DataAnnotations;

namespace AdminBlazor.Authentication
{
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
    }
}
