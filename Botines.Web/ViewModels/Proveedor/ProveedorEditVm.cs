using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Botines.Web.ViewModels.Proveedor
{
    public class ProveedorEditVm
    {
        public int ProveedorId { get; set; }

        [DisplayName("Razon Social")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string RazonSocial { get; set; }

        [DisplayName("Dirección")]
        public string Direccion { get; set; }

        [DisplayName("Localidad")]
        //public string NombreLocalidad { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una localidad")]
        public int LocalidadId { get; set; }
        [DisplayName("Provincia")]
        //public string NombreProvincia { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una provincia")]
        public int ProvinciaId { get; set; }
        [DisplayName("País")]
        //public string NombrePais { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un país")]
        public int PaisId { get; set; }
        [DisplayName("Teléfono fijo")]
        public string TelefonoFijo { get; set; }

        [DisplayName("Teléfono móvil")]
        public string TelefonoMovil { get; set; }

        [DisplayName("Correo electrónico")]
        public string CorreoElectronico { get; set; }

        public List<SelectListItem> Paises { get; set; }
        public List<SelectListItem> Provincias { get; set; }
        public List<SelectListItem> Localidades { get; set; }
    }
}