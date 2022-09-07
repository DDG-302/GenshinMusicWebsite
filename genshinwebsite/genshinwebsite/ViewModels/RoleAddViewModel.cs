using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace genshinwebsite.ViewModels
{
    public class RoleAddViewModel
    {
        [Required]
        [Display(Name = "角色名称")]
        public string RoleName { get; set; }
    }
}
