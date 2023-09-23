using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Botines.Web.ViewModels.Venta
{
    public class VentaListVm
    {
        [DisplayName("Vta. Nro.")]
        public int VentaId { get; set; }
        [DisplayName("Fec. Vta.")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
    }
}