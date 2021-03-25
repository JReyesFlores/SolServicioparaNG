using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class HelperServiceContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public HelperServiceContext(DbContextOptions<HelperServiceContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            var products = new List<Product>();
            products.Add(new Product()
            {
                id = "1",
                image = "assets/images/camiseta.png",
                title = "Camiseta",
                price = 1500,
                description = "Bla bla bla Bla bla bla Bla bla bla Bla bla bla"
            });

            products.Add(new Product()
            {
                id = "2",
                image = "assets/images/hoodie.png",
                title = "Hoodie",
                price = 1600,
                description = "Bla bla bla Bla bla bla Bla bla bla Bla bla bla"
            });

            products.Add(new Product()
            {
                id = "3",
                image = "assets/images/mug.png",
                title = "Mug",
                price = 1700,
                description = "Bla bla bla Bla bla bla Bla bla bla Bla bla bla"
            });

            products.Add(new Product()
            {
                id = "4",
                image = "assets/images/pin.png",
                title = "Pin",
                price = 1800,
                description = "Bla bla bla Bla bla bla Bla bla bla Bla bla bla"
            });

            products.Add(new Product()
            {
                id = "5",
                image = "assets/images/stickers1.png",
                title = "Sticker 1",
                price = 1900,
                description = "Bla bla bla Bla bla bla Bla bla bla Bla bla bla"
            });

            products.Add(new Product()
            {
                id = "6",
                image = "assets/images/stickers2.png",
                title = "Sticker 2",
                price = 2000,
                description = "Bla bla bla Bla bla bla Bla bla bla Bla bla bla"
            });

            modelBuilder.Entity<Product>().HasData(products);
        }
    }
}
