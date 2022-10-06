using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace genshinwebsite.Models
{
    public class EmailVCodeModel:DbContext
    {
        [Key]
        public string Email { get; set; }
        public string VCode { get; set; }
        
        
        public DateTime SendDateTime { get; set; } = DateTime.Now;
        public int RequestTime { get; set; }
    }
}
