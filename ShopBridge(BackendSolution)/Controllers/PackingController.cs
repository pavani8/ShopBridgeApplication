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
    public class PackingController : ApiController
    {
        PackingOperations packOp = new PackingOperations();
        // GET: api/Packing
        [Route("api/Packing/GetAll")]
        public async Task<HttpResponseMessage> GetAll()
        {
            List<Packing> result = await packOp.getAllPackings();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // GET: api/Packing/5
        [Route("api/Packing/GetPackingById/{Id}")]
        public async Task<HttpResponseMessage> GetPacking(int Id)
        {
            try
            {
                Packing pack = await packOp.getPackingById(Id);
                if (pack != null)
                    return Request.CreateResponse(HttpStatusCode.OK, pack);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Packing with Id = " + Id.ToString() + " not found");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // POST: api/Packing
        [Route("api/Packing/AddPacking")]
        public async Task<HttpResponseMessage> AddPacking([FromBody] Packing pack)
          {
                try
                {
                    await packOp.AddPacking(pack);
                    return Request.CreateResponse(HttpStatusCode.Created, pack);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
          }

        // PUT: api/Packing/5
        [HttpPut]

        [Route("api/Packing/UpdatePacking/{id}")]
        public async Task<HttpResponseMessage> UpdatePacking(int id, Packing pack)
        {
            try
            {
                Packing packing = await packOp.getPackingById(id);
                if (packing == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Packing with Id = " + id.ToString() + " not found to update");
                }
                else
                {
                    await packOp.UpdatePacking(id, pack);
                    return Request.CreateResponse(HttpStatusCode.OK, packing);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }


        }

        // DELETE: api/Packing/5

        [Route("api/Packing/DeletePacking/{id}")]
        public async Task<HttpResponseMessage> DeletePacking(int id)
        {
            try
            {               
                int response = await packOp.DeletePacking(id);
                if (response == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Packing with Id = " + id.ToString() + " not found to delete");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Packing with Id = " + id.ToString() + " is deleted");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }


        }
    }
}
