using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Entidades.Entidades
{
    public class Rol
    {
        public int RolId { get; set; }
        public string Descripcion { get; set; }
        public List<Permiso> Permisos { get; set; }

        public Rol()
        {
            Permisos = new List<Permiso>();
        }
    }
}
