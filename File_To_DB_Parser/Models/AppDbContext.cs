using File_To_DB_Parser.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace File_To_DB_Parser.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DbConnectionString")
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionLine> TransactionLines { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures one to many relationship
            modelBuilder.Entity<TransactionLine>()
                .HasRequired<Transaction>(t => t.Transaction)
                .WithMany(g => g.TransactionLines)
                .HasForeignKey<int>(t => t.TransactionID);
        }
    }
}