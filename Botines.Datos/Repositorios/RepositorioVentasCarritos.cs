using Botines.Datos.Interfaces;
using Botines.Entidades.Dtos.VentaBotin;
using Botines.Entidades.Dtos.VentaCarrito;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Datos.Repositorios
{
    public class RepositorioVentasCarritos:IRepositorioVentasCarritos
    {
        private readonly BotinesDbContext _context;
        public RepositorioVentasCarritos(BotinesDbContext context)
        {
            _context = context;   
        }

        public void Agregar(VentaCarrito ventacarrito)
        {
            _context.VentasCarritos.Add(ventacarrito);
        }

        public void Borrar(int id)
        {
            throw new NotImplementedException();
        }

        public void Editar(VentaCarrito ventacarrito)
        {
            throw new NotImplementedException();
        }

        public bool EstaRelacionado(VentaCarrito ventacarrito)
        {
            throw new NotImplementedException();
        }

        public bool Existe(VentaCarrito ventacarrito)
        {
            throw new NotImplementedException();
        }

        public VentaCarrito GetVentaCarritoPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<VentaCarritoListDto> GetVentasCarritos(int ventaId)
        {
            return _context.VentasCarritos.Include(v => v.ItemCarrito)
                     .Where(v => v.VentaId == ventaId)
                    .Select(vc => new VentaCarritoListDto()
                    {
                     VentaCarritoId = vc.VentaCarritoId,
                     NombreBotin = vc.ItemCarrito.NombreBotin,
                     Cantidad = vc.Cantidad,
                     PrecioVenta = vc.PrecioVenta
                    }).ToList();
        }
    }
}
