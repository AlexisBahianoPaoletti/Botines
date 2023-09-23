using Botines.Datos.Interfaces;
using Botines.Datos;
using Botines.Entidades.Dtos.Proveedor;
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
    public class ServiciosProveedores:IServiciosProveedores
    {
        private readonly IRepositorioProveedores _repositorioProveedores;
        private readonly IUnitOfWork _unitOfWork;

        public ServiciosProveedores(IRepositorioProveedores repositorioProveedores, IUnitOfWork unitOfWork)
        {
            _repositorioProveedores = repositorioProveedores;
            _unitOfWork = unitOfWork;

        }

        public void Borrar(int proveedorId)
        {
            try
            {
                _repositorioProveedores.Borrar(proveedorId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionada(Proveedor proveedor)
        {
            try
            {
                return _repositorioProveedores.EstaRelacionada(proveedor);
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
                return _repositorioProveedores.Existe(proveedor);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<ProveedorListDto> GetProveedores()
        {

            try
            {
                return _repositorioProveedores.GetProveedores();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Proveedor GetProveedorPorId(int proveedorId)
        {
            try
            {
                return _repositorioProveedores.GetProveedorPorId(proveedorId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ProveedorListDto> GetProveedoresPais(int paisId)
        {
            try
            {
                return _repositorioProveedores.GetProveedoresPais(paisId);
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
                return _repositorioProveedores.GetProveedoresProvincia(provinciaId);
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
                return _repositorioProveedores.GetProveedoresLocalidad(localidadId);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void Guardar(Proveedor proveedor)
        {
            try
            {
                if (proveedor.ProveedorId == 0)
                {
                    _repositorioProveedores.Agregar(proveedor);

                }
                else
                {
                    _repositorioProveedores.Editar(proveedor);
                }
                _unitOfWork.SaveChanges();


            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProveedorListDto> Filtrar(Func<Proveedor, bool> predicado, int cantidad, int pagina)
        {
            try
            {
                return _repositorioProveedores.Filtrar(predicado, cantidad, pagina);
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
                return _repositorioProveedores.GetCantidad();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ProveedorListDto> GetProveedoresPorPagina(int cantidad, int pagina)
        {
            try
            {
                return _repositorioProveedores.GetProveedoresPorPagina(cantidad, pagina);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad(Func<Proveedor, bool> predicado)
        {
            try
            {
                return _repositorioProveedores.GetCantidad(predicado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetProveedoresDropDownList()
        {
            try
            {
                return _repositorioProveedores.GetProveedoresDropDownList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
