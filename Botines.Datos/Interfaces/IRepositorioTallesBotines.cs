using Botines.Entidades.Dtos.Botin;
using Botines.Entidades.Dtos.Talle;
using Botines.Entidades.Dtos.TalleBotin;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Datos.Interfaces
{
    public interface IRepositorioTallesBotines
    {
        List<TalleBotinListDto> GetTallesBotines();
        void Agregar(TalleBotin tallebotin);
        bool Existe(TalleBotin tallebotin);
        void Editar(TalleBotin tallebotin);
        void Borrar(int tallebotinId);
        bool EstaRelacionada(TalleBotin tallebotin);
        TalleBotin GetTalleBotinPorId(int tallebotinId);
        TalleBotin GetTalleBotinPorId2(int botinId, int talleId);

        //List<BotinListDto> GetBotinesProveedor(int proveedorId);
        List<BotinListDto> GetTalles(int talleId);
        List<TalleListDto> GetBotines(int botinId);
        List<TalleBotinListDto> Filtrar(Func<TalleBotin, bool> predicado, int cantidad, int pagina);
        int GetCantidad();
        List<TalleBotinListDto> GetTallesBotinesPorPagina(int cantidad, int pagina);
        int GetCantidad(Func<TalleBotin, bool> predicado);

        void ActualizarStock(int tallebotinId, int cantidad);
        int GetCantidadStock();
    }
}
