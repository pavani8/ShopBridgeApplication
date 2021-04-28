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
    public class BrandController : ApiController
    {
        BrandOperations brandOp = new BrandOperations();

        // GET: api/Brand
        [Route("api/Brand/GetAll")]
        public async Task<HttpResponseMessage> GetAll()
        {
            List<Brand> result = await brandOp.getAllBrands();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // GET: api/Brand/5
        [Route("api/Brand/GetBrandById/{Id}")]
        public async Task<HttpResponseMessage> GetBrand(int Id)
        {
            try
            {
                Brand brand = await brandOp.getBrandById(Id);
                if (brand != null)
                    return Request.CreateResponse(HttpStatusCode.OK, brand);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Brand with Id = " + Id.ToString() + " not found");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // POST: api/Brand
        [Route("api/Brand/AddBrand")]
        public async Task<HttpResponseMessage> AddBrand([FromBody] Brand brand)
        {
            try
            {
                await brandOp.AddBrand(brand);                
                return Request.CreateResponse(HttpStatusCode.Created, brand);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    // PUT: api/Brand/5
        [HttpPut]
        [Route("api/Brand/UpdateBrand/{id}")]
        public async Task<HttpResponseMessage> UpdateBrand(int id, Brand brand)
        {
            try
            {
                Brand _brand = await brandOp.getBrandById(id);
                if (_brand == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Brand with Id = " + id.ToString() + " not found to update");
                }
                else
                {
                    await brandOp.UpdateBrand(id, brand);
                    return Request.CreateResponse(HttpStatusCode.OK, _brand);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        // DELETE: api/Brand/5
        [Route("api/Brand/DeleteBrand/{id}")]
        public async Task<HttpResponseMessage> DeleteBrand(int id)
        {
            try
            {             
                int response = await brandOp.DeleteBrand(id);
                if (response == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Brand with Id = " + id.ToString() + " not found to delete");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Brand with Id = " + id.ToString() + " is deleted");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
