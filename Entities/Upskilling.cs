using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Upskilling.Models;

namespace Upskilling.Entities
{
    public class UpskillingDbContext :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = .; Database = UpskillingTask; Trusted_Connection = True; TrustServerCertificate = True;");
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

    }
}
