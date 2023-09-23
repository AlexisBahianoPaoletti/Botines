using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Servicios.Interfaces
{
    public interface IServiciosCarrito
    {
        List<ItemCarrito> GetCarrito(string user);
        void Guardar(ItemCarrito carritoTemp);
        void Borrar(string user, int tallebotinId);
        ItemCarrito GetItem(string user, int tallebotinId);
        int GetCantidad(string user);
    }
}
