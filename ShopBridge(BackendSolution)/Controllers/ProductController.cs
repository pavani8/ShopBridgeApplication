using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
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
        [Route("api/Product/GetAll")]
        public async Task<HttpResponseMessage> GetAll()
        {
            List<Product> products = await productOp.getAllProducts();
            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

        [Route("api/Product/GetProductsByCategory/{categoryId}")]
        public async Task<HttpResponseMessage> GetAllProductsByCategory(int categoryId)
        {
            List<Product> products = await productOp.getAllProductsByCategory(categoryId);
            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

        [Route("api/Product/GetProductsBySubCategory/{subCategoryId}")]
        public async Task<HttpResponseMessage> GetAllProductsBySubCategory(int subCategoryId)
        {
            List<Product> products = await productOp.getAllProductsBySubCategory(subCategoryId);
            return Request.CreateResponse(HttpStatusCode.OK, products);
        }



        [Route("api/Product/GetProductsByBrand/{brandId}")]
        public async Task<HttpResponseMessage> GetAllProductsByBrand(int brandId)
        {
            List<Product> products = await productOp.getAllProductsByBrand(brandId);
            return Request.CreateResponse(HttpStatusCode.OK, products);
        }


        [Route("api/Product/GetProductsByPacking/{packId}")]
        public async Task<HttpResponseMessage> GetAllProductsByPacking(int packId)
        {
            List<Product> products = await productOp.getAllProductsByPacking(packId);
            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

        // GET: api/Product/5 
        [Route("api/Product/GetProductById/{productId}")]
        public async Task<HttpResponseMessage> GetProduct(int productId)
        {
            
            try
            {
                Product product = await productOp.getProductById(productId);
                if (product != null)
                    return Request.CreateResponse(HttpStatusCode.OK, product);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with Id = " + productId.ToString() + " not found");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        // search api to search for a product in product list

        [Route("api/Product/SearchProduct/{search_str}")]
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
        [Route("api/Product/AddProduct")]
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
        [HttpPut]
        [Route("api/Product/UpdateProduct/{productId}")]
        public async Task<HttpResponseMessage> UpdateProduct(int productId, Product product)
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

        [Route("api/Product/DeleteProduct/{productId}")]
        public async Task<HttpResponseMessage> DeleteProduct(int productId)
        {
            try
            {             
                int response = await productOp.DeleteProduct(productId);
                if (response == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with Id = " + productId.ToString() + " not found to delete");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Product with Id = " + productId.ToString() + " is deleted");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}
