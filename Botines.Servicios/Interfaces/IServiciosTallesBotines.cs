using Botines.Entidades.Dtos.Cliente;
using Botines.Entidades.Dtos.Talle;
using Botines.Entidades.Dtos.TalleBotin;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Servicios.Interfaces
{
    public interface IServiciosTallesBotines
    {
        List<TalleBotinListDto> GetTallesBotines();
        bool Existe(TalleBotin tallebotin);
        void Guardar(TalleBotin tallebotin);
        bool EstaRelacionada(TalleBotin tallebotin);
        TalleBotin GetTalleBotinPorId(int tallebotinId);
        TalleBotin GetTalleBotinPorId2(int botinId,int talleId);
        void Borrar(int tallebotinId);
        //List<TalleBotinListDto> GetTallesBotines(int tallebotinId);
        List<TalleBotinListDto> GetTalles(int talleId);
        List<TalleListDto> GetBotines(int botinId);
        List<TalleBotinListDto> Filtrar(Func<TalleBotin, bool> predicado, int cantidad, int pagina);
        int GetCantidad();
        List<TalleBotinListDto> GetTallesBotinesPorPagina(int cantidad, int pagina);
        int GetCantidad(Func<TalleBotin, bool> predicado);
        int GetCantidadStock();
    }
}
