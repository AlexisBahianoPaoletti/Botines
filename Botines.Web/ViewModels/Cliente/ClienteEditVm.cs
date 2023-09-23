using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Botines.Web.ViewModels.Cliente
{
    public class ClienteEditVm
    {
        public int ClienteId { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Nombre { get; set; }

        [DisplayName("Apellido")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Apellido { get; set; }

        [DisplayName("Dirección")]
        public string Direccion { get; set; }

        [DisplayName("Localidad")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una localidad")]
        public int LocalidadId { get; set; }

        [DisplayName("Provincia")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una provincia")]
        public int ProvinciaId { get; set; }

        [DisplayName("País")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un país")]
        public int PaisId { get; set; }

        [DisplayName("Teléfono fijo")]
        public string TelefonoFijo { get; set; }

        [DisplayName("Teléfono móvil")]
        public string TelefonoMovil { get; set; }


        [DisplayName("Correo electrónico")]
        [MaxLength(50,ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [DataType(DataType.EmailAddress)]
        public string CorreoElectronico { get; set; }


        public List<SelectListItem> Paises { get; set; }
        public List<SelectListItem> Provincias { get; set; }
        public List<SelectListItem> Localidades { get; set; }
    }
}