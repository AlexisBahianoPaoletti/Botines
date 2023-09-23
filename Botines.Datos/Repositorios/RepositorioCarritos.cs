using Botines.Datos.Interfaces;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Botines.Datos.Repositorios
{
    public class RepositorioCarritos : IRepositorioCarritos
    {
        private readonly BotinesDbContext _context;
        public RepositorioCarritos(BotinesDbContext context)
        {
            _context = context;
        }
        public void Borrar(string user, int tallebotinId)
        {
            var itemInDb = GetItem(user, tallebotinId);
            if (itemInDb != null)
            {
                _context.Entry(itemInDb).State = EntityState.Deleted;
            }
        }

        public void EditarCarrito(int itemCarritoId)
        {
            var iC = GetItemCarritoPorId(itemCarritoId);
            iC.Estado = true;
            _context.Entry(iC).State = EntityState.Modified;
        }
        public ItemCarrito GetItemCarritoPorId(int itemCarritoId)
        {
            try
            {
                return _context.Carrito

                    .SingleOrDefault(b => b.ItemCarritoId == itemCarritoId);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public int GetCantidad(string user)
        {
            return _context.Carrito.Count(c => c.UserName == user);
        }

        public List<ItemCarrito> GetCarrito(string user)
        {
            return _context.Carrito.Include(tb=>tb.TalleBotin).Include(t=>t.TalleBotin.Talle).Include(b=>b.TalleBotin.Botin)
               .Where(c => c.UserName == user && c.Estado==false)
               .ToList();
        }

        public ItemCarrito GetItem(string user, int tallebotinId)
        {
            return _context.Carrito.Include(tb => tb.TalleBotin)
                        .SingleOrDefault(i => i.UserName == user && i.TalleBotinId == tallebotinId && i.Estado==false);

        }

        public void Guardar(ItemCarrito carritoTemp)
        {

            var itemInDb = GetItem(carritoTemp.UserName,
                carritoTemp.TalleBotinId);

            if (itemInDb != null)
            {
                itemInDb.Cantidad += carritoTemp.Cantidad;
                _context.Entry(itemInDb).State = EntityState.Modified;

            }
            else
            {
                _context.Carrito.Add(carritoTemp);

            }
        }

    } 
}
