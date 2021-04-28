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
    public class CategoryController : ApiController
    {
        CategoryOperations catOp = new CategoryOperations();
        // GET: api/Category

        [Route("api/Category/GetAll")]
        public async Task<HttpResponseMessage> GetAll()
        {
            List<Category> result = await catOp.getAllCategories();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // GET: api/Category/5
        [Route("api/Category/GetCategoryById/{Id}")]
        public async Task<HttpResponseMessage> GetCategory(int Id)
        {
            try
            {
                Category category = await catOp.getCategoryById(Id);
                if (category != null)
                    return Request.CreateResponse(HttpStatusCode.OK, category);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category with Id = " + Id.ToString() + " not found");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // POST: api/Category
        [Route("api/Category/AddCategory")]
        public async Task<HttpResponseMessage> AddCategory([FromBody] Category category)
        {
            try
            {
                await catOp.AddCategory(category);             
                return Request.CreateResponse(HttpStatusCode.Created, category);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
            // PUT: api/Category/5
        [HttpPut]
        [Route("api/Category/UpdateCategory/{id}")]
        public async Task<HttpResponseMessage> UpdateCategory(int id, Category category)
        {
            try
            {
                Category cat = await catOp.getCategoryById(id);
                if (cat == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category with Id = " + id.ToString() + " not found to update");
                }
                else
                {
                    await catOp.UpdateCategory(id, category);
                    return Request.CreateResponse(HttpStatusCode.OK, cat);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            }

        // DELETE: api/Category/5
        [Route("api/Category/DeleteCategory/{id}")]
        public async Task<HttpResponseMessage> DeleteCategory(int id)
        {
            try
            {       
                int response = await catOp.DeleteCategory(id);
                if (response == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category with Id = " + id.ToString() + " not found to delete");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Category with Id = " + id.ToString() + " is deleted");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}
