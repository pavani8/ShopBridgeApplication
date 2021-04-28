using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopBridgeClassLibrary;


namespace ShopBridge_BackendSolution_.Controllers
{
    public class SubCategoryController : ApiController
    {
        
        SubCategoryOperations subCatOp = new SubCategoryOperations();
        // GET: api/SubCategory
        [Route("api/SubCategory/GetAll")]
        public async Task<HttpResponseMessage> GetAll()
        {
            List<SubCategory> subcategories = await subCatOp.getAllSubCategories();
            return Request.CreateResponse(HttpStatusCode.OK, subcategories);
        }
        // GET: api/SubCategory/5
        [Route("api/SubCategory/GetSubCategoryById/{Id}")]
        public async Task<HttpResponseMessage> GetSubCategory(int Id)
        {
            
            try
            {
                SubCategory subCategory = await subCatOp.getSubCategoryById(Id);
                if (subCategory != null)
                    return Request.CreateResponse(HttpStatusCode.OK, subCategory);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "SubCategory with Id = " + Id.ToString() + " not found");

            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        // POST: api/SubCategory
        // [HttpPost]
        [Route("api/SubCategory/AddSubCategory")]
        public async Task<HttpResponseMessage> AddSubCategory([FromBody] SubCategory subCategory)
        {
            try
            {
                await subCatOp.AddSubCategory(subCategory);
                return Request.CreateResponse(HttpStatusCode.Created, subCategory);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        // PUT: api/SubCategory/5
        [HttpPut]
        [Route("api/SubCategory/UpdateSubCategory/{Id}")]
        public async Task<HttpResponseMessage> UpdateSubCategory(int id, [FromBody] SubCategory subCategory)
        {
            try
            {
                SubCategory subcat = await subCatOp.getSubCategoryById(id);
                if (subcat == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "SubCategory with Id = " + id.ToString() + " not found to update");
                }
                else
                {
                    await subCatOp.UpdateSubCategory(id, subCategory);
                    return Request.CreateResponse(HttpStatusCode.OK, subcat);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        // DELETE: api/SubCategory/5
        [Route("api/SubCategory/DeleteSubCategory/{Id}")]
        public async Task<HttpResponseMessage> DeleteSubCategory(int id)
        {
            try
            {
                int response = await subCatOp.DeleteSubCategory(id);
                if (response == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "SubCategory with Id = " + id.ToString() + " not found to delete");
                }
                else
                {                  
                    return Request.CreateResponse(HttpStatusCode.OK, "SubCategory with Id = " + id.ToString() + " is deleted");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }


        }

    }
}
