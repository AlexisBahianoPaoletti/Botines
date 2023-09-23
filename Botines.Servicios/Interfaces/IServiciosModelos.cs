using Botines.Entidades.Dtos.Modelo;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Botines.Servicios.Interfaces
{
    public interface IServiciosModelos
    {
        List<ModeloListDto> GetModelos();
        bool Existe(Modelo modelo);
        void Guardar(Modelo modelo);
        bool EstaRelacionada(Modelo modelo);
        Modelo GetModeloPorId(int modeloId);
        void Borrar(int modeloId);
        List<ModeloListDto> GetModelos(int marcaId);
        List<ModeloListDto> Filtrar(Func<Modelo, bool> predicado, int cantidad, int pagina);
        int GetCantidad();
        List<ModeloListDto> GetModelosPorPagina(int cantidad, int pagina);
        int GetCantidad(Func<Modelo, bool> predicado);
        List<SelectListItem> GetModelosDropDownList();
    }
}
