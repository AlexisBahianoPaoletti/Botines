using Botines.Datos.Interfaces;
using Botines.Entidades.Dtos.Venta;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Datos.Repositorios
{
    public class RepositorioVentas: IRepositorioVentas
    {
        private readonly BotinesDbContext _context;

        public RepositorioVentas(BotinesDbContext context)
        {
            _context = context;
        }

        public void Agregar(Venta venta)
        {
            _context.Ventas.Add(venta);
        }

        public List<VentaListDto> Filtrar(Func<Venta, bool> predicado)
        {
            throw new NotImplementedException();
        }

        public int GetCantidad()
        {
            return _context.Ventas.Count();
        }

        public VentaListDto GetVentaPorId(int id)
        {
            return _context.Ventas.Include(v => v.Cliente)
                .Select(v => new VentaListDto
                {
                    VentaId = v.VentaId,
                    Fecha = v.Fecha,
                    Cliente = v.Cliente.Nombre,
                    Total = v.Total,
                    Estado = v.Estado.ToString()
                })
                .SingleOrDefault(v => v.VentaId == id);

        }

        public List<VentaListDto> GetVentas()
        {
            return _context.Ventas
                .Include(v => v.Cliente)
                .OrderBy(v => v.Fecha)
                .Select(v => new VentaListDto
                {
                    VentaId = v.VentaId,
                    Fecha = v.Fecha,
                    Cliente = v.Cliente.Nombre,
                    Total = v.Total,
                    Estado = v.Estado.ToString()
                }).ToList();
        }
    }
}
