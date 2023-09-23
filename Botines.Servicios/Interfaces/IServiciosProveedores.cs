using Botines.Entidades.Dtos.Proveedor;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Botines.Servicios.Interfaces
{
    public interface IServiciosProveedores
    {
        List<ProveedorListDto> GetProveedores();
        bool Existe(Proveedor proveedor);
        void Guardar(Proveedor proveedor);
        bool EstaRelacionada(Proveedor proveedor);
        Proveedor GetProveedorPorId(int proveedorId);
        void Borrar(int proveedorId);
        List<ProveedorListDto> GetProveedoresPais(int paisId);
        List<ProveedorListDto> GetProveedoresProvincia(int provinciaId);
        List<ProveedorListDto> GetProveedoresLocalidad(int LocalidadId);
        List<ProveedorListDto> Filtrar(Func<Proveedor, bool> predicado, int cantidad, int pagina);
        int GetCantidad();
        List<ProveedorListDto> GetProveedoresPorPagina(int cantidad, int pagina);
        int GetCantidad(Func<Proveedor, bool> predicado);
        List<SelectListItem> GetProveedoresDropDownList();
    }
}
