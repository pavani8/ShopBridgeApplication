using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridgeClassLibrary
{
    public interface ICategoryOperations
    {
        Task AddCategory(Category cat);
        Task<List<Category>> getAllCategories();
        Task<Category> getCategoryById(int id);
        Task UpdateCategory(int id, Category cat);
        Task DeleteCategory(int id);
    }
}
