using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace genshinwebsite.Models
{
    public class CommentModel:DbContext
    {
        [Key]
        public int Uid { get; set; }
        [Key]
        public int Muid { get; set; }
        public DateTime Datetime { get; set; } = DateTime.Now;
        public string Content { get; set; }
    }
}
