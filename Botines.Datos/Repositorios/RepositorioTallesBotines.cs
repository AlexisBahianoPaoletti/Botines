using Botines.Datos.Interfaces;
using Botines.Entidades.Dtos.Botin;
using Botines.Entidades.Dtos.Cliente;
using Botines.Entidades.Dtos.Talle;
using Botines.Entidades.Dtos.TalleBotin;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Datos.Repositorios
{
    public class RepositorioTallesBotines : IRepositorioTallesBotines
    {
        private readonly BotinesDbContext _context;
        public RepositorioTallesBotines(BotinesDbContext context)
        {
            _context = context;
        }

        public void Agregar(TalleBotin tallebotin)
        {
            try
            {
                _context.TallesBotines.Add(tallebotin);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Borrar(int tallebotinId)
        {
            try
            {
                var tallebotinInDb = GetTalleBotinPorId(tallebotinId);
                if (tallebotinInDb == null)
                {
                    throw new Exception("Registro dado de baja por otro usuario");
                }
                else
                {
                    _context.Entry(tallebotinInDb).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Editar(TalleBotin tallebotin)
        {
            try
            {
                var tallebotinInDb = _context.TallesBotines.SingleOrDefault(tb => tb.TalleBotinId == tallebotin.TalleBotinId);

                //var clienteInDb = GetClientePorId(cliente.ClienteId);

                //if (clienteInDb == null)
                //{
                //    throw new Exception("Registro dado de baja por otro usuario");
                //}

                tallebotinInDb.Talle = null;
                tallebotinInDb.Botin = null;

                tallebotinInDb.TalleId = tallebotin.TalleId;
                tallebotinInDb.BotinId = tallebotin.BotinId;

                _context.Entry(tallebotinInDb).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool EstaRelacionada(TalleBotin tallebotin)
        {
            throw new NotImplementedException();
        }

        public bool Existe(TalleBotin tallebotin)
        {
            try
            {
                if (tallebotin.TalleBotinId == 0)
                {
                    return _context.TallesBotines.Any(tb => tb.Talle == tallebotin.Talle
                        && tb.Botin == tallebotin.Botin);

                }
                return _context.TallesBotines.Any(tb => tb.Talle == tallebotin.Talle
                        && tb.Botin == tallebotin.Botin
                        && tb.TalleBotinId != tallebotin.TalleBotinId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TalleBotinListDto> Filtrar(Func<TalleBotin, bool> predicado, int cantidad, int pagina)
        {
            return _context.TallesBotines.Include(t => t.Talle).Include(b => b.Botin)
                .Where(predicado)
                .OrderBy(tb => tb.TalleId)
                .Skip(cantidad * (pagina - 1))
                .Take(cantidad)
                .Select(tb => new TalleBotinListDto
                {
                    TalleBotinId = tb.TalleBotinId,
                    NumeroTalle = tb.Talle.NumeroTalle,
                    NombreBotin = tb.Botin.NombreBotin,
                }).ToList();
        }

        public List<TalleListDto> GetBotines(int botinId)
        {
            try
            {
                return _context.TallesBotines.Include(t => t.Talle)
                    .Where(b => b.BotinId == botinId)
                    .Select(tb => new TalleListDto
                    {
                        TalleId = tb.TalleId,
                        NumeroTalle = tb.Talle.NumeroTalle

                    }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad()
        {
            return _context.TallesBotines.Count();
        }

        public int GetCantidad(Func<TalleBotin, bool> predicado)
        {
            return _context.TallesBotines.Count(predicado);
        }

        public Botin GetTalleBotinPorId(int tallebotinId)
        {
            //try
            //{
            //    return _context.TallesBotines.Include(t => t.Talle).Include(b => b.Botin)
            //             .SingleOrDefault(tb => tb.TalleBotinId == tallebotinId);

            //}
            //catch (Exception)
            //{

            //    throw;
            //}

            throw new NotImplementedException();
        }

        public List<BotinListDto> GetTalles(int talleId)
        {
            try
            {
                return _context.TallesBotines.Include(b => b.Botin)
                    .Where(t => t.TalleId == talleId)
                    .Select(tb => new BotinListDto
                    {
                        BotinId = tb.BotinId,
                        NombreBotin = tb.Botin.NombreBotin,
                        Marca= tb.Botin.Marca.NombreMarca,
                        Modelo=tb.Botin.Modelo.NombreModelo,
                        PrecioVenta=tb.Botin.PrecioCosto

                    }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TalleBotinListDto> GetTallesBotines()
        {
            return _context.TallesBotines.Include(t => t.Talle).Include(b => b.Botin)
                            .Select(tb => new TalleBotinListDto
                            {
                                TalleBotinId = tb.TalleBotinId,
                                NumeroTalle = tb.Talle.NumeroTalle,
                                NombreBotin = tb.Botin.NombreBotin,
                            }).AsNoTracking()
                            .ToList();
        }

        public List<TalleBotinListDto> GetTallesBotinesPorPagina(int cantidad, int pagina)
        {
            return _context.TallesBotines.Include(t => t.Talle).Include(b => b.Botin)
                .OrderBy(tb => tb.TalleId)
                .Skip(cantidad * (pagina - 1))
                .Take(cantidad)
                .Select(tb => new TalleBotinListDto
                {
                    TalleBotinId = tb.TalleBotinId,
                    NumeroTalle = tb.Talle.NumeroTalle,
                    NombreBotin = tb.Botin.NombreBotin,
                }).ToList();
        }

        TalleBotin IRepositorioTallesBotines.GetTalleBotinPorId(int tallebotinId)
        {
            throw new NotImplementedException();
        }

        TalleBotin IRepositorioTallesBotines.GetTalleBotinPorId2(int botinId,int talleId)
        {
            try
            {
                return _context.TallesBotines.Include(b => b.Botin).Include(t => t.Talle)
                    .SingleOrDefault(b=>b.BotinId==botinId && b.TalleId==talleId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ActualizarStock(int tallebotinId, int cantidad)
        {
            var tallebotinInDb = _context.TallesBotines.SingleOrDefault(b => b.TalleBotinId == tallebotinId);
            tallebotinInDb.UnidadesEnPedido -= cantidad;
            tallebotinInDb.Stock -= cantidad;
            _context.Entry(tallebotinInDb).State = EntityState.Modified;
        }

        public int GetCantidadStock()
        {
            try
            {
                return _context.TallesBotines.Sum(tb => tb.Stock);
                    

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
