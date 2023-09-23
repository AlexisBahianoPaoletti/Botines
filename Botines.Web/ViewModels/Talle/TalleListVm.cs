using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Botines.Web.ViewModels.Talle
{
    public class TalleListVm
    {
        public int TalleId { get; set; }

        [DisplayName("Talle")]
        public int NumeroTalle { get; set; }
        [DisplayName("Cantidad")]
        public int Cantidad { get; set; }
    }
}