using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Botines.Web.ViewModels.Carrito
{
    public class CheckOutVm
    {
        public CarritoVm Carrito { get; set; }
        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Tarjeta de Crédito")]
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "CVV")]

        public string CVV { get; set; }
        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Exp. Mes")]
        //[Range(1,12,ErrorMessage ="Debe seleccionar un mes")]
        public string Month { get; set; }
        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Exp. Año")]
        //[Range(2022,2032,ErrorMessage ="Debe seleccionar un año")]
        public string Year { get; set; }
        //public List<SelectListItem> Meses { get; set; }
        //public List<SelectListItem> Anios { get; set; } 

    }
}