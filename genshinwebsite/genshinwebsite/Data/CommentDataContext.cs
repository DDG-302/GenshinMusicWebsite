using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using genshinwebsite.Models;

namespace genshinwebsite.Data
{
    public class CommentDataContext:DbContext
    {
        public CommentDataContext(DbContextOptions<CommentDataContext> options)
          : base(options)
        {

        }

        public DbSet<CommentModel> Comment { get; set; }
        public DbSet<UserModel> AspNetUsers { get; set; }
        public DbSet<MusicModel> Music { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CommentModel>().HasKey(t => new { t.Uid, t.Muid });

        }
    }
}
