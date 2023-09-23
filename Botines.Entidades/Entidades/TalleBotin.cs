using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Entidades.Entidades
{
    public class TalleBotin
    {
        public int TalleBotinId { get; set; }
        public int TalleId { get; set; }
        public int BotinId { get; set; }
        public int Stock { get; set; }
        public int UnidadesEnPedido { get; set; }


        public Talle Talle { get; set; }
        public Botin Botin { get; set; }

    }
}
