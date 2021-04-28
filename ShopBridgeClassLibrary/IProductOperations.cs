using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridgeClassLibrary
{
    public interface IProductOperations  // this is an interface for product operations
    {
        Task AddProduct(Product product);
        Task<List<Product>> getAllProducts();
        Task<Product> getProductById(int id);
        Task UpdateProduct(int id, Product product);
        Task<int> DeleteProduct(int id);
        Task<List<Product>> getAllProductsByCategory(int catId);
        Task<List<Product>> getAllProductsByBrand(int brandId);
        Task<List<Product>> getAllProductsBySubCategory(int subCatId);
        Task<List<Product>> getAllProductsByPacking(int packingId);
        Task<List<Product>> searchProducts(string search_string);


    }
}
