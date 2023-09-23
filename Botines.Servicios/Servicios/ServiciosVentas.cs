using Botines.Datos.Interfaces;
using Botines.Datos;
using Botines.Entidades.Dtos.Venta;
using Botines.Entidades.Entidades;
using Botines.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Botines.Entidades.Dtos.VentaBotin;
using Botines.Servicios.Interfaces;
using Botines.Entidades.Dtos.VentaCarrito;

namespace Botines.Servicios.Servicios
{
    public class ServiciosVentas : IServiciosVentas
    {
        private readonly IRepositorioVentas _repositorio;
        private readonly IRepositorioVentasCarritos _repoVentasCarritos;
        private readonly IRepositorioBotines _repoBotines;
        private readonly IRepositorioCarritos _repoCarritos;
        private readonly IUnitOfWork _unitOfWork;

        public ServiciosVentas(IRepositorioVentas repositorio,
            IRepositorioVentasCarritos repoVentasCarritos,
            IRepositorioBotines repoBotines,
            IRepositorioCarritos repoCarritos,
            IUnitOfWork unitOfWork)
        {
            _repositorio = repositorio;
            _repoVentasCarritos = repoVentasCarritos;
            _repoBotines = repoBotines;
            _repoCarritos = repoCarritos;
            _unitOfWork = unitOfWork;
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


        public List<VentaCarritoListDto> GetVentaCarrito(int ventaId)
        {
            try
            {
                return _repoVentasCarritos.GetVentasCarritos(ventaId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<VentaListDto> GetVentas()
        {
            try
            {
                return _repositorio.GetVentas();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public VentaListDto GetVentasPorId(int id)
        {
            try
            {
                return _repositorio.GetVentaPorId(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Guardar(Venta venta)
        {
            try
            {
                Venta ventaAgregar = new Venta()
                {
                    Estado = venta.Estado,
                    Fecha = venta.Fecha,
                    Total = venta.Total,
                    ClienteId = venta.ClienteId
                };
                _repositorio.Agregar(ventaAgregar);
                _unitOfWork.SaveChanges();
                foreach (var ventaCarrito in venta.Detalles)
                {
                    ventaCarrito.VentaId = ventaAgregar.VentaId;

                    _repoVentasCarritos.Agregar(ventaCarrito);
                    _repoCarritos.EditarCarrito(ventaCarrito.ItemCarritoId);
                    //_repoBotines.ActualizarStock(item.BotinId, item.Cantidad);
                    //_repoCarritos.Borrar(user, item.BotinId);
                }
                _unitOfWork.SaveChanges();


            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
