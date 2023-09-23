using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Botines.Web.ViewModels.Modelo
{
    public class ModeloEditVm
    {
        public int ModeloId { get; set; }

        [DisplayName("Modelo")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string NombreModelo { get; set; }

        [DisplayName("Marca")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una marca")]
        public int MarcaId { get; set; }
        public List<SelectListItem> Marcas { get; set; }
    }
}