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
    public class CategoryController : ApiController
    {
        CategoryOperations catOp = new CategoryOperations();
        // GET: api/Category
        public async Task<HttpResponseMessage> GetAll()
        {
            List<Category> result = await catOp.getAllCategories();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        // GET: api/Category/5
        public async Task<HttpResponseMessage> GetCategory(int Id)
        {
            Category category =  await catOp.getCategoryById(Id);
            if (category != null)
            {
               return  Request.CreateResponse(HttpStatusCode.OK, category);
            
            }
            else
            {
               return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category with Id = " + Id.ToString() + " not found");
            }
        }
        // POST: api/Category
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
            public async Task<HttpResponseMessage> Put(int id, Category category)
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
        public async Task<HttpResponseMessage> DeleteCategory(int id)
        {
            try
            {
             Category cat = await catOp.getCategoryById(id);
               if (cat == null)
               {
                   return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category with Id = " + id.ToString() + " not found to delete");
               }
               else
               {
                   await catOp.DeleteCategory(id);
                   return Request.CreateResponse(HttpStatusCode.OK, cat);
               }
           }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}
