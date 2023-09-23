using Botines.Entidades.Dtos.Venta;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Datos.Interfaces
{
    public interface IRepositorioVentas
    {
        List<VentaListDto> GetVentas();
        void Agregar(Venta venta);
        List<VentaListDto> Filtrar(Func<Venta, bool> predicado);
        VentaListDto GetVentaPorId(int id);
        int GetCantidad();
    }
}
