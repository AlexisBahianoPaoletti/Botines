using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Entidades.Entidades
{
    public class Proveedor
    {
        public int ProveedorId { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public int LocalidadId { get; set; }
        public int ProvinciaId { get; set; }
        public int PaisId { get; set; }
        public string TelefonoFijo { get; set; }
        public string TelefonoMovil { get; set; }
        public string CorreoElectronico { get; set; }

        public Localidad Localidad { get; set; }
        public Provincia Provincia { get; set; }
        public Pais Pais { get; set; }

        //public Provincia Provincia { get; set; }
        //public Pais Pais { get; set; }


        //public ProveedorListDto ToProveedorListDto()
        //{
        //    return new ProveedorListDto
        //    {
        //        ProveedorId = ProveedorId,
        //        RazonSocial = RazonSocial,
        //        Localidad = Localidad.NombreLocalidad

        //    };
        //}
    }
}
