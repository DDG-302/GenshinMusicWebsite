using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using genshinwebsite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace genshinwebsite.Data
{
    public class DataContext : IdentityDbContext<UserModel, IdentityRole<int>, int>
    {
        public DbSet<UserModel> User { get; set; }
        //public DbSet<MusicModel> Music { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        {
        
        }
    }
}

