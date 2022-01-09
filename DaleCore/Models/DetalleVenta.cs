using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaleCore.Models
{
    [Table("DETALLEVENTA")]
    public class DetalleVenta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Venta Venta { get; set; }
        public Producto Producto { get; set; }
        public double Cantidad { get; set; }
        public double ValorUnitario { get; set; }
        public double ValorTotal { get; set; }
        [NotMapped]
        public int IdTemporal { get; set; }
    }
}
