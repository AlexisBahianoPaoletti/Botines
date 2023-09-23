using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Botines.Web.ViewModels.Botin
{
    public class BotinListVm
    {
        public int BotinId { get; set; }

        [DisplayName("Nombre")]
        public string NombreBotin { get; set; }

        [DisplayName("Marca")]
        public string Marca { get; set; }

        [DisplayName("Modelo")]
        public string Modelo { get; set; }

        //public int Stock { get; set; }
        [DisplayName("Precio")]
        public decimal PrecioVenta { get; set; }
        public bool Suspendido { get; set; }
        [DisplayName("Imágen")]
        public string Imagen { get; set; }
    }
}