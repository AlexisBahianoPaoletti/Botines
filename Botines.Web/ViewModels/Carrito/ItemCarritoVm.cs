using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Botines.Web.ViewModels.Carrito
{
    public class ItemCarritoVm
    {
        public int ItemCarritoId { get; set; }
        public int TalleBotinId { get; set; }

        [DisplayName("Nombre")]
        public string NombreBotin { get; set; }


        public string Marca { get; set; }

        public string Modelo { get; set; }

        public int Talle { get; set; }
        public int Cantidad { get; set; }

        [DisplayName("P. Unit.")]

        public decimal PrecioVenta { get; set; }

        [DisplayName("P. Total")]
        public decimal PrecioTotal => Cantidad * PrecioVenta;
    }
}