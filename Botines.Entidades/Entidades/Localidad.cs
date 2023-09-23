using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Entidades.Entidades
{
    public class Localidad
    {
        public int LocalidadId { get; set; }
        public string NombreLocalidad { get; set; }
        public int ProvinciaId { get; set; }
        public Provincia Provincia { get; set; }

        //public LocalidadListDto ToLocalidadListDto()
        //{
        //    return new LocalidadListDto()
        //    {
        //        LocalidadId = LocalidadId,
        //        NombreLocalidad = NombreLocalidad,
        //        NombreProvincia = Provincia.NombreProvincia
        //    };
        //}
    }
}
