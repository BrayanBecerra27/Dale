using DaleCore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaleInfraestructure.Models
{
    public class DaleDbContext : DbContext
    {
        public DaleDbContext() :base("Server=localhost;Database=master;Trusted_Connection=True;")
        {

        }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetallesVenta { get; set; }
    }
}
