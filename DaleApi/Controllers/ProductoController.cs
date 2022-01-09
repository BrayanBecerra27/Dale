using DaleCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DaleApi.Controllers
{
    public class ProductoController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetObtenerProductos()
        {
            List<Producto> clientes = new List<Producto>();         
            clientes = DaleInfraestructure.Implementations.Producto.GetProductos();
            return Ok(clientes);
        }

        [HttpPost]
        public IHttpActionResult AddProducto(Producto producto)
        {
            bool add = false;
            add = DaleInfraestructure.Implementations.Producto.AddProductos(producto);
            return Ok(add);
        }

        [HttpPut]
        public IHttpActionResult UpdateProducto(Producto producto)
        {
            bool update = false;
            if (producto.Id > 0)
            {

                update = DaleInfraestructure.Implementations.Producto.UpdateProductos(producto);
            }
            return Ok(update);
        }
        [HttpDelete]
        public IHttpActionResult DeleteProducto(Producto producto)
        {
            bool delete = false;
            if (producto.Id > 0)
            {
                delete = DaleInfraestructure.Implementations.Producto.DeleteProductos(producto);
            }
            return Ok(delete);
        }

    }
}
