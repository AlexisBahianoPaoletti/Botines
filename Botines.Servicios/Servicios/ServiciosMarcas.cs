using Botines.Datos.Interfaces;
using Botines.Datos;
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
    public class ServiciosMarcas:IServiciosMarcas
    {
        private readonly IRepositorioMarcas _repositorioMarcas;
        private readonly IUnitOfWork _unitOfWork;

        public ServiciosMarcas(IRepositorioMarcas repositorioMarcas, IUnitOfWork unitOfWork)
        {
            _repositorioMarcas = repositorioMarcas;
            _unitOfWork = unitOfWork;
        }

        public void Borrar(int id)
        {
            try
            {
                _repositorioMarcas.Borrar(id);
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionado(Marca marca)
        {
            try
            {
                return _repositorioMarcas.EstaRelacionado(marca);
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
                return _repositorioMarcas.Existe(marca);
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
                return _repositorioMarcas.GetCantidad();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Marca> GetMarcas()
        {
            try
            {
                return _repositorioMarcas.GetMarcas();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetMarcasDropDownList()
        {
            try
            {
                return _repositorioMarcas.GetMarcasDropDownList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Marca GetMarcaPorId(int marcaId)
        {
            try
            {
                return _repositorioMarcas.GetMarcaPorId(marcaId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Marca> GetMarcaPorPagina(int cantidad, int pagina)
        {
            try
            {
                return _repositorioMarcas.GetMarcasPorPagina(cantidad, pagina);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Marca marca)
        {
            try
            {
                if (marca.MarcaId == 0)
                {
                    _repositorioMarcas.Agregar(marca);

                }
                else
                {
                    _repositorioMarcas.Editar(marca);
                }
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
