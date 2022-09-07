using System.ComponentModel.DataAnnotations;

namespace genshinwebsite.ViewModels
{
    public class UserLoginViewModel
    {

        [Required(ErrorMessage = "账号不可为空")]
        [Display(Name = "邮箱账号")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(70, ErrorMessage = "账号过长")]
        public string Account { get; set; }

        [Required(ErrorMessage ="密码不可为空")]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        [MaxLength(30, ErrorMessage = "密码至过长，不应超出30位")]
        public string Password { get; set; }

    }
}
