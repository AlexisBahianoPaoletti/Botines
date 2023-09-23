using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Botines.Datos.Interfaces
{
    public interface IRepositorioMarcas
    {
        List<Marca> GetMarcas();
        void Agregar(Marca marca);
        void Editar(Marca marca);
        void Borrar(int id);
        bool Existe(Marca marca);
        Marca GetMarcaPorId(int marcaId);
        bool EstaRelacionado(Marca marca);
        List<Marca> GetMarcasPorPagina(int cantidad, int pagina);
        int GetCantidad();
        List<SelectListItem> GetMarcasDropDownList();
    }
}
