using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Botines.Web.ViewModels.Talle
{
    public class TalleEditVm
    {
        public int TalleId { get; set; }

        [DisplayName("Talle")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        //[StringLength(50, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        public int NumeroTalle { get; set; }
    }
}