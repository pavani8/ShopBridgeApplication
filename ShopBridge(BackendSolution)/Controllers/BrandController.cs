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
    public class BrandController : ApiController
    {
        BrandOperations brandOp = new BrandOperations();

        // GET: api/Brand
        public async Task<HttpResponseMessage> GetAll()
        {
            List<Brand> result = await brandOp.getAllBrands();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        // GET: api/Brand/5
        public async Task<HttpResponseMessage> GetBrand(int Id)
        {
            Brand brand = await brandOp.getBrandById(Id);
            if (brand != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, brand);

            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Brand with Id = " + Id.ToString() + " not found");
            }
        }

        // POST: api/Brand
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
    public async Task<HttpResponseMessage> Put(int id, Brand brand)
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
    public async Task<HttpResponseMessage> DeleteBrand(int id)
    {
            try
            {
                Brand brand = await brandOp.getBrandById(id);
                if (brand == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Brand with Id = " + id.ToString() + " not found to delete");
                }
                else
                {
                    await brandOp.DeleteBrand(id);
                    return Request.CreateResponse(HttpStatusCode.OK, brand);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }


        }
    }
}
