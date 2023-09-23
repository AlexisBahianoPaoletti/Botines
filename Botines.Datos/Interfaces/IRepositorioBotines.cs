using Botines.Entidades.Dtos.Botin;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Datos.Interfaces
{
    public interface IRepositorioBotines
    {
        List<BotinListDto> GetBotines();
        void Agregar(Botin botin);
        bool Existe(Botin botin);
        void Editar(Botin botin);
        void Borrar(int botinId);
        bool EstaRelacionada(Botin botin);
        Botin GetBotinPorId(int botinId);
        
        //List<BotinListDto> GetBotinesProveedor(int proveedorId);
        List<BotinListDto> GetBotinesMarca(int marcaId);
        List<BotinListDto> Filtrar(Func<Botin, bool> predicado, int cantidad, int pagina);
        int GetCantidad();
        List<BotinListDto> GetBotinesPorPagina(int cantidad, int pagina);
        int GetCantidad(Func<Botin, bool> predicado);

        //List<SelectListItem> GetBotinesDropDownList();
    }
}
