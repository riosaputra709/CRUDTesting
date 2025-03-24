using APICRUDTESTING.Data;
using APICRUDTESTING.Models;
using Microsoft.EntityFrameworkCore;

namespace APICRUDTESTING.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProduct()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<List<Product>> SearchProductsByKeyword(string keyword)
        {
            // Logika pencarian produk berdasarkan kata kunci
            return await _context.Products
                .Where(p => p.Name.Contains(keyword))
                .ToListAsync();
        }

        public async Task<List<Product>> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return await _context.Products.ToListAsync();
        }

        public async Task<List<Product>?> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return null;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> UpdateProduct(int id, Product request)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return null;

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.CreatedAt = request.CreatedAt;

            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> GetSingleProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return null;
            return product;
        }
    }
}
