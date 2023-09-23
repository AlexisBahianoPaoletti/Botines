using Botines.Entidades.Dtos.Botin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Entidades.Dtos.Talle
{
    public class TalleListDto
    {
        public int TalleId { get; set; }
        [DisplayName("Número")]
        public int NumeroTalle { get; set; }
        public List<BotinListDto> Botines { get; set; }
    }
}
