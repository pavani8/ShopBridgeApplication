using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridgeClassLibrary
{
    public interface IPackingOperations
    {
        Task AddPacking(Packing pack);
        Task<List<Packing>> getAllPackings();
        Task<Packing> getPackingById(int id);
        Task UpdatePacking(int id, Packing pack);
        Task DeletePacking(int id);
    }
}
