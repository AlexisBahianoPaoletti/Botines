using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Botines.Datos.Interfaces
{
    public interface IRepositorioTalles
    {
        List<Talle> GetTalles();
        void Agregar(Talle talle);
        void Editar(Talle talle);
        void Borrar(int id);
        bool Existe(Talle talle);
        Talle GetTallePorId(int talleId);
        bool EstaRelacionado(Talle talle);
        List<Talle> GetTallesPorPagina(int cantidad, int pagina);
        int GetCantidad();
        List<SelectListItem> GetTallesDropDownList();
    }
}
