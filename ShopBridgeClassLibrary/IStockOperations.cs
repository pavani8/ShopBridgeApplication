using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridgeClassLibrary
{
    public interface IStockOperations
    {
        Task AddStock(Stock stock);
        Task<List<Stock>> getAllStocks();
        Task<List<Stock>> getAllStocksByProduct(int productId);
        Task<Stock> getStockByStockId(int stockId);
        Task UpdateStock(int id, Stock stock);
        Task<int> DeleteStock(int stockId);
    }
}
