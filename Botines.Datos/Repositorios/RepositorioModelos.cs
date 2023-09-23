using Botines.Datos.Interfaces;
using Botines.Entidades.Dtos.Modelo;
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
    public class RepositorioModelos:IRepositorioModelos
    {
        private readonly BotinesDbContext _context;
        public RepositorioModelos(BotinesDbContext context)
        {
            _context = context;
        }

        public void Agregar(Modelo modelo)
        {
            try
            {
                _context.Modelos.Add(modelo);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Borrar(int modeloId)
        {
            try
            {
                var modeloInDb = GetModeloPorId(modeloId);
                if (modeloInDb == null)
                {
                    throw new Exception("Registro dado de baja por otro usuario");
                }
                else
                {
                    _context.Entry(modeloInDb).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Editar(Modelo modelo)
        {
            try
            {
                var modeloInDb = _context.Modelos.SingleOrDefault(p => p.ModeloId == modelo.ModeloId);


                //var modeloInDb = GetModeloPorId(modelo.ModeloId);

                //if (modeloInDb == null)
                //{
                //    throw new Exception("Registro dado de baja por otro usuario");
                //}


                modeloInDb.Marca = null;
                modeloInDb.NombreModelo = modelo.NombreModelo;
                modeloInDb.MarcaId = modelo.MarcaId;


                _context.Entry(modeloInDb).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EstaRelacionada(Modelo modelo)
        {
            try
            {
                return _context.Botines.Any(b => b.ModeloId == modelo.ModeloId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Modelo modelo)
        {
            try
            {
                if (modelo.ModeloId == 0)
                {
                    return _context.Modelos.Any(m => m.NombreModelo == modelo.NombreModelo
                        && m.MarcaId == modelo.MarcaId);

                }
                return _context.Modelos.Any(m => m.NombreModelo == modelo.NombreModelo
                            && m.MarcaId == modelo.MarcaId
                            && m.ModeloId != modelo.ModeloId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ModeloListDto> Filtrar(Func<Modelo, bool> predicado, int cantidad, int pagina)
        {
            return _context.Modelos.Include(m => m.Marca)
                 .Where(predicado)
                 .OrderBy(p => p.NombreModelo)
                 .Skip(cantidad * (pagina - 1))
                 .Take(cantidad)
                 .Select(m => new ModeloListDto
                 {
                     ModeloId = m.ModeloId,
                     NombreModelo = m.NombreModelo,
                     NombreMarca = m.Marca.NombreMarca

                 }).ToList();
        }

        public int GetCantidad()
        {
            return _context.Modelos.Count();
        }

        public int GetCantidad(Func<Modelo, bool> predicado)
        {
            return _context.Modelos.Count(predicado);
        }


        public List<ModeloListDto> GetModelos()
        {
            return _context.Modelos.Include(m => m.Marca)
                .Select(m => new ModeloListDto
                {
                    ModeloId = m.ModeloId,
                    NombreModelo = m.NombreModelo,
                    NombreMarca = m.Marca.NombreMarca
                }).AsNoTracking()
                .OrderBy(m => m.NombreMarca)
                .ToList();
        }

        public List<ModeloListDto> GetModelos(int marcaId)
        {
            try
            {
                return _context.Modelos.Include(m => m.Marca)
                    .Where(m => m.MarcaId == marcaId)
                    .Select(m => new ModeloListDto
                    {
                        ModeloId = m.ModeloId,
                        NombreModelo = m.NombreModelo,
                        NombreMarca = m.Marca.NombreMarca
                    }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ModeloListDto> GetModelosPorPagina(int cantidad, int pagina)
        {
            return _context.Modelos.Include(m => m.Marca)
                .OrderBy(m => m.MarcaId)
                .Skip(cantidad * (pagina - 1))
                .Take(cantidad)
                .Select(m => new ModeloListDto
                {
                    ModeloId = m.ModeloId,
                    NombreModelo = m.NombreModelo,
                    NombreMarca = m.Marca.NombreMarca
                }).ToList();
        }
        public Modelo GetModeloPorId(int modeloId)
        {
            try
            {
                return _context.Modelos.Include(m => m.Marca)

                    .SingleOrDefault(p => p.ModeloId == modeloId);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetModelosDropDownList()
        {
            var listaModelos = GetModelos();
            var dropDownModelos = listaModelos.Select(m => new SelectListItem()
            {
                Text = m.NombreModelo,
                Value = m.ModeloId.ToString()
            }).ToList();
            return dropDownModelos;
        }


    }
}
