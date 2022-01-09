using DaleCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dale.Models
{
    public class VentaViewModel
    {
        public int idVenta { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public double ValorTotal { get; set; }
        
        public IEnumerable<SelectListItem> Clientes { get; set; }
        
        public IEnumerable<SelectListItem> Productos { get; set; }
        
        public Producto Producto { get; set; }
        
        public List<DetalleVenta> DetallesVenta { get; set; }
        
        public DetalleVenta DetalleVenta { get; set; }
    }
}