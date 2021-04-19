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
    public class PackingController : ApiController
    {
        PackingOperations packOp = new PackingOperations();
        // GET: api/Packing
        public async Task<HttpResponseMessage> GetAll()
        {
            List<Packing> result = await packOp.getAllPackings();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        // GET: api/Packing/5
        public async Task<HttpResponseMessage> GetPacking(int Id)
        {
            Packing pack = await packOp.getPackingById(Id);
            if (pack != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, pack);

            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Packing with Id = " + Id.ToString() + " not found");
            }
        }

            // POST: api/Packing
            public async Task<IHttpActionResult> AddPacking([FromBody] Packing pack)
            {
                try
                {
                    await packOp.AddPacking(pack);
                    return Ok(Request.CreateResponse(HttpStatusCode.Created, pack));
                }
                catch (Exception ex)
                {
                    return Ok(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message));
                }
            }

        // PUT: api/Packing/5
        public async Task<HttpResponseMessage> Put(int id, Packing pack)
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
        public async Task<HttpResponseMessage> DeletePacking(int id)
        {
            try
            {
                Packing pack = await packOp.getPackingById(id);
                if (pack == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Packing with Id = " + id.ToString() + " not found to delete");
                }
                else
                {
                    await packOp.DeletePacking(id);
                    return Request.CreateResponse(HttpStatusCode.OK, pack);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }


        }
    }
}
