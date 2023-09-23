using Botines.Entidades.Dtos.VentaVentaBotin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Entidades.Dtos.Venta
{
    public class VentaDetalleDto
    {
        public VentaListDto venta { get; set; }
        public List<VentaVentaBotinListDto> detalleVenta { get; set; }
    }
}
