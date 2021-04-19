using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using ShopBridgeClassLibrary;


namespace ShopBridge_BackendSolution_.Controllers
{
    public class SubCategoryController : ApiController
    {
        
        SubCategoryOperations subCatOp = new SubCategoryOperations();
        // GET: api/SubCategory
        public async Task<HttpResponseMessage> GetAll()
        {
            List<SubCategory> subcategories = await subCatOp.getAllSubCategories();
            return Request.CreateResponse(HttpStatusCode.OK, subcategories);
        }
        // GET: api/SubCategory/5
        public async Task<HttpResponseMessage> GetSubCategory(int Id)
        {
            SubCategory subCategory = await subCatOp.getSubCategoryById(Id);
            if (subCategory != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, subCategory);

            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "SubCategory with Id = " + Id.ToString() + " not found");
            }
        }
        // POST: api/SubCategory
        public async Task<HttpResponseMessage> AddSubCategory([FromBody] SubCategory subCategory)
        {
            try
            {
                await subCatOp.AddSubCategory(subCategory);
                Console.WriteLine(subCategory);
                return Request.CreateResponse(HttpStatusCode.Created, subCategory);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        // PUT: api/SubCategory/5
        public async Task<HttpResponseMessage> Put(int id, SubCategory subCategory)
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
        public async Task<HttpResponseMessage> DeleteSubCategory(int id)
        {
            try
            {
                SubCategory subcat = await subCatOp.getSubCategoryById(id);
                if (subcat == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "SubCategory with Id = " + id.ToString() + " not found to delete");
                }
                else
                {
                    await subCatOp.DeleteSubCategory(id);
                    return Request.CreateResponse(HttpStatusCode.OK, subcat);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }


        }

    }
}
