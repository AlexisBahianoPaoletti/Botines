using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Botines.Web.ViewModels.Proveedor
{
    public class ProveedorListVm
    {
        public int ProveedorId { get; set; }
        [DisplayName("Razon Social")]
        public string RazonSocial { get; set; }
        [DisplayName("Localidad")]
        public string NombreLocalidad { get; set; }
        [DisplayName("Provincia")]
        public string NombreProvincia { get; set; }
        [DisplayName("País")]
        public string NombrePais { get; set; }
    }
}