using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridgeClassLibrary
{
    public class StockOperations : IStockOperations
    {
        ShopBridgeDBEntities sde;
        public StockOperations()
        {
            sde = new ShopBridgeDBEntities(); // creating an object for database entities class
        }

        // this method adds a new stock in the database.
        public async Task AddStock(Stock stock)
        {
            try
            {
                sde.Stocks.Add(stock);
                await sde.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // this method deletes the stock with matching stock id from database.
        public async Task<int> DeleteStock(int stockId)
        {
            try
            {               
                Stock stock = await getStockByStockId(stockId);
                if (stock != null)
                {
                    sde.Stocks.Remove(stock);
                    sde.SaveChanges();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<Stock>> getAllStocks()
        {
            return await sde.Stocks.ToListAsync();
        }

        // this method returns all the stocks of the product based on given product id
        public async Task<List<Stock>> getAllStocksByProduct(int productId)
        {
            try
            {
                List<Stock> stocks = new List<Stock>();
                stocks = await (from s in sde.Stocks where s.productId == productId select s).ToListAsync();
                return stocks;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //this method returns the stock with matching stock id
        public async Task<Stock> getStockByStockId(int stockId)
        {
            try
            {
                Stock stock = await (from s in sde.Stocks where s.stockId == stockId select s).FirstOrDefaultAsync();
                return stock;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // this method updates the stock details
        public async Task UpdateStock(int stockId, Stock stock)
        {
            try
            {
                Stock old_stock = await getStockByStockId(stockId);
                old_stock.productId = stock.productId;
                old_stock.batchCode = stock.batchCode;
                old_stock.quantity = stock.quantity;
                old_stock.sellingPrice = stock.sellingPrice;
                old_stock.purchasePrice = stock.purchasePrice;
                old_stock.modifiedAt = stock.modifiedAt;
                old_stock.modifiedBy = stock.modifiedBy;
                sde.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
