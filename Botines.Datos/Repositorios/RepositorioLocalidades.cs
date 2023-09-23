using Botines.Datos.Interfaces;
using Botines.Entidades.Dtos.Localidad;
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
    public class RepositorioLocalidades : IRepositorioLocalidades
    {
        private readonly BotinesDbContext _context;
        public RepositorioLocalidades(BotinesDbContext context)
        {
            _context = context;
        }

        public void Agregar(Localidad localidad)
        {
            try
            {
                _context.Localidades.Add(localidad);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Borrar(int localidadId)
        {
            try
            {
                var localidadInDb = GetLocalidadPorId(localidadId);
                if (localidadInDb == null)
                {
                    throw new Exception("Registro dado de baja por otro usuario");
                }
                else
                {
                    _context.Entry(localidadInDb).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Editar(Localidad localidad)
        {
            try
            {
                var localidadInDb = _context.Localidades.SingleOrDefault(p => p.LocalidadId == localidad.LocalidadId);

                //var localidadInDb = GetLocalidadPorId(localidad.LocalidadId);

                //if (localidadInDb == null)
                //{
                //    throw new Exception("Registro dado de baja por otro usuario");
                //}

                localidadInDb.Provincia = null;
                localidadInDb.NombreLocalidad = localidad.NombreLocalidad;
                localidadInDb.ProvinciaId = localidad.ProvinciaId;


                _context.Entry(localidadInDb).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EstaRelacionada(Localidad localidad)
        {
            try
            {
                return _context.Clientes.Any(c => c.LocalidadId == localidad.LocalidadId)
                    || _context.Proveedores.Any(p => p.LocalidadId == localidad.LocalidadId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Localidad localidad)
        {
            try
            {
                if (localidad.LocalidadId == 0)
                {
                    return _context.Localidades.Any(l => l.NombreLocalidad == localidad.NombreLocalidad
                        && l.ProvinciaId == localidad.ProvinciaId);

                }
                return _context.Localidades.Any(l => l.NombreLocalidad == localidad.NombreLocalidad
                            && l.ProvinciaId == localidad.ProvinciaId
                            && l.LocalidadId != localidad.LocalidadId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<LocalidadListDto> Filtrar(Func<Localidad, bool> predicado, int cantidad, int pagina)
        {
            return _context.Localidades.Include(l => l.Provincia)
                .Where(predicado)
                .OrderBy(l => l.NombreLocalidad)
                .Skip(cantidad * (pagina - 1))
                .Take(cantidad)
                .Select(l => new LocalidadListDto
                {
                    LocalidadId = l.LocalidadId,
                    NombreLocalidad = l.NombreLocalidad,
                    NombreProvincia = l.Provincia.NombreProvincia

                }).ToList();
        }

        public int GetCantidad()
        {
            return _context.Localidades.Count();
        }

        public int GetCantidad(Func<Localidad, bool> predicado)
        {
            return _context.Localidades.Count(predicado);
        }

        public List<LocalidadListDto> GetLocalidades()
        {
            return _context.Localidades.Include(l => l.Provincia)
                .Select(l => new LocalidadListDto
                {
                    LocalidadId = l.LocalidadId,
                    NombreLocalidad = l.NombreLocalidad,
                    NombreProvincia = l.Provincia.NombreProvincia
                }).AsNoTracking()
                .ToList();
        }

        public List<LocalidadListDto> GetLocalidades(int provinciaId)
        {
            try
            {
                return _context.Localidades.Include(l => l.Provincia)
                    .Where(l => l.ProvinciaId == provinciaId)
                    .Select(l => new LocalidadListDto
                    {
                        LocalidadId = l.LocalidadId,
                        NombreLocalidad = l.NombreLocalidad,
                        NombreProvincia = l.Provincia.NombreProvincia
                    }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetLocalidadesDropDownList()
        {
            var listaLocalidades = GetLocalidades();
            var dropDownLocalidades = listaLocalidades.Select(l => new SelectListItem()
            {
                Text = l.NombreLocalidad,
                Value = l.LocalidadId.ToString()
            }).ToList();
            return dropDownLocalidades;
        }

        public List<LocalidadListDto> GetLocalidadesPorPagina(int cantidad, int pagina)
        {
            return _context.Localidades.Include(l => l.Provincia)
                    .OrderBy(l => l.ProvinciaId)
                    .Skip(cantidad * (pagina - 1))
                     .Take(cantidad)
                     .Select(l => new LocalidadListDto
                     {
                        LocalidadId = l.LocalidadId,
                        NombreLocalidad = l.NombreLocalidad,
                        NombreProvincia = l.Provincia.NombreProvincia
                     }).ToList();
        }

        public Localidad GetLocalidadPorId(int localidadId)
        {
            try
            {
                return _context.Localidades.Include(l => l.Provincia)

                    .SingleOrDefault(l => l.LocalidadId == localidadId);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
