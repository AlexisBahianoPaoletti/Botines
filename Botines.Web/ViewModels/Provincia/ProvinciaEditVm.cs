using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Botines.Web.ViewModels.Provincia
{
    public class ProvinciaEditVm
    {
        public int ProvinciaId { get; set; }
        
        [DisplayName("Provincia")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string NombreProvincia { get; set; }
        
        [DisplayName("País")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un país")]
        public int PaisId { get; set; }
        public List<SelectListItem> Paises { get; set; }
    }
}