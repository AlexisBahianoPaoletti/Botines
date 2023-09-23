using Botines.Datos;
using Botines.Datos.Interfaces;
using Botines.Datos.Repositorios;
using Botines.Entidades.Dtos.Localidad;
using Botines.Entidades.Entidades;
using Botines.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Botines.Servicios.Servicios
{
    public class ServiciosLocalidades : IServiciosLocalidades
    {
        private readonly IRepositorioLocalidades _repositorioLocalidades;
        private readonly IUnitOfWork _unitOfWork;

        public ServiciosLocalidades(IRepositorioLocalidades repositorioLocalidades, IUnitOfWork unitOfWork)
        {
            _repositorioLocalidades=repositorioLocalidades; 
            _unitOfWork=unitOfWork;

        }


        public void Borrar(int localidadId)
        {
            try
            {
                _repositorioLocalidades.Borrar(localidadId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionada(Localidad localidad)
        {
            try
            {
                return _repositorioLocalidades.EstaRelacionada(localidad);
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
                return _repositorioLocalidades.Existe(localidad);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<LocalidadListDto> GetLocalidades()
        {
            try
            {
                return _repositorioLocalidades.GetLocalidades();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Localidad GetLocalidadPorId(int localidadId)
        {
            try
            {
                return _repositorioLocalidades.GetLocalidadPorId(localidadId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<LocalidadListDto> GetLocalidades(int provinciaId)
        {
            try
            {
                return _repositorioLocalidades.GetLocalidades();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Guardar(Localidad localidad)
        {
            try
            {
                if (localidad.LocalidadId == 0)
                {
                    _repositorioLocalidades.Agregar(localidad);

                }
                else
                {
                    _repositorioLocalidades.Editar(localidad);
                }
                _unitOfWork.SaveChanges();


            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<LocalidadListDto> Filtrar(Func<Localidad, bool> predicado, int cantidad, int pagina)
        {
            try
            {
                return _repositorioLocalidades.Filtrar(predicado, cantidad, pagina);
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
                return _repositorioLocalidades.GetCantidad();
            }
            catch (Exception)
            {

                throw;
            }
        }




        public List<LocalidadListDto> GetLocalidadesPorPagina(int cantidad, int pagina)
        {
            try
            {
                return _repositorioLocalidades.GetLocalidadesPorPagina(cantidad, pagina);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int GetCantidad(Func<Localidad, bool> predicado)
        {
            try
            {
                return _repositorioLocalidades.GetCantidad(predicado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetLocalidadesDropDownList()
        {
            try
            {
                return _repositorioLocalidades.GetLocalidadesDropDownList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
