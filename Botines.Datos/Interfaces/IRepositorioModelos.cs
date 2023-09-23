using Botines.Entidades.Dtos.Modelo;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Botines.Datos.Interfaces
{
    public interface IRepositorioModelos
    {
        List<ModeloListDto> GetModelos();
        void Agregar(Modelo modelo);
        bool Existe(Modelo modelo);
        void Editar(Modelo modelo);
        void Borrar(int modeloId);
        bool EstaRelacionada(Modelo modelo);
        Modelo GetModeloPorId(int modeloId);
        List<ModeloListDto> GetModelos(int paisId);
        List<ModeloListDto> Filtrar(Func<Modelo, bool> predicado, int cantidad, int pagina);
        int GetCantidad();
        List<ModeloListDto> GetModelosPorPagina(int cantidad, int pagina);
        int GetCantidad(Func<Modelo, bool> predicado);
        List<SelectListItem> GetModelosDropDownList();
    }
}
