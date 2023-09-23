using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Botines.Web.ViewModels.Marca
{
    public class MarcaListVm
    {
        public int MarcaId { get; set; }

        [DisplayName("Marca")]
        public string NombreMarca { get; set; }
        [DisplayName("Imágen")]
        public string Imagen { get; set; }
        public int CantidadModelos { get; set; }
    }
}