using Botines.Entidades.Dtos.Proveedor;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Botines.Datos.Interfaces
{
    public interface IRepositorioProveedores
    {
        List<ProveedorListDto> GetProveedores();
        void Agregar(Proveedor proveedor);
        bool Existe(Proveedor proveedor);
        void Editar(Proveedor proveedor);
        void Borrar(int proveedorId);
        bool EstaRelacionada(Proveedor proveedor);
        Proveedor GetProveedorPorId(int proveedorId);
        List<ProveedorListDto> GetProveedoresPais(int paisId);
        List<ProveedorListDto> GetProveedoresProvincia(int provinciaId);
        List<ProveedorListDto> GetProveedoresLocalidad(int localidadId);
        List<ProveedorListDto> Filtrar(Func<Proveedor, bool> predicado, int cantidad, int pagina);
        int GetCantidad();
        List<ProveedorListDto> GetProveedoresPorPagina(int cantidad, int pagina);
        int GetCantidad(Func<Proveedor, bool> predicado);
        List<SelectListItem> GetProveedoresDropDownList();
    }
}
