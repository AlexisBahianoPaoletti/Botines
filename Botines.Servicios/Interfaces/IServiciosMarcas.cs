using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Botines.Servicios.Interfaces
{
    public interface IServiciosMarcas
    {
        List<Marca> GetMarcas();
        void Guardar(Marca marca);
        void Borrar(int id);
        bool Existe(Marca marca);
        Marca GetMarcaPorId(int marcaId);
        bool EstaRelacionado(Marca marca);
        List<Marca> GetMarcaPorPagina(int cantidad, int pagina);
        int GetCantidad();
        List<SelectListItem> GetMarcasDropDownList();
    }
}
