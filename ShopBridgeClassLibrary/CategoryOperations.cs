using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridgeClassLibrary
{
    public class CategoryOperations : ICategoryOperations
    {
        ProductOperations productOp;
        ShopBridgeDBEntities sde;
        public CategoryOperations()
        {
            sde = new ShopBridgeDBEntities();
            productOp = new ProductOperations();
        }
        public async Task AddCategory(Category cat)
        {
            try
            {
                sde.Categories.Add(cat);
                await sde.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteCategory(int id)
        {
            try
            {         
                Category cat = await getCategoryById(id);
                if (cat != null)
                {
                    sde.Products.RemoveRange(cat.Products);
                    await sde.SaveChangesAsync();
                    sde.Categories.Remove(cat);
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

        public async Task<List<Category>> getAllCategories()
        {
            return await sde.Categories.ToListAsync();
        }

        public async Task<Category> getCategoryById(int id)
        {
            try
            {
             Category cat =  await (from c in sde.Categories where c.categoryId == id select c).FirstOrDefaultAsync();
              return cat;
            }
            catch (Exception ex)
            {               
                throw ex;

            }
        }

        public async Task UpdateCategory(int id, Category cat)
        {
            try
            {
                Category old_cat =  await getCategoryById(id);
                old_cat.categoryName = cat.categoryName;
                old_cat.modifiedAt = cat.modifiedAt;
                old_cat.modifiedBy = cat.modifiedBy;

                await sde.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
