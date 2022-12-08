using System.ComponentModel.DataAnnotations;

namespace genshinwebsite.ViewModels
{
    public class ChangeUserInfoViewModel
    {
        [Display(Name = "UID")]
        public int Uid { get; set; }

        [Display(Name = "邮箱账号")]
        public string Account { get; set; }

        [Display(Name = "昵称")]
        public string Name { get; set; }

        [Required(ErrorMessage = "密码不可为空")]
        [Display(Name = "修改密码")]
        [DataType(DataType.Password)]
        [MaxLength(30, ErrorMessage = "密码至过长，不应超出30位")]
        [MinLength(6, ErrorMessage = "密码过短，至少为6位")]
        public string Password { get; set; }

        [Display(Name = "确认密码")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "两次输入密码不相同")]
        public string ConfirmPassoword { get; set; }

        [Display(Name = "邮箱验证码")]
        public string VCode { get; set; } = string.Empty;
    }
}
