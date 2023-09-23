using Botines.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Entidades.Entidades
{
    public class Venta
    {
        public int VentaId { get; set; }
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        public Estado Estado { get; set; }
        public decimal Total { get; set; }
        public List<VentaCarrito> Detalles { get; set; }

        public Venta()
        {
            Detalles = new List<VentaCarrito>();
        }
        //public decimal GetTotal() => Detalles.Sum(d => d.Cantidad * d.PrecioUnitario);
        public Cliente Cliente { get; set; }

    }
}
