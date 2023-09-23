using Botines.Entidades.Dtos.Venta;
using Botines.Entidades.Dtos.VentaBotin;
using Botines.Entidades.Dtos.VentaCarrito;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Servicios.Interfaces
{
    public interface IServiciosVentas
    {
        int GetCantidad();
        List<VentaCarritoListDto> GetVentaCarrito(int ventaId);
        List<VentaListDto> GetVentas();
        VentaListDto GetVentasPorId(int value);
        void Guardar(Venta venta);
    }
}
