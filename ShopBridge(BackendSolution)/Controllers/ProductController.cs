using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using ShopBridgeClassLibrary;

namespace ShopBridge_BackendSolution_.Controllers
{
    //[Route("api/[controller]")]
    public class ProductController : ApiController
    {
        ProductOperations productOp = new ProductOperations();
        // GET: api/Product
        //[HttpGet]
        public async Task<HttpResponseMessage> GetAll()
        {
            List<Product> products = await productOp.getAllProducts();
            return Request.CreateResponse(HttpStatusCode.OK, products);
        }
        public async Task<HttpResponseMessage> GetAllProductsByCategory(int categoryId)
        {
            List<Product> products = await productOp.getAllProductsByCategory(categoryId);
            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

        public async Task<HttpResponseMessage> GetAllProductsBySubCategory(int subCategoryId)
        {
            List<Product> products = await productOp.getAllProductsBySubCategory(subCategoryId);
            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

        public async Task<HttpResponseMessage> GetAllProductsByBrand(int brandId)
        {
            List<Product> products = await productOp.getAllProductsByBrand(brandId);
            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

        public async Task<HttpResponseMessage> GetAllProductsByPacking(int packId)
        {
            List<Product> products = await productOp.getAllProductsByPacking(packId);
            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

        // GET: api/Product/5    
        public async Task<HttpResponseMessage> GetProduct(int productId)
        {
            Product product = await productOp.getProductById(productId);
            if (product != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, product);

            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with Id = " + productId.ToString() + " not found");
            }
        }
        // search api to search for a product in product list
        public async Task<HttpResponseMessage> Search_Product(string search_str)
        {
            try
            {
                List<Product> products = await productOp.searchProducts(search_str);
                return Request.CreateResponse(HttpStatusCode.OK, products);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // POST: api/Product
        public async Task<HttpResponseMessage> AddProduct([FromBody] Product product)
        {
            try
            {
                await productOp.AddProduct(product);
                return Request.CreateResponse(HttpStatusCode.Created,product);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex.Message);
            }
        }
     
        // PUT: api/Product/5
        public async Task<HttpResponseMessage> Put(int productId, Product product)
        {
            try
            {
                Product prod = await productOp.getProductById(productId);
                if (prod == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with Id = " + productId.ToString() + " not found to update");
                }
                else
                {
                    await productOp.UpdateProduct(productId, product);
                    return Request.CreateResponse(HttpStatusCode.OK, prod);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }


        }

        // DELETE: api/Product/5
        public async Task<HttpResponseMessage> DeleteProduct(int productId)
        {
            try
            {
                Product product = await productOp.getProductById(productId);
                if (product == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category with Id = " + productId.ToString() + " not found to delete");
                }
                else
                {
                    await productOp.DeleteProduct(productId);
                    return Request.CreateResponse(HttpStatusCode.OK, product);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}
