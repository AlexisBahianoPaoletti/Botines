using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Entidades.Dtos.VentaBotin
{
    public class VentaBotinListDto
    {
        public int VentaBotinId { get; set; }
        public string Botin { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Total => Cantidad * PrecioVenta;
    }
}
