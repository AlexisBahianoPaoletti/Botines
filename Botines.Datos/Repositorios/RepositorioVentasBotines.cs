using Botines.Datos.Interfaces;
using Botines.Entidades.Dtos.VentaBotin;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Datos.Repositorios
{
    public class RepositorioVentasBotines : IRepositorioVentasBotines
    {
        private readonly BotinesDbContext _context;
        public RepositorioVentasBotines(BotinesDbContext context)
        {
            _context = context;
        }
        public void Agregar(VentaBotin ventabotin)
        {
            _context.VentasBotines.Add(ventabotin);
        }

        public void Borrar(int id)
        {
            throw new NotImplementedException();
        }

        public void Editar(VentaBotin ventabotin)
        {
            throw new NotImplementedException();
        }

        public bool EstaRelacionado(VentaBotin ventabotin)
        {
            throw new NotImplementedException();
        }

        public bool Existe(VentaBotin ventabotin)
        {
            throw new NotImplementedException();
        }

        public VentaBotin GetVentaBotinPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<VentaBotinListDto> GetVentasBotines(int ventaId)
        {
            return _context.VentasBotines.Include(b => b.Botin)
                .Where(v => v.VentaId == ventaId)
                .Select(vb => new VentaBotinListDto()
                {
                    VentaBotinId = vb.VentaBotinId,
                    Botin = vb.Botin.NombreBotin,
                    Cantidad = vb.Cantidad,
                    PrecioVenta = vb.PrecioVenta,

                }).ToList();
        }
    }
}
