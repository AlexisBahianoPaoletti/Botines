using Botines.Entidades.Dtos.Venta;
using Botines.Entidades.Dtos.VentaBotin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Entidades.Dtos.VentaVentaBotin
{
    public class VentaVentaBotinListDto
    {
        public int VentaBotinId { get; set; }
        public string Botin { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal => Cantidad * PrecioVenta;
    }
}
