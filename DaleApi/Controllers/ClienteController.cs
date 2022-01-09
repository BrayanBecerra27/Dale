using DaleCore.Interfaces;
using DaleCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DaleApi.Controllers
{
    public class ClienteController : ApiController
    {
        
        // [HttpGet("GetObtenerClientes")]
        
        [HttpGet]
        public IHttpActionResult GetObtenerClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            //clientes = _iCliente.GetClientes();
            clientes = DaleInfraestructure.Implementations.Cliente.GetClientes();
            return Ok(clientes);
        }

        [HttpPost]
        public IHttpActionResult AddCliente(Cliente cliente)
        {
            bool add = false;
            add = DaleInfraestructure.Implementations.Cliente.AddClientes(cliente);
            return Ok(add);
        }

        [HttpPut]
        public IHttpActionResult UpdateCliente(Cliente cliente)
        {
            bool update = false;
            if(cliente.Id > 0)
            {

                update = DaleInfraestructure.Implementations.Cliente.UpdateClientes(cliente);
            }
            return Ok(update);
        }
        [HttpDelete]
        public IHttpActionResult DeleteCliente(Cliente cliente)
        {
            bool delete = false;
            if(cliente.Id > 0)
            {
                delete = DaleInfraestructure.Implementations.Cliente.DeleteClientes(cliente);
            }
            return Ok(delete);
        }

    }
}
