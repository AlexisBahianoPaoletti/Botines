using Botines.Entidades.Dtos.Talle;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Botines.Servicios.Interfaces
{
    public interface IServiciosTalles
    {
        List<Talle> GetTalles();
        void Guardar(Talle talle);
        void Borrar(int id);
        bool Existe(Talle talle);
        Talle GetTallePorId(int talleId);
        bool EstaRelacionado(Talle talle);
        List<Talle> GetTallePorPagina(int cantidad, int pagina);
        int GetCantidad();
        List<SelectListItem> GetTallesDropDownList();

        List<TalleListDto> GetTallesBotines();

    }
}
