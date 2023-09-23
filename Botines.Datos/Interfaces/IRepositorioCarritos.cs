using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Datos.Interfaces
{
    public interface IRepositorioCarritos
    {
        List<ItemCarrito> GetCarrito(string user);
        void Guardar(ItemCarrito carritoTemp);
        ItemCarrito GetItem(string user, int productoId);
        int GetCantidad(string user);
        void Borrar(string user, int tallebotinId);
        void EditarCarrito(int itemCarritoId);
    }
}
