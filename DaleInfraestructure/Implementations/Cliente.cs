using DaleCore.Interfaces;
using DaleCore.Models;
using DaleInfraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DaleInfraestructure.Implementations
{
    public class Cliente 
    {
        private static DaleDbContext _dbContext = new DaleDbContext();
        public Cliente(DaleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public static List<DaleCore.Models.Cliente> GetClientes()
        {
            List<DaleCore.Models.Cliente> clientes = new List<DaleCore.Models.Cliente>();
            
            using(Models.DaleDbContext db = new DaleDbContext())
            {
                IQueryable<DaleCore.Models.Cliente> custQuery =
                    from cust in db.Clientes
                    select cust;
                clientes = custQuery.ToList();

            }
            return clientes;
        }

        public static DaleCore.Models.Cliente GetClienteById(int id)
        {
            DaleCore.Models.Cliente cliente = new DaleCore.Models.Cliente();

            using (Models.DaleDbContext db = new DaleDbContext())
            {
                IQueryable<DaleCore.Models.Cliente> custQuery =
                    from cust in db.Clientes
                    where cust.Id == id
                    select cust;
                cliente = custQuery.AsNoTracking().FirstOrDefault();

            }
            return cliente;
        }

        public static bool AddClientes(DaleCore.Models.Cliente cliente)
        {
            bool add = false;
            using (Models.DaleDbContext db = new DaleDbContext())
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                add = cliente.Id > 0 ? true : false;
            }
            return add;
        }

        public static bool UpdateClientes(DaleCore.Models.Cliente cliente)
        {
            bool add = false;
            using (Models.DaleDbContext db = new DaleDbContext())
            {

                db.Entry<DaleCore.Models.Cliente>(cliente).State = EntityState.Modified;
                db.SaveChanges();
                add = true;
            }
            return add;
        }

        public static bool DeleteClientes(DaleCore.Models.Cliente cliente)
        {
            bool add = false;
            using (Models.DaleDbContext db = new DaleDbContext())
            {
                db.Entry<DaleCore.Models.Cliente>(cliente).State = EntityState.Deleted;
                db.SaveChanges();
                add = true;
            }
            return add;
        }
    }
}
