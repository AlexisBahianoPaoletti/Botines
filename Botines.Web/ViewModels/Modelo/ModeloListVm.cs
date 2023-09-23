using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Botines.Web.ViewModels.Modelo
{
    public class ModeloListVm
    {
        public int ModeloId { get; set; }
        [DisplayName("Modelo")]
        public string NombreModelo { get; set; }
        [DisplayName("Marca")]
        public string NombreMarca { get; set; }
    }
}