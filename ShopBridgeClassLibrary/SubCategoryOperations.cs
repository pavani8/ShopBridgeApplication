using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridgeClassLibrary
{
    public class SubCategoryOperations : ISubCategoryOperations
    {
        ProductOperations productOp;
        ShopBridgeDBEntities sde;
        public SubCategoryOperations()
        {
            sde = new ShopBridgeDBEntities();
            productOp = new ProductOperations();
        }
        public async Task AddSubCategory(SubCategory subcat)
        {
            try
            {
                sde.SubCategories.Add(subcat);
                await sde.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteSubCategory(int id)
        {
            try
            {
                SubCategory subcat = await getSubCategoryById(id);
                if (subcat != null)
                {
                    sde.Products.RemoveRange(subcat.Products);
                    await sde.SaveChangesAsync();
                    sde.SubCategories.Remove(subcat);
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

        public async Task<List<SubCategory>> getAllSubCategories()
        {
            return await sde.SubCategories.ToListAsync();
        }

        public async Task<SubCategory> getSubCategoryById(int id)
        {
            try
            {
                SubCategory subcat = await (from c in sde.SubCategories where c.subCategoryId == id select c).FirstOrDefaultAsync();
                return subcat;
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }

        public async Task UpdateSubCategory(int id, SubCategory subcat)
        {

            try
            {
                SubCategory old_subcat = await getSubCategoryById(id);
                old_subcat.subCategoryName = subcat.subCategoryName;
                old_subcat.modifiedAt = subcat.modifiedAt;
                old_subcat.modifiedBy = subcat.modifiedBy;

                sde.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
