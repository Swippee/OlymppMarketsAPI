using Microsoft.EntityFrameworkCore;
using OlymppMarketsAPI.Domain.Entities;
using System.Text.Json;

namespace OlymppMarketsAPI.Infrastructure.Data
{
    public class OlymppMarketsDbContext : DbContext
    {
        public OlymppMarketsDbContext(DbContextOptions<OlymppMarketsDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Price)
                .WithOne(p => p.Product)
                .HasForeignKey<Price>(p => p.ProductId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Stock)
                .WithOne(s => s.Product)
                .HasForeignKey<Stock>(s => s.ProductId);
        }

        public static void Seed(OlymppMarketsDbContext context)
        {
            if (!context.Products.Any())
            {
                var data = File.ReadAllText("data.json");
                var seedData = JsonSerializer.Deserialize<List<Product>>(data);

                if (seedData != null)
                {
                    foreach (var product in seedData)
                    {
                        var price = new Price { Amount = product.Price.Amount };
                        var stock = new Stock { Quantity = product.Stock.Quantity };

                        var newProduct = new Product
                        {
                            Name = product.Name,
                            Brand = product.Brand,
                            Size = product.Size,
                            Price = price,
                            Stock = stock
                        };

                        context.Products.Add(newProduct);
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
