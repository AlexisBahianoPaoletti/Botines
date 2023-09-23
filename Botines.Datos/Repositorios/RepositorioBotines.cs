using Botines.Datos.Interfaces;
using Botines.Entidades.Dtos.Botin;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Datos.Repositorios
{
    public class RepositorioBotines : IRepositorioBotines
    {
        private readonly BotinesDbContext _context;
        public RepositorioBotines(BotinesDbContext context)
        {
            _context = context;
        }

        public void Agregar(Botin botin)
        {
            try
            {
                _context.Botines.Add(botin);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Borrar(int botinId)
        {
            try
            {
                var botinInDb = GetBotinPorId(botinId);
                if (botinInDb == null)
                {
                    throw new Exception("Registro dado de baja por otro usuario");
                }
                else
                {
                    _context.Entry(botinInDb).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Editar(Botin botin)
        {
            try
            {
                var botinInDb = _context.Botines.SingleOrDefault(c => c.BotinId == botin.BotinId);


                //var botinInDb = GetBotinPorId(botin.BotinId);

                //if (botinInDb == null)
                //{
                //    throw new Exception("Registro dado de baja por otro usuario");
                //}


                botinInDb.Marca = null;
                botinInDb.Modelo = null;
                botinInDb.Proveedor = null;
                botinInDb.NombreBotin = botin.NombreBotin;
               
                botinInDb.MarcaId = botin.MarcaId;
                botinInDb.ModeloId = botin.ModeloId;
                botinInDb.ProveedorId = botin.ProveedorId;

                botinInDb.PrecioCosto = botin.PrecioCosto;
                botinInDb.Imagen = botin.Imagen;
                botinInDb.Suspendido = botin.Suspendido;


                _context.Entry(botinInDb).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EstaRelacionada(Botin botin)
        {
            try
            {
                return _context.TallesBotines.Any(tb => tb.BotinId == botin.BotinId)
                        || _context.VentasBotines.Any(vb => vb.BotinId == botin.BotinId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Botin botin)
        {
            try
            {
                if (botin.BotinId == 0)
                {
                    return _context.Botines.Any(b => b.NombreBotin == botin.NombreBotin
            
                        && b.MarcaId == botin.MarcaId
                        && b.ModeloId == botin.ModeloId
                        && b.PrecioCosto == botin.PrecioCosto);

                }
                return _context.Botines.Any(b => b.NombreBotin == botin.NombreBotin

                        && b.MarcaId == botin.MarcaId
                        && b.ModeloId == botin.ModeloId
                        && b.PrecioCosto == botin.PrecioCosto
                        && b.BotinId != botin.BotinId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<BotinListDto> Filtrar(Func<Botin, bool> predicado, int cantidad, int pagina)
        {
            return _context.Botines.Include(m => m.Marca).Include(m => m.Modelo).Include(p => p.Proveedor)
                 .Where(predicado)
                 .OrderBy(b => b.NombreBotin)
                 .Skip(cantidad * (pagina - 1))
                 .Take(cantidad)
                 .Select(b => new BotinListDto
                 {
                     BotinId = b.BotinId,
                     NombreBotin = b.NombreBotin,
                     Marca = b.Marca.NombreMarca,
                     Modelo = b.Modelo.NombreModelo,
                     PrecioVenta = b.PrecioCosto,
                     Suspendido = b.Suspendido


                 }).ToList();
        }

        public int GetCantidad()
        {
            return _context.Botines.Count();
        }

        public int GetCantidad(Func<Botin, bool> predicado)
        {
            return _context.Botines.Count(predicado);
        }


        public List<BotinListDto> GetBotines()
        {
            return _context.Botines.Include(m => m.Marca).Include(m => m.Modelo).Include(p => p.Proveedor)
                .Select(b => new BotinListDto
                {
                    BotinId = b.BotinId,
                    NombreBotin = b.NombreBotin,
                    Marca = b.Marca.NombreMarca,
                    Modelo = b.Modelo.NombreModelo,
                    PrecioVenta = b.PrecioCosto,
                    Suspendido = b.Suspendido,
                    Imagen = b.Imagen
                    
                }).OrderBy(b=>b.Marca)
                .AsNoTracking()
                .ToList();
        }

        public List<BotinListDto> GetBotinesMarca(int marcaId)
        {
            try
            {
                return _context.Botines.Include(m => m.Marca)
                    .Where(m => m.MarcaId == marcaId)
                    .Select(b => new BotinListDto
                    {
                        BotinId = b.BotinId,
                        NombreBotin = b.NombreBotin,
                        Marca = b.Marca.NombreMarca,
                        Modelo = b.Modelo.NombreModelo,
                        PrecioVenta = b.PrecioCosto,
                        Suspendido = b.Suspendido

                    }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<BotinListDto> GetBotinesPorPagina(int cantidad, int pagina)
        {
            return _context.Botines.Include(m => m.Marca).Include(m => m.Modelo).Include(p => p.Proveedor)
                .OrderBy(b => b.NombreBotin)
                .Skip(cantidad * (pagina - 1))
                .Take(cantidad)
                .Select(b => new BotinListDto
                {
                    BotinId = b.BotinId,
                    NombreBotin = b.NombreBotin,
                    Marca = b.Marca.NombreMarca,
                    Modelo = b.Modelo.NombreModelo,
                    PrecioVenta = b.PrecioCosto,
                    Suspendido = b.Suspendido
                }).ToList();
        }
        public Botin GetBotinPorId(int botinId)
        {
            try
            {
                return _context.Botines.Include(m => m.Marca).Include(m => m.Modelo).Include(p => p.Proveedor)

                    .SingleOrDefault(b => b.BotinId == botinId);

            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
