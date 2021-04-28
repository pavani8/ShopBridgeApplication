using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridgeClassLibrary
{
    public class PackingOperations : IPackingOperations
    {
        ProductOperations productOp;
        ShopBridgeDBEntities sde;
        public PackingOperations()
        {
            sde = new ShopBridgeDBEntities();
            productOp = new ProductOperations();
        }
        public async Task AddPacking(Packing pack)
        {
            try
            {
                sde.Packings.Add(pack);
                await sde.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<int> DeletePacking(int id)
        {
            try
            {                
                Packing pack = await getPackingById(id);
                if (pack != null)
                {
                    sde.Products.RemoveRange(pack.Products);
                    await sde.SaveChangesAsync();
                    sde.Packings.Remove(pack);
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

        public async Task<List<Packing>> getAllPackings()
        {
            return await sde.Packings.ToListAsync();
        }

        public async Task<Packing> getPackingById(int id)
        {
            try
            {
                Packing pack = await (from p in sde.Packings where p.packingId == id select p).FirstOrDefaultAsync();
                return pack;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdatePacking(int id, Packing pack)
        {
            try
            {
                Packing old_pack = await getPackingById(id);
                old_pack.packingName = pack.packingName;
                old_pack.modifiedAt = pack.modifiedAt;
                old_pack.modifiedBy = pack.modifiedBy;

                sde.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
