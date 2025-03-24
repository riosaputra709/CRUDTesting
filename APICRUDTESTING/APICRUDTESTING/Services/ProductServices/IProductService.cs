using APICRUDTESTING.Models;

namespace APICRUDTESTING.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProduct();
        Task<List<Product>> AddProduct(Product product);
        Task<List<Product>?> DeleteProduct(int id);
        Task<Product> UpdateProduct(int id, Product request);
        Task<Product?> GetSingleProduct(int id);
        Task<List<Product>> SearchProductsByKeyword(string keyword);
    }
}
