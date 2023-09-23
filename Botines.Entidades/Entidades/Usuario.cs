using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Entidades.Entidades
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public int RolId { get; set; }
        public bool Activo { get; set; }

        public Rol Rol { get; set; }

    }
}
