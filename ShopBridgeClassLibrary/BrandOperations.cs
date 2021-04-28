using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridgeClassLibrary
{
    public class BrandOperations : IBrandOpertions // implementing the interface 
    {
        ShopBridgeDBEntities sde;
        ProductOperations productOp;
        public BrandOperations()
        {
            sde = new ShopBridgeDBEntities();
            productOp = new ProductOperations();
        }
        
        // this method adds a new brand in brand table in db
        public async Task AddBrand(Brand brand)
        {
            try
            {
                sde.Brands.Add(brand);
                await sde.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // this method deletes a brand with matching brand id 
        public async Task<int> DeleteBrand(int id)
        {
            try
            {       
                Brand brand = await getBrandById(id);
                if (brand != null)
                {
                    sde.Products.RemoveRange(brand.Products);
                    await sde.SaveChangesAsync();
                    sde.Brands.Remove(brand);
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

        // this method returns the list of all brands available in the database
        public async Task<List<Brand>> getAllBrands()
        {
            return await sde.Brands.ToListAsync();
        }

        // this method returns the brand with matching brnad id
        public async Task<Brand> getBrandById(int id)
        {
            try
            {
                Brand brand = await (from b in sde.Brands where b.brandId == id select b).FirstOrDefaultAsync();
                return brand;
            }
            catch (Exception ex)
            {
                //throw new Exception("Record not found");
                //Console.WriteLine(ex);
                throw ex;
            }
        }

        // this method updates the matching brand details.
        public async Task UpdateBrand(int id, Brand brand)
        {
            try
            {
                Brand old_brand = await getBrandById(id);
                old_brand.brandName = brand.brandName;
                old_brand.modifiedAt = brand.modifiedAt;
                old_brand.modifiedBy = brand.modifiedBy;
                sde.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
