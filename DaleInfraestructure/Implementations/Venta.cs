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
    public class Venta
    {
        public static List<DaleCore.Models.Venta> GetVentas()
        {
            List<DaleCore.Models.Venta> ventas = new List<DaleCore.Models.Venta>();

            using (Models.DaleDbContext db = new DaleDbContext())
            {
                IQueryable<DaleCore.Models.Venta> custQuery =
                    from cust in db.Ventas
                    select cust;
                ventas = custQuery.Include(s=>s.Cliente).ToList();

            }
            return ventas;
        }

        public static bool AddVenta(DaleCore.Models.Venta venta, List<DetalleVenta> detallesVenta)
        {
            bool add = false;
            using (Models.DaleDbContext db = new DaleDbContext())
            {
                venta.Cliente = db.Clientes.Where(s => s.Id == venta.Cliente.Id).FirstOrDefault(); 
                db.Entry<DaleCore.Models.Venta>(venta).State = EntityState.Added;
                db.SaveChanges();
                detallesVenta.ForEach(s => { s.Venta = venta; s.Producto = db.Productos.Where(j => j.Id == s.Producto.Id).FirstOrDefault(); });
                db.DetallesVenta.AddRange(detallesVenta);
                db.SaveChanges();
                add = venta.Id > 0 ? true : false;
            }
            return add;
        }

        public static bool UpdateVenta(DaleCore.Models.Venta venta, List<DetalleVenta> detallesVenta)
        {
            bool add = false;
            using (Models.DaleDbContext db = new DaleDbContext())
            {
                DaleCore.Models.Venta ventaUpdate = db.Ventas.Find(venta.Id);
                ventaUpdate.Cliente = db.Clientes.Find(venta.Cliente.Id);
                ventaUpdate.ValorTotal = detallesVenta.Sum(s => s.ValorTotal);
                db.Entry<DaleCore.Models.Venta>(ventaUpdate).State = EntityState.Modified;
                db.SaveChanges();
                detallesVenta.ForEach(s => { s.Venta = venta; s.Producto = db.Productos.Where(j => j.Id == s.Producto.Id).FirstOrDefault(); });
                List<DetalleVenta> detalleVentasUpdate = detallesVenta.Where(s => s.Id != 0).ToList();
                List<DetalleVenta> detalleVentasAdd = detallesVenta.Where(s => s.Id == 0).ToList();
                foreach (var item in detalleVentasUpdate)
                {
                    DetalleVenta detalleVenta = db.DetallesVenta.Find(item.Id);
                    detalleVenta.Producto = db.Productos.Where(j => j.Id == item.Producto.Id).FirstOrDefault();
                    detalleVenta.Cantidad = item.Cantidad;
                    detalleVenta.ValorTotal = item.ValorTotal;
                    detalleVenta.ValorUnitario = item.ValorUnitario;
                    db.Entry<DaleCore.Models.DetalleVenta>(detalleVenta).State = EntityState.Modified;
                }
                foreach (var item in detalleVentasAdd)
                {
                    item.Producto = db.Productos.Where(j => j.Id == item.Producto.Id).FirstOrDefault();
                    item.Venta = db.Ventas.Find(venta.Id);
                    db.Entry<DaleCore.Models.DetalleVenta>(item).State = EntityState.Added;
                }

                db.SaveChanges();
                add = venta.Id > 0 ? true : false;
            }
            return add;
        }

        public static bool DeleteVenta(int id)
        {
            bool add = false;
            using (Models.DaleDbContext db = new DaleDbContext())
            {
                DaleCore.Models.Venta venta = db.Ventas.Where(s => s.Id == id).FirstOrDefault();
                List<DaleCore.Models.DetalleVenta> detalleVentas = db.DetallesVenta.Where(s => s.Venta.Id == id).ToList();
                foreach (var item in detalleVentas)
                {
                    db.Entry<DaleCore.Models.DetalleVenta>(item).State = EntityState.Deleted;
                }
                db.SaveChanges();
                db.Entry<DaleCore.Models.Venta>(venta).State = EntityState.Deleted;
                db.SaveChanges();
            }
            return add;
        }


        public static DaleCore.Models.Venta GetVentaBy(int id)
        {
            using (Models.DaleDbContext db = new DaleDbContext())
            {
                return db.Ventas.Where(s => s.Id == id).Include(s=>s.Cliente).FirstOrDefault();
            }
            
        }

        public static bool DeleteDetalleVenta(int id)
        {
            bool add = false;
            using (Models.DaleDbContext db = new DaleDbContext())
            {
                DetalleVenta detalleVenta = db.DetallesVenta.Where(s => s.Id == id).FirstOrDefault();
                db.Entry<DaleCore.Models.DetalleVenta>(detalleVenta).State = EntityState.Deleted;
                db.SaveChanges();
            }
            return add;
        }

        public static List<DetalleVenta> GetDetalleVentaByVenta(int id)
        {
            using (Models.DaleDbContext db = new DaleDbContext())
            {
                return db.DetallesVenta.Where(s => s.Venta.Id == id).Include(s => s.Producto).Include(s => s.Venta).ToList(); ;
            }
        }
    }
}
