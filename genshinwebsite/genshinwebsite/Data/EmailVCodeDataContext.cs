using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using genshinwebsite.Models;

namespace genshinwebsite.Data
{
    public class EmailVCodeDataContext : DbContext
    {
        public EmailVCodeDataContext(DbContextOptions<EmailVCodeDataContext> options)
           : base(options)
        {

        }
        public DbSet<EmailVCodeModel> EmailVcode { get; set; }

    }
}
