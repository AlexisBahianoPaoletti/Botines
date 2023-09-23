using Botines.Datos.Interfaces;
using Botines.Entidades.Dtos.Provincia;
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
    public class RepositorioProvincias : IRepositorioProvincias
    {
        private readonly BotinesDbContext _context;
        public RepositorioProvincias(BotinesDbContext context)
        {
            _context = context;
        }

        public void Agregar(Provincia provincia)
        {
            try
            {
                _context.Provincias.Add(provincia);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Borrar(int provinciaId)
        {
            try
            {
                var provinciaInDb = GetProvinciaPorId(provinciaId);
                if (provinciaInDb == null)
                {
                    throw new Exception("Registro dado de baja por otro usuario");
                }
                else
                {
                    _context.Entry(provinciaInDb).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Editar(Provincia provincia)
        {
            try
            {
                var provinciaInDb = _context.Provincias.SingleOrDefault(p => p.ProvinciaId == provincia.ProvinciaId);


                //var provinciaInDb = GetProvinciaPorId(provincia.ProvinciaId);

                //if (provinciaInDb == null)
                //{
                //    throw new Exception("Registro dado de baja por otro usuario");
                //}


                provinciaInDb.Pais = null;
                provinciaInDb.NombreProvincia = provincia.NombreProvincia;
                provinciaInDb.PaisId = provincia.PaisId;


                _context.Entry(provinciaInDb).State = EntityState.Modified;               
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EstaRelacionada(Provincia provincia)
        {
            try
            {
                return _context.Clientes.Any(c => c.ProvinciaId == provincia.ProvinciaId)
                    || _context.Proveedores.Any(p => p.ProvinciaId == provincia.ProvinciaId)
                    || _context.Localidades.Any(l => l.ProvinciaId == provincia.ProvinciaId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Provincia provincia)
        {
            try
            {
                if (provincia.ProvinciaId == 0)
                {
                    return _context.Provincias.Any(p => p.NombreProvincia == provincia.NombreProvincia
                        && p.PaisId == provincia.PaisId);

                }
                return _context.Provincias.Any(p => p.NombreProvincia == provincia.NombreProvincia
                            && p.PaisId == provincia.PaisId
                            && p.ProvinciaId != provincia.ProvinciaId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProvinciaListDto> Filtrar(Func<Provincia, bool> predicado, int cantidad, int pagina)
        {
            return _context.Provincias.Include(p => p.Pais)
                 .Where(predicado)
                 .OrderBy(p => p.NombreProvincia)
                 .Skip(cantidad * (pagina - 1))
                 .Take(cantidad)
                 .Select(p => new ProvinciaListDto
                 {
                     ProvinciaId = p.ProvinciaId,
                     NombreProvincia = p.NombreProvincia,
                     NombrePais = p.Pais.NombrePais

                 }).ToList();
        }

        public int GetCantidad()
        {
            return _context.Provincias.Count();
        }

        public int GetCantidad(Func<Provincia, bool> predicado)
        {
            return _context.Provincias.Count(predicado);
        }


        public List<ProvinciaListDto> GetProvincias()
        {
            return _context.Provincias.Include(p => p.Pais)
                .Select(p => new ProvinciaListDto
                {
                    ProvinciaId = p.ProvinciaId,
                    NombreProvincia = p.NombreProvincia,
                    NombrePais = p.Pais.NombrePais
                }).AsNoTracking()
                .ToList();
        }

        public List<ProvinciaListDto> GetProvincias(int paisId)
        {
            try
            {
                return _context.Provincias.Include(p => p.Pais)
                    .Where(p => p.PaisId == paisId)
                    .Select(p => new ProvinciaListDto
                    {
                        ProvinciaId = p.ProvinciaId,
                        NombreProvincia = p.NombreProvincia,
                        NombrePais = p.Pais.NombrePais
                    }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProvinciaListDto> GetProvinciasPorPagina(int cantidad, int pagina)
        {
            return _context.Provincias.Include(p => p.Pais)
                .OrderBy(p => p.PaisId)
                .Skip(cantidad * (pagina - 1))
                .Take(cantidad)
                .Select(p => new ProvinciaListDto
                {
                    ProvinciaId = p.ProvinciaId,
                    NombreProvincia = p.NombreProvincia,
                    NombrePais = p.Pais.NombrePais
                }).ToList();
        }
        public Provincia GetProvinciaPorId(int provinciaId)
        {
            try
            {
                return _context.Provincias.Include(p => p.Pais)

                    .SingleOrDefault(p => p.ProvinciaId == provinciaId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetProvinciasDropDownList()
        {
            var listaProvincias = GetProvincias();
            var dropDownProvincias = listaProvincias.Select(p => new SelectListItem()
            {
                Text = p.NombreProvincia,
                Value = p.ProvinciaId.ToString()
            }).ToList();
            return dropDownProvincias;
        }
    }
}
