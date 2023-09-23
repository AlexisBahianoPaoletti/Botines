
using Botines.Entidades.Dtos.Localidad;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Botines.Datos.Interfaces
{
    public interface IRepositorioLocalidades
    {
        List<LocalidadListDto> GetLocalidades();
        void Agregar(Localidad localidad);
        bool Existe(Localidad localidad);
        void Editar(Localidad localidad);
        void Borrar(int localidadId);
        bool EstaRelacionada(Localidad localidad);
        Localidad GetLocalidadPorId(int localidadId);
        List<LocalidadListDto> GetLocalidades(int provinciaId);
        List<LocalidadListDto> Filtrar(Func<Localidad, bool> predicado, int cantidad, int pagina);
        int GetCantidad();
        List<LocalidadListDto> GetLocalidadesPorPagina(int cantidad, int pagina);
        int GetCantidad(Func<Localidad, bool> predicado);
        List<SelectListItem> GetLocalidadesDropDownList();
    }
}
