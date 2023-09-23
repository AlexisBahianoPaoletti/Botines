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
    public class RepositorioTalles:IRepositorioTalles
    {
        private readonly BotinesDbContext _context;

        public RepositorioTalles(BotinesDbContext context)
        {
            _context = context;
        }

        public void Agregar(Talle talle)
        {
            _context.Talles.Add(talle);
        }

        public void Borrar(int id)
        {
            try
            {
                var talleInDb = _context.Talles.SingleOrDefault(p => p.TalleId == id);
                if (talleInDb == null)
                {
                    Exception ex = new Exception("Borrado por otro usuario");
                    throw ex;
                }
                _context.Entry(talleInDb).State = EntityState.Deleted;
                //_context.SaveChanges();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Editar(Talle talle)
        {
            try
            {
                _context.Entry(talle).State = EntityState.Modified;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EstaRelacionado(Talle talle)
        {
            try
            {
                return _context.TallesBotines.Any(tb => tb.TalleId == talle.TalleId);                 

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Talle talle)
        {
            try
            {
                if (talle.TalleId == 0)
                {
                    return _context.Talles.Any(t => t.NumeroTalle == talle.NumeroTalle);
                }
                return _context.Talles.Any(t => t.NumeroTalle == talle.NumeroTalle && t.TalleId != talle.TalleId);


            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad()
        {
            return _context.Talles.Count();
        }

        public List<Talle> GetTalles()
        {
            try
            {
                return _context.Talles
                    .AsNoTracking()
                    .OrderBy(t => t.NumeroTalle)
                    .ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetTallesDropDownList()
        {
            var listaTalles = GetTalles();
            var dropDownTalles = listaTalles.Select(t => new SelectListItem()
            {
                Text = t.NumeroTalle.ToString(),
                Value = t.TalleId.ToString()
            }).ToList();
            return dropDownTalles;
        }

        public List<Talle> GetTallesPorPagina(int cantidad, int pagina)
        {
            return _context.Talles.OrderBy(t => t.NumeroTalle)
                .Skip(cantidad * (pagina - 1))
                .Take(cantidad)
                .ToList();
        }

        public Talle GetTallePorId(int talleId)
        {
            try
            {
                var talleInDb = _context.Talles
                    .SingleOrDefault(t => t.TalleId == talleId);
                //if (talleInDb != null) {
                //    _context.Entry(talleInDb).Reload();
                //}
                return talleInDb;


            }
            catch (Exception)
            {

                throw;
            }
        }



    }
}
