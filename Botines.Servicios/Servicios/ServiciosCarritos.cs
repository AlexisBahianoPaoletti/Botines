using Botines.Datos.Interfaces;
using Botines.Datos;
using Botines.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botines.Entidades.Entidades;

namespace Botines.Servicios.Servicios
{
    public class ServiciosCarritos: IServiciosCarrito
    {
        private readonly IRepositorioCarritos _repositorio;
        private readonly IUnitOfWork _unitOfWork;

        public ServiciosCarritos(IRepositorioCarritos repositorio)
        {
            _repositorio = repositorio;
        }

        public ServiciosCarritos(IRepositorioCarritos repositorio, IUnitOfWork unitOfWork) : this(repositorio)
        {
            _unitOfWork = unitOfWork;
        }

        public void Borrar(string user, int tallebotinId)
        {
            try
            {
                _repositorio.Borrar(user, tallebotinId);
                _unitOfWork.SaveChanges();
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public int GetCantidad(string user)
        {
            try
            {
                return _repositorio.GetCantidad(user);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public List<ItemCarrito> GetCarrito(string user)
        {
            try
            {
                return _repositorio.GetCarrito(user);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public ItemCarrito GetItem(string user, int tallebotinId)
        {
            throw new System.NotImplementedException();
        }

        public void Guardar(ItemCarrito item)
        {
            try
            {
                _repositorio.Guardar(item);
                _unitOfWork.SaveChanges();

            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}
