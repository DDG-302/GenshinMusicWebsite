using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using genshinwebsite.Models;
namespace genshinwebsite.Services
{
    public interface ICommentService<T> where T: class
    {
        public DBOperationResult add_comment(T comment);
    }
}
