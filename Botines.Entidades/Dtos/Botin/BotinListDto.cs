using Botines.Entidades.Dtos.Talle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Entidades.Dtos.Botin
{
    public class BotinListDto
    {
        public int BotinId { get; set; }
        [DisplayName("Nombre")]
        public string NombreBotin { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }

        [DisplayName("Precio")]
        public decimal PrecioVenta { get; set; }
        public bool Suspendido { get; set; }
        public string Imagen { get; set; }


        public List<TalleListDto> Talles { get; set; }
    }
}
