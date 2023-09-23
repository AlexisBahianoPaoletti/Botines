using Botines.Web.ViewModels.VentaBotin;
using Botines.Web.ViewModels.VentaCarrito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Botines.Web.ViewModels.Venta
{
    public class VentaDetailVm
    {
        public VentaListVm Venta { get; set; }
        public List<VentaCarritoListVm> Detalles { get; set; }
    }
}