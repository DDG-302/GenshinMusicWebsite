using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using genshinwebsite.Models;
using genshinwebsite.Services;
using genshinwebsite.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace genshinwebsite.Services
{
    public class UserRepository : IRepository<UserModel>
        //public class UserRepository 
    {
        private readonly DataContext Context;
        public UserRepository(DataContext context)
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

        public UserModel Find_by_account(string account)
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
    }
}
