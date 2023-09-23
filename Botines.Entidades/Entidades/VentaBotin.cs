using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Entidades.Entidades
{
    public class VentaBotin
    {
        public int VentaBotinId { get; set; }
        public int VentaId { get; set; }
        public int BotinId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Total { get; set; }

        public Botin Botin { get; set; }
    }
}
