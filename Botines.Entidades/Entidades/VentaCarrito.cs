using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Entidades.Entidades
{
    public class VentaCarrito
    {
        public int VentaCarritoId { get; set; }
        public int VentaId { get; set; }
        public int ItemCarritoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Total { get; set; }

        public ItemCarrito ItemCarrito { get; set; }
    }
}
