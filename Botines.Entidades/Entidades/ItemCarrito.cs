using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Entidades.Entidades
{
    public class ItemCarrito
    {
        public int ItemCarritoId { get; set; }
        public int TalleBotinId { get; set; }
        public string UserName { get; set; }
        public string NombreBotin { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public bool Estado { get; set; }
        public decimal PrecioTotal => Cantidad * PrecioUnitario;

        public TalleBotin TalleBotin { get; set; }
    }
}
