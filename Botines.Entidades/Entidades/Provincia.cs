using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Entidades.Entidades
{
    public class Provincia
    {
        public int ProvinciaId { get; set; }
        public string NombreProvincia { get; set; }
        public int PaisId { get; set; }
        public Pais Pais { get; set; }

        //public ProvinciaListDto ToProvinciaListDto()
        //{
        //    return new ProvinciaListDto()
        //    {
        //        ProvinciaId = ProvinciaId,
        //        NombreProvincia = NombreProvincia,
        //        NombrePais = Pais.NombrePais
        //    };
        //}
    }
}
