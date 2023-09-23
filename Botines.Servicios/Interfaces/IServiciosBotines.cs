using Botines.Entidades.Dtos.Botin;
using Botines.Entidades.Dtos.Talle;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Servicios.Interfaces
{
    public interface IServiciosBotines
    {
        List<BotinListDto> GetBotines();
        bool Existe(Botin botin);
        void Guardar(Botin botin);
        bool EstaRelacionada(Botin botin);
        Botin GetBotinPorId(int botinId);
        void Borrar(int botinId);
        List<BotinListDto> GetBotinesMarca(int marcaId);
        List<BotinListDto> Filtrar(Func<Botin, bool> predicado, int cantidad, int pagina);
        int GetCantidad();
        List<BotinListDto> GetBotinesPorPagina(int cantidad, int pagina);
        int GetCantidad(Func<Botin, bool> predicado);

        List<BotinListDto> GetTallesBotines();
        //List<SelectListItem> GetBotinesDropDownList();
    }
}
