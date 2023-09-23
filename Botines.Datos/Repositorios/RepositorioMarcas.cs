using Botines.Datos.Interfaces;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Botines.Datos.Repositorios
{
    public class RepositorioMarcas:IRepositorioMarcas
    {
        private readonly BotinesDbContext _context;

        public RepositorioMarcas(BotinesDbContext context)
        {
            _context = context;
        }

        public void Agregar(Marca marca)
        {
            _context.Marcas.Add(marca);
        }

        public void Borrar(int id)
        {
            try
            {
                var marcaInDb = _context.Marcas.SingleOrDefault(p => p.MarcaId == id);
                if (marcaInDb == null)
                {
                    Exception ex = new Exception("Borrado por otro usuario");
                    throw ex;
                }
                _context.Entry(marcaInDb).State = EntityState.Deleted;
                //_context.SaveChanges();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Editar(Marca marca)
        {
            try
            {
                _context.Entry(marca).State = EntityState.Modified;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EstaRelacionado(Marca marca)
        {
            try
            {
                return _context.Modelos.Any(m => m.MarcaId == marca.MarcaId)
                        || _context.Botines.Any(b => b.MarcaId == marca.MarcaId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Marca marca)
        {
            try
            {
                if (marca.MarcaId == 0)
                {
                    return _context.Marcas.Any(m => m.NombreMarca == marca.NombreMarca);
                }
                return _context.Marcas.Any(m => m.NombreMarca == marca.NombreMarca && m.MarcaId != marca.MarcaId);


            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad()
        {
            return _context.Marcas.Count();
        }

        public List<Marca> GetMarcas()
        {
            try
            {
                return _context.Marcas
                    .AsNoTracking()
                    .OrderBy(m => m.NombreMarca)
                    .ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetMarcasDropDownList()
        {
            var listaMarcas = GetMarcas();
            var dropDownMarcas = listaMarcas.Select(m => new SelectListItem()
            {
                Text = m.NombreMarca,
                Value = m.MarcaId.ToString()
            }).ToList();
            return dropDownMarcas;
        }

        public List<Marca> GetMarcasPorPagina(int cantidad, int pagina)
        {
            return _context.Marcas.OrderBy(m => m.NombreMarca)
                .Skip(cantidad * (pagina - 1))
                .Take(cantidad)
                .ToList();
        }

        public Marca GetMarcaPorId(int marcaId)
        {
            try
            {
                var marcaInDb = _context.Marcas
                    .SingleOrDefault(m => m.MarcaId == marcaId);
                //if (marcaInDb != null) {
                //    _context.Entry(marcaInDb).Reload();
                //}
                return marcaInDb;


            }
            catch (Exception)
            {

                throw;
            }
        }




    }
}
