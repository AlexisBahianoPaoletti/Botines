using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Botines.Web.ViewModels.Botin
{
    public class BotinEditVm
    {
        public int BotinId { get; set; }
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        //[StringLength(50, ErrorMessage = "El campo {0} debe contener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string NombreBotin { get; set; }
        [DisplayName("Modelo")]
        public int ModeloId { get; set; }
        [DisplayName("Marca")]
        public int MarcaId { get; set; }
        [DisplayName("Proveedor")]
        public int ProveedorId { get; set; }
        [DisplayName("Precio")]
        public decimal PrecioCosto { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }
        [DisplayName("Imagen")]
        public HttpPostedFileBase imagenFile { get; set; }


        public bool Suspendido { get; set; }



        public List<SelectListItem> Marcas { get; set; }
        public List<SelectListItem> Modelos { get; set; }
        public List<SelectListItem> Proveedores { get; set; }
    }
}