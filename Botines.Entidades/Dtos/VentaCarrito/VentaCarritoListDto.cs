using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botines.Entidades.Entidades;

namespace Botines.Entidades.Dtos.VentaCarrito
{
    public class VentaCarritoListDto
    {
        public int VentaCarritoId { get; set; }
        public string NombreBotin { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Total => Cantidad * PrecioVenta;
    }
}
