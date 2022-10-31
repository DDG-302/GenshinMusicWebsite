using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace genshinwebsite.ViewModels
{
    public class CommentViewModel
    {
        public int Uid { get; set; }
        public string UserName { get; set; }
        public int Muid { get; set; }

        public string CommentContent { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public string UploadDateStr { get; set; }
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public string UpdateDateStr { get; set; }
        //public string UploadIP { get; set; }
    }
}
