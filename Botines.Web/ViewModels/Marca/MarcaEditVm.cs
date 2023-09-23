using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Botines.Web.ViewModels.Marca
{
    public class MarcaEditVm
    {
        public int MarcaId { get; set; }

        [DisplayName("Marca")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string NombreMarca { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }
        [DisplayName("Imagen")]
        public HttpPostedFileBase imagenFile { get; set; }
    }
}