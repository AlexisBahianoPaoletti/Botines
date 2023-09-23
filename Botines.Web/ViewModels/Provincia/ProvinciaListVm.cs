using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Botines.Web.ViewModels.Provincia
{
    public class ProvinciaListVm
    {
        public int ProvinciaId { get; set; }
        [DisplayName("Provincia")]
        public string NombreProvincia { get; set; }
        [DisplayName("País")]
        public string NombrePais { get; set; }
    }
}