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
    public class StockController : ApiController
    {
        StockOperations  stockOp = new StockOperations();
        // GET: api/Stock
        [Route("api/Stock/GetAll")]
        public async Task<HttpResponseMessage> GetAll()
        {
            List<Stock> stocks = await stockOp.getAllStocks();
            return Request.CreateResponse(HttpStatusCode.OK, stocks);
        }
        [Route("api/Stock/GetStocksByProductId/{productId}")]
        public async Task<HttpResponseMessage> GetAllStocksByProductId(int productId)
        {
            List<Stock> stocks = await stockOp.getAllStocksByProduct(productId);
            return Request.CreateResponse(HttpStatusCode.OK, stocks);
        }

        // GET: api/Stock/5
        [Route("api/Stock/GetStockByStockId/{Id}")]
        public async Task<HttpResponseMessage> GetStockByStockId(int stockId)
        {
           

            try
            {
                Stock stock = await stockOp.getStockByStockId(stockId);
                if (stock != null)
                    return Request.CreateResponse(HttpStatusCode.OK, stock);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Stock with Id = " + stockId.ToString() + " not found");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // POST: api/Stock
        [Route("api/Stock/AddStock")]
        public async Task<HttpResponseMessage> AddStock([FromBody] Stock stock)
        {
            try
            {
                await stockOp.AddStock(stock);
                return Request.CreateResponse(HttpStatusCode.Created, stock);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // PUT: api/Stock/5
        [HttpPut]

        [Route("api/Stock/UpdateStock/{stockId}")]
        public async Task<HttpResponseMessage> UpdateStock(int stockId, Stock stock)
        {
            try
            {
                Stock _stock = await stockOp.getStockByStockId(stockId);
                if (_stock == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Stock with Id = " + stockId.ToString() + " not found to update");
                }
                else
                {
                    await stockOp.UpdateStock(stockId, stock);
                    return Request.CreateResponse(HttpStatusCode.OK, _stock);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }


        // DELETE: api/Stock/5
        [Route("api/Stock/DeleteStock/{stockId}")]
        public async Task<HttpResponseMessage> DeleteStock(int stockId)
        {
            try
            {               
                int response = await stockOp.DeleteStock(stockId);
                if (response == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Stock with Id = " + stockId.ToString() + " not found to delete");
                }
                else
                {

                    return Request.CreateResponse(HttpStatusCode.OK, "Stock with Id = " + stockId.ToString() + " is deleted");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

    }
}
