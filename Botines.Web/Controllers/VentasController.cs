using AutoMapper;
using Botines.Entidades.Dtos.VentaCarrito;
using Botines.Servicios.Interfaces;
using Botines.Web.App_Start;
using Botines.Web.ViewModels.Venta;
using Botines.Web.ViewModels.VentaCarrito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Botines.Web.Controllers
{
    public class VentasController : Controller
    {
        // GET: Ventas
        private readonly IServiciosVentas _servicio;
        private readonly IMapper _mapper;
        public VentasController(IServiciosVentas servicio)
        {
            _servicio = servicio;
            _mapper = AutoMapperConfig.Mapper;
        }
        public ActionResult Index()
        {
            var lista = _servicio.GetVentas();
            var listaVm = _mapper.Map<List<VentaListVm>>(lista);
            return View(listaVm);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            var venta = _servicio.GetVentasPorId(id.Value);
            if (venta == null)
            {
                return HttpNotFound("Código no encontrado");
            }
            var detalle = _servicio.GetVentaCarrito(id.Value);
            var detalleVm = _mapper.Map<List<VentaCarritoListVm>>(detalle);
            var ventaVm = new VentaDetailVm
            {
                Venta = _mapper.Map<VentaListVm>(venta),
                Detalles = detalleVm
            };
            return View(ventaVm);
        }
    }
}