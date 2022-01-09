using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaleCore.Models;
using DaleInfraestructure.Models;

namespace DaleInfraestructure.Implementations
{
    public class Producto
    {
        public static bool AddProductos(DaleCore.Models.Producto producto)
        {
            bool add = false;
            using (Models.DaleDbContext db = new DaleDbContext())
            {
                db.Productos.Add(producto);
                db.SaveChanges();
                add = producto.Id > 0 ? true : false;
            }
            return add;
        }

        public static bool UpdateProductos(DaleCore.Models.Producto producto)
        {
            bool add = false;
            using (Models.DaleDbContext db = new DaleDbContext())
            {
                db.Entry<DaleCore.Models.Producto>(producto).State = EntityState.Modified;
                db.SaveChanges();
                add = true;
            }
            return add;
        }

        public static List<DaleCore.Models.Producto> GetProductos()
        {
            List<DaleCore.Models.Producto> clientes = new List<DaleCore.Models.Producto>();

            using (Models.DaleDbContext db = new DaleDbContext())
            {
                IQueryable<DaleCore.Models.Producto> custQuery =
                    from cust in db.Productos
                    select cust;
                clientes = custQuery.ToList();

            }
            return clientes;
        }

        public static bool DeleteProductos(DaleCore.Models.Producto producto)
        {
            bool add = false;
            using (Models.DaleDbContext db = new DaleDbContext())
            {
                db.Entry<DaleCore.Models.Producto>(producto).State = EntityState.Deleted;
                db.SaveChanges();
                add = true;
            }
            return add;
        }
    }
}
