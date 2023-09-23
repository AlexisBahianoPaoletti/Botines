using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Entidades.Dtos.Proveedor
{
    public class ProveedorListDto
    {
        public int ProveedorId { get; set; }
        public string RazonSocial { get; set; }
        public string NombreLocalidad { get; set; }
        public string NombreProvincia { get; set; }
        public string NombrePais { get; set; }
    }
}
