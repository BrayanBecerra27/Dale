using DaleApi.Models;
using DaleCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DaleApi.Controllers
{
    public class VentaController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetObtenerVentas()
        {
            List<Venta> ventas = new List<Venta>();
            ventas = DaleInfraestructure.Implementations.Venta.GetVentas();
            return Ok(ventas);
        }

        [HttpPost]
        public IHttpActionResult AddVenta(VentaViewModel ventaModel)
        {
            bool agg = false;
            if(ventaModel.idVenta == 0)
            {
                Venta venta = new Venta();
                venta.Cliente = DaleInfraestructure.Implementations.Cliente.GetClienteById(ventaModel.Cliente.Id);
                venta.Fecha = ventaModel.Fecha;
                venta.ValorTotal = ventaModel.ValorTotal;
                List<DetalleVenta> detallesVenta = ventaModel.DetallesVenta;
                agg = DaleInfraestructure.Implementations.Venta.AddVenta(venta, detallesVenta);
            }
            else
            {
                Venta venta = DaleInfraestructure.Implementations.Venta.GetVentaBy(ventaModel.idVenta);
                venta.Cliente = DaleInfraestructure.Implementations.Cliente.GetClienteById(ventaModel.Cliente.Id);
                List<DetalleVenta> detallesVenta = ventaModel.DetallesVenta;
                agg = DaleInfraestructure.Implementations.Venta.UpdateVenta(venta, detallesVenta);
            }
            

            return Ok(agg);
        }

        [HttpDelete]
        public IHttpActionResult EliminarVenta(int id)
        {
            bool delete = false;
            if (id > 0)
            {
                delete = DaleInfraestructure.Implementations.Venta.DeleteVenta(id);
            }
            return Ok(delete);
        }
        [HttpGet]
        [Route("Venta/GetObtenerVenta/{id}")]
        public VentaViewModel GetObtenerVenta(int id)
        {
            VentaViewModel ventaModel = new VentaViewModel();
            if (id > 0)
            {
                Venta venta =  DaleInfraestructure.Implementations.Venta.GetVentaBy(id);
                ventaModel.DetallesVenta  = DaleInfraestructure.Implementations.Venta.GetDetalleVentaByVenta(id);
                ventaModel.Cliente = venta.Cliente;
                ventaModel.Fecha = venta.Fecha;
                ventaModel.ValorTotal = venta.ValorTotal;
                ventaModel.idVenta = venta.Id;
                
            }
            return ventaModel;
        }

        [HttpDelete]
        [Route("Venta/EliminarDetalleVenta/{id}")]
        public IHttpActionResult EliminarDetalleVenta(int id)
        {
            bool delete = false;
            if (id > 0)
            {
                delete = DaleInfraestructure.Implementations.Venta.DeleteDetalleVenta(id);
            }
            return Ok(delete);
        }
    }
}
