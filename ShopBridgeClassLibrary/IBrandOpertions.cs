using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridgeClassLibrary
{
    public interface IBrandOpertions
    {
        Task AddBrand(Brand brand);
        Task<List<Brand>> getAllBrands();
        Task<Brand> getBrandById(int id);
        Task UpdateBrand(int id, Brand brand);
        Task<int> DeleteBrand(int id);
    }
}
