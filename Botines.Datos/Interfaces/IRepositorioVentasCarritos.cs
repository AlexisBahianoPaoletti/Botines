using Botines.Entidades.Dtos.VentaBotin;
using Botines.Entidades.Dtos.VentaCarrito;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Datos.Interfaces
{
    public interface IRepositorioVentasCarritos
    {
        void Agregar(VentaCarrito ventacarrito);
        void Borrar(int id);
        void Editar(VentaCarrito ventacarrito);
        bool EstaRelacionado(VentaCarrito ventacarrito);
        bool Existe(VentaCarrito ventacarrito);
        VentaCarrito GetVentaCarritoPorId(int id);
        List<VentaCarritoListDto> GetVentasCarritos(int ventaId);
    }
}
