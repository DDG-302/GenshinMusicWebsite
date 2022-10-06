using System.ComponentModel.DataAnnotations;

namespace genshinwebsite.ViewModels
{
    public class UserRegisterViewModel:UserLoginViewModel
    {


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
