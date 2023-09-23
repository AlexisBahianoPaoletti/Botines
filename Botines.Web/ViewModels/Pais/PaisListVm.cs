using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Botines.Web.ViewModels.Pais
{
    public class PaisListVm
    {
        public int PaisId { get; set; }

        [DisplayName("País")]
        public string NombrePais { get; set; }
    }
}