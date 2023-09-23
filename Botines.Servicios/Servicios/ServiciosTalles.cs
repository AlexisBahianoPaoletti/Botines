using Botines.Datos.Interfaces;
using Botines.Datos;
using Botines.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botines.Entidades.Entidades;
using System.Web.Mvc;
using Botines.Entidades.Dtos.Talle;

namespace Botines.Servicios.Servicios
{
    public class ServiciosTalles:IServiciosTalles
    {
        private readonly IRepositorioTalles _repositorioTalles;
        private readonly IRepositorioTallesBotines _repositorioTallesBotines;
        private readonly IUnitOfWork _unitOfWork;

        public ServiciosTalles(IRepositorioTalles repositorioTalles,
            IRepositorioTallesBotines repositorioTallesBotines, IUnitOfWork unitOfWork)
        {
            _repositorioTalles = repositorioTalles;
            _repositorioTallesBotines = repositorioTallesBotines;
            _unitOfWork = unitOfWork;
        }

        public void Borrar(int id)
        {
            try
            {
                _repositorioTalles.Borrar(id);
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionado(Talle talle)
        {
            try
            {
                return _repositorioTalles.EstaRelacionado(talle);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Talle talle)
        {
            try
            {
                return _repositorioTalles.Existe(talle);
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
                return _repositorioTalles.GetCantidad();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Talle> GetTalles()
        {
            try
            {
                return _repositorioTalles.GetTalles();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SelectListItem> GetTallesDropDownList()
        {
            try
            {
                return _repositorioTalles.GetTallesDropDownList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Talle GetTallePorId(int talleId)
        {
            try
            {
                return _repositorioTalles.GetTallePorId(talleId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Talle> GetTallePorPagina(int cantidad, int pagina)
        {
            try
            {
                return _repositorioTalles.GetTallesPorPagina(cantidad, pagina);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Talle talle)
        {
            try
            {
                if (talle.TalleId == 0)
                {
                    _repositorioTalles.Agregar(talle);

                }
                else
                {
                    _repositorioTalles.Editar(talle);
                }
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TalleListDto> GetTallesBotines()
        {
            try
            {
                var listaTalles = _repositorioTalles.GetTalles();
                var listaTallesListDto= new List<TalleListDto>();
                foreach (var talle in listaTalles)
                {
                    TalleListDto talleListDto = new TalleListDto();
                    talleListDto.NumeroTalle = talle.NumeroTalle;
                    talleListDto.TalleId= talle.TalleId;
                    talleListDto.Botines = _repositorioTallesBotines.GetTalles(talle.TalleId);
                    listaTallesListDto.Add(talleListDto);
                }
                return listaTallesListDto;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
