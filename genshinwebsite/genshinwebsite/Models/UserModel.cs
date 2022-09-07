using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;



namespace genshinwebsite.Models
{
    public class UserModel : IdentityUser<int>
    {

        override public int Id { get; set; }
        
        //[Display(Name = "昵称")]
        //public string Name { get; set; }

        //[Display(Name = "邮箱账号")]
        //[DataType(DataType.EmailAddress)]
        
        //public string Account { get; set; }

        //[Display(Name = "密码")]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        //public UserType User_type { get; set; } = UserType.NORMAL;

        //public enum UserType { NORMAL, ADMIN };



    }

}
