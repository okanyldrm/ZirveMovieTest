using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zirve.Entity.Concrete;

namespace Zirve.DataAccess.Concrete
{
    public class ZirveMovieContext : IdentityDbContext<ApplicationUser> 
    {

        public ZirveMovieContext()
        {
        }

        public ZirveMovieContext(DbContextOptions<ZirveMovieContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionServices.connstring);



        }

       
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Email> Emails { get; set; }


    }
}
