using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Botines.Web.ViewModels.Cliente
{
    public class ClienteListVm
    {
        public int ClienteId { get; set; }
        [DisplayName("Nombre")]
        public string Nombre { get; set; }
        [DisplayName("Apellido")]
        public string Apellido { get; set; }
        [DisplayName("Localidad")]
        public string Localidad { get; set; }
        [DisplayName("Provincia")]
        public string Provincia { get; set; }
        [DisplayName("País")]
        public string Pais { get; set; }
    }
}