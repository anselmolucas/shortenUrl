using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2EGroup.ShortenUrl.Models
{
    public class ShortenUrlContext : DbContext
    {
        public ShortenUrlContext():base("ShortenUrl")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Url> Urls { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>()
        //        .HasIndex("userNameIndex",          // Provide the index name.
        //            e => e.Property(x => x.Name));   // Specify at least one column.
                                
        //}
    }
}
