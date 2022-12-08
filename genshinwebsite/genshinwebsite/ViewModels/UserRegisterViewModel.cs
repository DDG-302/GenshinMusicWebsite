using System.ComponentModel.DataAnnotations;

namespace genshinwebsite.ViewModels
{
    public class UserRegisterViewModel
    {

        [Required(ErrorMessage = "账号不可为空")]
        [Display(Name = "邮箱账号")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(70, ErrorMessage = "账号过长")]
        public string Account { get; set; }

        [Required(ErrorMessage = "密码不可为空")]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        [MaxLength(30, ErrorMessage = "密码至过长，不应超出30位")]
        [MinLength(6, ErrorMessage = "密码过短，至少为6位")]

        public string Password { get; set; }
        [Display(Name = "确认密码")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "两次输入密码不相同")]
        public string ConfirmPassoword { get; set; }

        [Display(Name = "昵称")]
        [Required(ErrorMessage = "昵称不可为空")]
        [MinLength(2, ErrorMessage = "昵称至少需要两个字符")]
        [MaxLength(20, ErrorMessage = "昵称过长，不应超出20个字符")]
        [RegularExpression(@"([\u4e00-\u9fa5]|[a-z]|[A-Z]|[0-9]|_|-|\||\+|=)+", ErrorMessage = "昵称仅能由汉字、英文、数字和其他字符（-,+,|,=,_）组成")]
        public string Name { get; set; }

        [Display(Name = "邮箱验证码")]
        public string VCode { get; set; }


    }
}
