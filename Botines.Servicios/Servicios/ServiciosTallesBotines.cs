using Botines.Datos.Interfaces;
using Botines.Datos;
using Botines.Entidades.Dtos.TalleBotin;
using Botines.Entidades.Entidades;
using Botines.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botines.Datos.Repositorios;
using Botines.Entidades.Dtos.Talle;

namespace Botines.Servicios.Servicios
{
    public class ServiciosTallesBotines : IServiciosTallesBotines
    {
        private readonly IRepositorioTallesBotines _repositorioTallesBotines;
        private readonly IUnitOfWork _unitOfWork;

        public ServiciosTallesBotines(IRepositorioTallesBotines repositorioTallesBotines, IUnitOfWork unitOfWork)
        {
            _repositorioTallesBotines = repositorioTallesBotines;
            _unitOfWork = unitOfWork;
        }

        public void Borrar(int tallebotinId)
        {
            try
            {
                _repositorioTallesBotines.Borrar(tallebotinId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionada(TalleBotin tallebotin)
        {
            try
            {
                return _repositorioTallesBotines.EstaRelacionada(tallebotin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(TalleBotin tallebotin)
        {
            try
            {
                return _repositorioTallesBotines.Existe(tallebotin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TalleBotinListDto> Filtrar(Func<TalleBotin, bool> predicado, int cantidad, int pagina)
        {
            try
            {
                return _repositorioTallesBotines.Filtrar(predicado, cantidad, pagina);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TalleListDto> GetBotines(int botinId)
        {
            try
            {
                return _repositorioTallesBotines.GetBotines(botinId);
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
                return _repositorioTallesBotines.GetCantidad();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad(Func<TalleBotin, bool> predicado)
        {
            try
            {
                return _repositorioTallesBotines.GetCantidad(predicado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidadStock()
        {
            try
            {
                return _repositorioTallesBotines.GetCantidadStock();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TalleBotin GetTalleBotinPorId(int tallebotinId)
        {
            try
            {
                return _repositorioTallesBotines.GetTalleBotinPorId(tallebotinId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TalleBotin GetTalleBotinPorId2(int botinId, int talleId)
        {
            try
            {
                return _repositorioTallesBotines.GetTalleBotinPorId2( botinId, talleId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TalleBotinListDto> GetTalles(int talleId)
        {
            try
            {
                return _repositorioTallesBotines.GetTallesBotines();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TalleBotinListDto> GetTallesBotines()
        {
            try
            {
                return _repositorioTallesBotines.GetTallesBotines();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TalleBotinListDto> GetTallesBotinesPorPagina(int cantidad, int pagina)
        {
            try
            {
                return _repositorioTallesBotines.GetTallesBotinesPorPagina(cantidad, pagina);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(TalleBotin tallebotin)
        {
            try
            {
                if (tallebotin.TalleBotinId == 0)
                {
                    _repositorioTallesBotines.Agregar(tallebotin);

                }
                else
                {
                    _repositorioTallesBotines.Editar(tallebotin);
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
