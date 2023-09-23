using Botines.Datos;
using Botines.Datos.Interfaces;
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
    public class ServiciosPaises:IServiciosPaises
    {
        private readonly IRepositorioPaises _repositorio;
        private readonly IUnitOfWork _unitOfWork;

        public ServiciosPaises()
        {
        }

        //private readonly BotinesDbContext _context;

        public ServiciosPaises(IRepositorioPaises repositorio, IUnitOfWork unitOfWork)
        {
            _repositorio = repositorio;
            _unitOfWork = unitOfWork;
        }


        public void Borrar(int id)
        {
            try
            {
                _repositorio.Borrar(id);
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionado(Pais pais)
        {
            try
            {
                return _repositorio.EstaRelacionado(pais);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Pais pais)
        {
            try
            {
                return _repositorio.Existe(pais);
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
                return _repositorio.GetCantidad();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Pais> GetPaises()
        {
            try
            {
                return _repositorio.GetPaises();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetPaisesDropDownList()
        {
            try
            {
                return _repositorio.GetPaisesDropDownList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Pais GetPaisPorId(int paisId)
        {
            try
            {
                return _repositorio.GetPaisPorId(paisId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Pais> GetPaisPorPagina(int cantidad, int pagina)
        {
            try
            {
                return _repositorio.GetPaisesPorPagina(cantidad, pagina);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Pais pais)
        {
            try
            {
                if (pais.PaisId == 0)
                {
                    _repositorio.Agregar(pais);

                }
                else
                {
                    _repositorio.Editar(pais);
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
