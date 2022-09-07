using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using genshinwebsite.Models;
using genshinwebsite.Data;

namespace genshinwebsite.Services
{
    public class EFCoreRepository : IRepository<UserModel>
    {
        private readonly DataContext Context;
        public EFCoreRepository(DataContext context)
        {
            Context = context;
        }

        public UserModel Add(UserModel new_model)
        {
            Context.User.Add(new_model);
            Context.SaveChanges();
            return new_model;
        }

        public UserModel Delete(UserModel old_model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> Get_all()
        {
            return Context.User.ToList();
        }

        public UserModel Get_by_id(int id)
        {
            return Context.User.Find(id);
        }

        public UserModel Update(int id, UserModel new_model)
        {
            throw new NotImplementedException();
        }

        public UserModel Find_by_account(string account)
        {
            //var user = from d in Context.User where d.Account == account select d;
            return new UserModel();
        }
    }
}
