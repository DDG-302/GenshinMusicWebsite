﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace genshinwebsite.Models
{
    public class CommentModel
    {
        
        public int Uid { get; set; }
        public int Muid { get; set; }
        
        public string CommentContent { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public string UploadIP { get; set; }


    }

    
}
