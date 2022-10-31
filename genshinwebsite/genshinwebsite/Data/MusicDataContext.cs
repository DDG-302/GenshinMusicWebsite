using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using genshinwebsite.Models;

namespace genshinwebsite.Data
{
    public class MusicDataContext:DbContext
    {
        public MusicDataContext(DbContextOptions<MusicDataContext> options)
           : base(options)
        {

        }

        public DbSet<MusicModel> Music { get; set; }
        public DbSet<UserModel> AspNetUsers { get; set; }

    
    }
}
