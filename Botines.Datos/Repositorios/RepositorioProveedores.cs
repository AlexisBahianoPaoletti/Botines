using Botines.Datos.Interfaces;
using Botines.Entidades.Dtos.Proveedor;
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
    public class RepositorioProveedores:IRepositorioProveedores
    {
        private readonly BotinesDbContext _context;
        public RepositorioProveedores(BotinesDbContext context)
        {
            _context = context;
        }

        public void Agregar(Proveedor proveedor)
        {
            try
            {
                _context.Proveedores.Add(proveedor);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Borrar(int proveedorId)
        {
            try
            {
                var proveedorInDb = GetProveedorPorId(proveedorId);
                if (proveedorInDb == null)
                {
                    throw new Exception("Registro dado de baja por otro usuario");
                }
                else
                {
                    _context.Entry(proveedorInDb).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Editar(Proveedor proveedor)
        {
            try
            {
                var proveedorInDb = _context.Proveedores.SingleOrDefault(p => p.ProveedorId == proveedor.ProveedorId);


                //var proveedorInDb = GetProveedorPorId(proveedor.ProveedorId);

                //if (proveedorInDb == null)
                //{
                //    throw new Exception("Registro dado de baja por otro usuario");
                //}


                proveedorInDb.Pais = null;
                proveedorInDb.Provincia = null;
                proveedorInDb.Localidad = null;
                proveedorInDb.RazonSocial = proveedor.RazonSocial;
                proveedorInDb.PaisId = proveedor.PaisId;
                proveedorInDb.ProvinciaId = proveedor.ProvinciaId;
                proveedorInDb.LocalidadId = proveedor.LocalidadId;

                proveedorInDb.Direccion = proveedor.Direccion;
                proveedorInDb.TelefonoFijo = proveedor.TelefonoFijo;
                proveedorInDb.TelefonoMovil = proveedor.TelefonoMovil;
                proveedorInDb.CorreoElectronico = proveedor.CorreoElectronico;


                _context.Entry(proveedorInDb).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EstaRelacionada(Proveedor proveedor)
        {
            try
            {
                return _context.Botines.Any(b => b.ProveedorId == proveedor.ProveedorId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Proveedor proveedor)
        {
            try
            {
                if (proveedor.ProveedorId == 0)
                {
                    return _context.Proveedores.Any(p => p.RazonSocial == proveedor.RazonSocial
                        && p.PaisId == proveedor.PaisId
                        && p.ProvinciaId == proveedor.ProvinciaId
                        && p.LocalidadId == proveedor.LocalidadId);

                }
                return _context.Proveedores.Any(p => p.RazonSocial == proveedor.RazonSocial
                            && p.PaisId == proveedor.PaisId 
                            && p.ProvinciaId == proveedor.ProvinciaId
                            && p.LocalidadId == proveedor.LocalidadId
                            && p.ProveedorId != proveedor.ProveedorId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProveedorListDto> Filtrar(Func<Proveedor, bool> predicado, int cantidad, int pagina)
        {
            return _context.Proveedores.Include(p => p.Pais).Include(p=>p.Provincia).Include(p=>p.Localidad)
                 .Where(predicado)
                 .OrderBy(p => p.RazonSocial)
                 .Skip(cantidad * (pagina - 1))
                 .Take(cantidad)
                 .Select(p => new ProveedorListDto
                 {
                     ProveedorId = p.ProveedorId,
                     RazonSocial = p.RazonSocial,
                     NombrePais = p.Pais.NombrePais,
                     NombreProvincia = p.Provincia.NombreProvincia,
                     NombreLocalidad = p.Localidad.NombreLocalidad

                 }).ToList();
        }

        public int GetCantidad()
        {
            return _context.Proveedores.Count();
        }

        public int GetCantidad(Func<Proveedor, bool> predicado)
        {
            return _context.Proveedores.Count(predicado);
        }


        public List<ProveedorListDto> GetProveedores()
        {
            return _context.Proveedores.Include(p => p.Pais).Include(p => p.Provincia).Include(p => p.Localidad)
                .Select(p => new ProveedorListDto
                {
                    ProveedorId = p.ProveedorId,
                    RazonSocial = p.RazonSocial,
                    NombrePais = p.Pais.NombrePais,
                    NombreProvincia=p.Provincia.NombreProvincia,
                    NombreLocalidad=p.Localidad.NombreLocalidad
                }).AsNoTracking()
                .ToList();
        }

        public List<ProveedorListDto> GetProveedoresPais(int paisId)
        {
            try
            {
                return _context.Proveedores.Include(p => p.Pais)
                    .Where(p => p.PaisId == paisId)
                    .Select(p => new ProveedorListDto
                    {
                        ProveedorId = p.ProveedorId,
                        RazonSocial = p.RazonSocial,
                        NombrePais = p.Pais.NombrePais
                    }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ProveedorListDto> GetProveedoresProvincia(int provinciaId)
        {
            try
            {
                return _context.Proveedores.Include(p => p.Provincia)
                    .Where(p => p.ProvinciaId == provinciaId)
                    .Select(p => new ProveedorListDto
                    {
                        ProveedorId = p.ProveedorId,
                        RazonSocial = p.RazonSocial,
                        NombreProvincia = p.Provincia.NombreProvincia
                    }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ProveedorListDto> GetProveedoresLocalidad(int localidadId)
        {
            try
            {
                return _context.Proveedores.Include(p => p.Localidad)
                    .Where(p => p.LocalidadId == localidadId)
                    .Select(p => new ProveedorListDto
                    {
                        ProveedorId = p.ProveedorId,
                        RazonSocial = p.RazonSocial,
                        NombreLocalidad = p.Localidad.NombreLocalidad
                    }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProveedorListDto> GetProveedoresPorPagina(int cantidad, int pagina)
        {
            return _context.Proveedores.Include(p => p.Pais).Include(p => p.Provincia).Include(p => p.Localidad)
                .OrderBy(p => p.RazonSocial)
                .Skip(cantidad * (pagina - 1))
                .Take(cantidad)
                .Select(p => new ProveedorListDto
                {
                    ProveedorId = p.ProveedorId,
                    RazonSocial = p.RazonSocial,
                    NombrePais = p.Pais.NombrePais,
                    NombreProvincia=p.Provincia.NombreProvincia,
                    NombreLocalidad=p.Localidad.NombreLocalidad
                }).ToList();
        }
        public Proveedor GetProveedorPorId(int proveedorId)
        {
            try
            {
                return _context.Proveedores.Include(p => p.Pais).Include(p => p.Provincia).Include(p => p.Localidad)
                        .SingleOrDefault(p => p.ProveedorId == proveedorId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetProveedoresDropDownList()
        {
            var listaProveedores = GetProveedores();
            var dropDownProveedores = listaProveedores.Select(p => new SelectListItem()
            {
                Text = p.RazonSocial,
                Value = p.ProveedorId.ToString()
            }).ToList();
            return dropDownProveedores;
        }
    }
}
