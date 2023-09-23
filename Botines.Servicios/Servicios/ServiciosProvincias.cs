using Botines.Datos;
using Botines.Datos.Interfaces;
using Botines.Entidades.Dtos.Provincia;
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
    public class ServiciosProvincias : IServiciosProvincias
    {
        private readonly IRepositorioProvincias _repositorioProvincias;
        private readonly IUnitOfWork _unitOfWork;

        public ServiciosProvincias(IRepositorioProvincias repositorioProvincias, IUnitOfWork unitOfWork)
        {
            _repositorioProvincias = repositorioProvincias;
            _unitOfWork = unitOfWork;

        }

        public void Borrar(int provinciaId)
        {
            try
            {
                _repositorioProvincias.Borrar(provinciaId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionada(Provincia provincia)
        {
            try
            {
                return _repositorioProvincias.EstaRelacionada(provincia);
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
                return _repositorioProvincias.Existe(provincia);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<ProvinciaListDto> GetProvincias()
        {

            try
            {
                return _repositorioProvincias.GetProvincias();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Provincia GetProvinciaPorId(int provinciaId)
        {
            try
            {
                return _repositorioProvincias.GetProvinciaPorId(provinciaId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ProvinciaListDto> GetProvincias(int paisId)
        {
            try
            {
                return _repositorioProvincias.GetProvincias(paisId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Guardar(Provincia provincia)
        {
            try
            {
                if (provincia.ProvinciaId == 0)
                {
                    _repositorioProvincias.Agregar(provincia);

                }
                else
                {
                    _repositorioProvincias.Editar(provincia);
                }
                _unitOfWork.SaveChanges();


            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProvinciaListDto> Filtrar(Func<Provincia, bool> predicado, int cantidad, int pagina)
        {
            try
            {
                return _repositorioProvincias.Filtrar(predicado, cantidad, pagina);
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
                return _repositorioProvincias.GetCantidad();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ProvinciaListDto> GetProvinciasPorPagina(int cantidad, int pagina)
        {
            try
            {
                return _repositorioProvincias.GetProvinciasPorPagina(cantidad, pagina);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad(Func<Provincia, bool> predicado)
        {
            try
            {
                return _repositorioProvincias.GetCantidad(predicado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetProvinciasDropDownList()
        {
            try
            {
                return _repositorioProvincias.GetProvinciasDropDownList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
