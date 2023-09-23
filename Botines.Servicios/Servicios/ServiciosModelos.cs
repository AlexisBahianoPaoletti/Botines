using Botines.Datos.Interfaces;
using Botines.Datos;
using Botines.Entidades.Dtos.Modelo;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botines.Servicios.Interfaces;
using System.Web.Mvc;

namespace Botines.Servicios.Servicios
{
    public class ServiciosModelos:IServiciosModelos
    {
        private readonly IRepositorioModelos _repositorioModelos;
        private readonly IUnitOfWork _unitOfWork;

        public ServiciosModelos(IRepositorioModelos repositorioModelos, IUnitOfWork unitOfWork)
        {
            _repositorioModelos = repositorioModelos;
            _unitOfWork = unitOfWork;

        }

        public void Borrar(int modeloId)
        {
            try
            {
                _repositorioModelos.Borrar(modeloId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionada(Modelo modelo)
        {
            try
            {
                return _repositorioModelos.EstaRelacionada(modelo);
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
                return _repositorioModelos.Existe(modelo);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<ModeloListDto> GetModelos()
        {

            try
            {
                return _repositorioModelos.GetModelos();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Modelo GetModeloPorId(int modeloId)
        {
            try
            {
                return _repositorioModelos.GetModeloPorId(modeloId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ModeloListDto> GetModelos(int marcaId)
        {
            try
            {
                return _repositorioModelos.GetModelos(marcaId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Guardar(Modelo modelo)
        {
            try
            {
                if (modelo.ModeloId == 0)
                {
                    _repositorioModelos.Agregar(modelo);

                }
                else
                {
                    _repositorioModelos.Editar(modelo);
                }
                _unitOfWork.SaveChanges();


            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ModeloListDto> Filtrar(Func<Modelo, bool> predicado, int cantidad, int pagina)
        {
            try
            {
                return _repositorioModelos.Filtrar(predicado, cantidad, pagina);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad()
        {
            try
            {
                return _repositorioModelos.GetCantidad();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ModeloListDto> GetModelosPorPagina(int cantidad, int pagina)
        {
            try
            {
                return _repositorioModelos.GetModelosPorPagina(cantidad, pagina);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad(Func<Modelo, bool> predicado)
        {
            try
            {
                return _repositorioModelos.GetCantidad(predicado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetModelosDropDownList()
        {
            try
            {
                return _repositorioModelos.GetModelosDropDownList();
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
