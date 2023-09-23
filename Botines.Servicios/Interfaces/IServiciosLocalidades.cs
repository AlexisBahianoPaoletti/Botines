using Botines.Entidades.Dtos.Localidad;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Botines.Servicios.Interfaces
{
    public interface IServiciosLocalidades
    {
        List<LocalidadListDto> GetLocalidades();
        bool Existe(Localidad localidad);
        void Guardar(Localidad localidad);
        bool EstaRelacionada(Localidad localidad);
        Localidad GetLocalidadPorId(int localidadId);
        void Borrar(int localidadId);
        List<LocalidadListDto> GetLocalidades(int provinciaId);
        List<LocalidadListDto> Filtrar(Func<Localidad, bool> predicado, int cantidad, int pagina);
        int GetCantidad();
        List<LocalidadListDto> GetLocalidadesPorPagina(int cantidad, int pagina);
        int GetCantidad(Func<Localidad, bool> predicado);
        List<SelectListItem> GetLocalidadesDropDownList();
    }
}
