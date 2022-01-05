using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using russu_vlad_proiect_final.Models;

namespace russu_vlad_proiect_final.Data
{
    public class RecordStoreContext:DbContext
    {
        public RecordStoreContext(DbContextOptions<RecordStoreContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Album> Albums { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Album>().ToTable("Album");
        }
    }
}
