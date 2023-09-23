using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Botines.Web.ViewModels.VentaCarrito
{
    public class VentaCarritoListVm
    {
        public int VentaCarritoId { get; set; }
        public string Botin { get; set; }

        [DisplayName("P. Unit.")]
        public decimal PrecioVenta { get; set; }
        public int Cantidad { get; set; }
        [DisplayName("P. Total")]
        public decimal Subtotal => Cantidad * PrecioVenta;
    }
}