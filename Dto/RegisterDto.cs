using System.ComponentModel.DataAnnotations;

namespace AdminBlazor.Dto
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "用户名不能为空")] 
        public string UserName { get; set; }

        [Required(ErrorMessage = "密码不能为空")] 
        public string Password { get; set; }

        [Compare(nameof(Password),ErrorMessage ="两次输入的密码不一致")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="手机号不能为空")]
        [RegularExpression("^1[3456789]\\d{9}$", ErrorMessage = "请输入正确的手机号")]
        public string Phone { get; set; }

        [Required(ErrorMessage ="手机验证码不能为空")]
        [RegularExpression("^\\d{6}$",ErrorMessage ="请输入6位数字")]
        public string Captcha { get; set; }
    }
}
