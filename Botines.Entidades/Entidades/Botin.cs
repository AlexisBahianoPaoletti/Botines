using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Entidades.Entidades
{
    public class Botin
    {
        public int BotinId { get; set; }
        public string NombreBotin { get; set; }
        public int ModeloId { get; set; }
        public int MarcaId { get; set; }
        public int ProveedorId { get; set; }
        public decimal PrecioCosto { get; set; }
        public string Imagen { get; set; }
        public bool Suspendido { get; set; }

        public Modelo Modelo { get; set; }
        public Marca Marca { get; set; }
        public Proveedor Proveedor { get; set; }


        //public int ProveedorId { get; set; }
        //public Proveedor Proveedor { get; set; }


        //public int UnidadesDisponibles() => Stock - UnidadesEnPedido;
    }
}

