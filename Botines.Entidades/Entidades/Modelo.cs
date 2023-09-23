using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Entidades.Entidades
{
    public class Modelo
    {
        public int ModeloId { get; set; }
        public string NombreModelo { get; set; }
        public int MarcaId { get; set; }
        public Marca Marca { get; set; }
    }
}
