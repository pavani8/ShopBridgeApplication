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
    public class StockController : ApiController
    {
        StockOperations  stockOp = new StockOperations();
        // GET: api/Stock
        public async Task<HttpResponseMessage> GetAll()
        {
            List<Stock> stocks = await stockOp.getAllStocks();
            return Request.CreateResponse(HttpStatusCode.OK, stocks);
        }
        public async Task<HttpResponseMessage> GetAllStocksByProductId(int productId)
        {
            List<Stock> stocks = await stockOp.getAllStocksByProduct(productId);
            return Request.CreateResponse(HttpStatusCode.OK, stocks);
        }

        // GET: api/Stock/5
        public async Task<HttpResponseMessage> GetStock(int Id)
        {
            Stock stock = await stockOp.getStockByStockId(Id);
            if (stock != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, stock);

            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Stock with Id = " + Id.ToString() + " not found");
            }
        }

        // POST: api/Stock
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
        public async Task<HttpResponseMessage> Put(int stockId, Stock stock)
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
        public async Task<HttpResponseMessage> DeleteStock(int stockId)
        {
            try
            {
                Stock stock = await stockOp.getStockByStockId(stockId);
                if (stock == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Stock with Id = " + stockId.ToString() + " not found to delete");
                }
                else
                {
                    await stockOp.DeleteStock(stockId);
                    return Request.CreateResponse(HttpStatusCode.OK, stock);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

    }
}
