using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShopBridgeClassLibrary
{
    public class ProductOperations : IProductOperations // implementing the interface in this class
    {
        ShopBridgeDBEntities sde; // defining an object for database entities class
        StockOperations stockOp;
        public ProductOperations()
        {
            sde = new ShopBridgeDBEntities(); // creating an object for database entities class
            stockOp = new StockOperations(); 
        }

        // AddProduct method to add new product to database.
        public async Task AddProduct(Product product)
        {
            try
            {
                sde.Products.Add(product);
               await sde.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        // DeleteProduct method to add new product to database.
        public async Task DeleteProduct(int productId)
        {
            try
            {
                List<Stock> stocks = new List<Stock>();
                stocks = await stockOp.getAllStocksByProduct(productId);
                sde.Stocks.RemoveRange(stocks);
                // In order to delete product, first we need to delete stock then we need to delete products
                Product prod = await getProductById(productId);
                sde.Products.Remove(prod); // deleting a product from product list
                sde.SaveChanges(); // saving the changes to database
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // This method returns the list of all products available in database
        public async Task<List<Product>> getAllProducts()
        {
            return await sde.Products.ToListAsync();
        }

        // this method filters the products by given brand and returns those products
        public async Task<List<Product>> getAllProductsByBrand(int brandId)
        {
            try
            {
                List<Product> products = new List<Product>();
                products = await (from p in sde.Products where p.brandId == brandId select p).ToListAsync();
                return products;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // this method filters the products by given category and returns those products
        public async Task<List<Product>> getAllProductsByCategory(int catId)
        {
            try
            {
                List<Product> products = new List<Product>();
                products = await (from p in sde.Products where p.categoryId == catId select p).ToListAsync();
                return products;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // this method filters the products by given packing and returns those products
        public async Task<List<Product>> getAllProductsByPacking(int packingId)
        {
            try
            {
                List<Product> products = new List<Product>();
                products = await (from p in sde.Products where p.packingId == packingId select p).ToListAsync();
                return products;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // this method filters the products by given subCategory and returns those products
        public async Task<List<Product>> getAllProductsBySubCategory(int subCatId)
        {
            try
            {
                List<Product> products = new List<Product>();
                products = await (from p in sde.Products where p.subCategoryId == subCatId select p).ToListAsync();
                return products;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // this method returns the product with matching productid
        public async Task<Product> getProductById(int productId)
        {
            try
            {
                Product prod = await (from p in sde.Products where p.productId == productId select p).FirstOrDefaultAsync();
                return  prod;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // this method searches the products based on user input.
        public async Task<List<Product>> searchProducts(string search_string)
        {
            try
            {
                List<Product> matching_products = await (from item in sde.Products
                                                         where item.name.ToLower().Contains(search_string)
                                                         select item).ToListAsync();
                return matching_products;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //this method updates the existing product details
        public async Task UpdateProduct(int id, Product product)
        {
            try
            {
                Product old_prod = await getProductById(id);
                old_prod.name = product.name;
                old_prod.image = product.image;
                old_prod.MRP = product.MRP;
                old_prod.categoryId = product.categoryId;
                old_prod.subCategoryId = product.subCategoryId;
                old_prod.brandId = product.brandId;
                old_prod.packingId = product.packingId;
                old_prod.description = product.description;
                old_prod.status = product.status;
                old_prod.modifiedAt = product.modifiedAt;
                old_prod.modifiedBy = product.modifiedBy;

               await sde.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
