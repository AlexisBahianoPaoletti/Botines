using Botines.Datos.Interfaces;
using Botines.Datos;
using Botines.Entidades.Dtos.Botin;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botines.Servicios.Interfaces;
using Botines.Datos.Repositorios;
using Botines.Entidades.Dtos.Talle;

namespace Botines.Servicios.Servicios
{
    public class ServiciosBotines:IServiciosBotines
    {
        private readonly IRepositorioBotines _repositorioBotines;
        private readonly IRepositorioTallesBotines _repositorioTallesBotines;
        private readonly IUnitOfWork _unitOfWork;

        public ServiciosBotines(IRepositorioBotines repositorioBotines, 
                        IRepositorioTallesBotines repositorioTallesBotines, 
                            IUnitOfWork unitOfWork)
        {
            _repositorioBotines = repositorioBotines;
            _repositorioTallesBotines = repositorioTallesBotines;
            _unitOfWork = unitOfWork;

        }

        public void Borrar(int botinId)
        {
            try
            {
                _repositorioBotines.Borrar(botinId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionada(Botin botin)
        {
            try
            {
                return _repositorioBotines.EstaRelacionada(botin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Botin botin)
        {
            try
            {
                return _repositorioBotines.Existe(botin);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<BotinListDto> GetBotines()
        {

            try
            {
                return _repositorioBotines.GetBotines();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Botin GetBotinPorId(int botinId)
        {
            try
            {
                return _repositorioBotines.GetBotinPorId(botinId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<BotinListDto> GetBotinesMarca(int marcaId)
        {
            try
            {
                return _repositorioBotines.GetBotinesMarca(marcaId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Guardar(Botin botin)
        {
            try
            {
                if (botin.BotinId == 0)
                {
                    _repositorioBotines.Agregar(botin);

                }
                else
                {
                    _repositorioBotines.Editar(botin);
                }
                _unitOfWork.SaveChanges();


            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<BotinListDto> Filtrar(Func<Botin, bool> predicado, int cantidad, int pagina)
        {
            try
            {
                return _repositorioBotines.Filtrar(predicado, cantidad, pagina);
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
                return _repositorioBotines.GetCantidad();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<BotinListDto> GetBotinesPorPagina(int cantidad, int pagina)
        {
            try
            {
                return _repositorioBotines.GetBotinesPorPagina(cantidad, pagina);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad(Func<Botin, bool> predicado)
        {
            try
            {
                return _repositorioBotines.GetCantidad(predicado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<BotinListDto> GetTallesBotines()
        {
            try
            {
                var listaBotines = _repositorioBotines.GetBotines();
                var listaBotinesListDto = new List<BotinListDto>();
                foreach (var botin in listaBotines)
                {
                    BotinListDto botinListDto = new BotinListDto();
                    botinListDto.NombreBotin = botin.NombreBotin;
                    botinListDto.BotinId = botin.BotinId;
                    botinListDto.PrecioVenta = botin.PrecioVenta;
                    botinListDto.Suspendido=botin.Suspendido;
                    botinListDto.Modelo = botin.Modelo;
                    botinListDto.Marca = botin.Marca;
                    botinListDto.Talles = _repositorioTallesBotines.GetBotines(botin.BotinId);
                    listaBotinesListDto.Add(botinListDto);
                }
                return listaBotinesListDto;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
