using Botines.Entidades.Dtos.Provincia;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Botines.Servicios.Interfaces
{
    public interface IServiciosProvincias
    {
        List<ProvinciaListDto> GetProvincias();
        bool Existe(Provincia provincia);
        void Guardar(Provincia provincia);
        bool EstaRelacionada(Provincia provincia);
        Provincia GetProvinciaPorId(int provinciaId);
        void Borrar(int provinciaId);
        List<ProvinciaListDto> GetProvincias(int paisId);
        List<ProvinciaListDto> Filtrar(Func<Provincia, bool> predicado, int cantidad, int pagina);
        int GetCantidad();
        List<ProvinciaListDto> GetProvinciasPorPagina(int cantidad, int pagina);
        int GetCantidad(Func<Provincia, bool> predicado);
        List<SelectListItem> GetProvinciasDropDownList();
    }
}
