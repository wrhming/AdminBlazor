using System.ComponentModel.DataAnnotations;

namespace AdminBlazor.Dto
{
    public class LoginDto
    {
        [Required(ErrorMessage ="用户名不能为空")] public string UserName { get; set; }

        [Required(ErrorMessage = "密码不能为空")] public string Password { get; set; }

        public string Mobile { get; set; }

        public string Captcha { get; set; }

        public string LoginType { get; set; }

        public bool AutoLogin { get; set; }
    }
}
