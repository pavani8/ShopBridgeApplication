using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridgeClassLibrary
{
    public interface ISubCategoryOperations
    {
        Task AddSubCategory(SubCategory subcat);
        Task<List<SubCategory>> getAllSubCategories();
        Task<SubCategory> getSubCategoryById(int id);
        Task UpdateSubCategory(int id, SubCategory subcat);
        Task DeleteSubCategory(int id);
    }
}
