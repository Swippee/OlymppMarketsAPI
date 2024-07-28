using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OlymppMarketsAPI.Domain.Entities;
using OlymppMarketsAPI.Domain.Interfaces;
using OlymppMarketsAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlymppMarketsAPI.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly OlymppMarketsDbContext _context;

        public ProductRepository(OlymppMarketsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                 .Include(p => p.Price)
                 .Include(p => p.Stock)
                 .ToListAsync();
        }

        public async Task CreateProductAsync(Product product)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            var existingProduct = await _context.Products
           .Include(p => p.Price)
           .Include(p => p.Stock)
           .FirstOrDefaultAsync(p => p.Id == product.Id);

            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Brand = product.Brand;
                existingProduct.Size = product.Size;

                existingProduct.Price.Amount = product.Price.Amount;
                existingProduct.Stock.Quantity = product.Stock.Quantity;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Product with Id {product.Id} not found.");
            }
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products
                 .Include(p => p.Price)
                 .Include(p => p.Stock)
                 .FirstOrDefaultAsync(p=>p.Id==id);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
