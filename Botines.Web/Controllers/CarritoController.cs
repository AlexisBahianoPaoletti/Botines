using AutoMapper;
using Botines.Entidades.Dtos.Botin;
using Botines.Entidades.Entidades;
using Botines.Entidades.Enums;
using Botines.Servicios.Interfaces;
using Botines.Servicios.Servicios;
using Botines.Web.App_Start;
using Botines.Web.Models.Carrito;
using Botines.Web.ViewModels.Carrito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Botines.Web.Controllers
{
    public class CarritoController : Controller
    {
        // GET: Carrito
        // GET: Carrito
        private readonly IServiciosCarrito _serviciosCarritos;
        private readonly IServiciosTallesBotines _serviciosTallesBotines;
        private readonly IServiciosBotines _serviciosBotines;
        private readonly IServiciosClientes _serviciosClientes;
        private readonly IServiciosVentas _serviciosVentas;
        private readonly IMapper _mapper;
        public CarritoController(IServiciosTallesBotines serviciosTallesBotines,
            IServiciosCarrito serviciosCarritos,
            IServiciosClientes serviciosClientes,
            IServiciosBotines serviciosBotines,
            IServiciosVentas serviciosVentas)
        {
            _serviciosTallesBotines = serviciosTallesBotines;
            _mapper = AutoMapperConfig.Mapper;
            _serviciosCarritos = serviciosCarritos;
            _serviciosClientes = serviciosClientes;
            _serviciosBotines = serviciosBotines;
            _serviciosVentas = serviciosVentas;
        }

        private Carrito _carrito;
        [Authorize]
        public ActionResult Index(string returnUrl)
        {
            _carrito = GetCarrito();
            //if (_carrito.GetCantidad() == 0)
            //{
            //    return View("EmptyCart");
            //}
            var listaItem = _carrito.GetItems();
            var listaItemVm = _mapper.Map<List<ItemCarritoVm>>(listaItem);

            CarritoVm carritoVm = new CarritoVm
            {
                Items = listaItemVm,
                returnUrl = returnUrl
            };
            return View(carritoVm);
        }
        //public ActionResult MostrarCarrito()
        //{
        //    _carrito = GetCarrito();

        //    return PartialView("_MostrarCarrito", _carrito);
        //}

        private Carrito GetCarrito()
        {
            if (_carrito != null)
            {
                return _carrito;
            }
            _carrito = (Carrito)Session["carrito"];
            if (_carrito == null)
            {
                _carrito = new Carrito();
                Session["carrito"] = _carrito;
            }

            if (_carrito.GetCantidad() == 0)
            {
                if (_serviciosCarritos.GetCantidad(User.Identity.Name) > 0)
                {
                    var listaItemsCarrito = _serviciosCarritos
                        .GetCarrito(User.Identity.Name);
                    foreach (var item in listaItemsCarrito)
                    {
                        _carrito.AddToCart(item);
                    }
                    Session["carrito"] = _carrito;
                }

            }
            return _carrito;
        }

        [HttpPost]
        public ActionResult AddToCart(int botinId, int talleId, string returnUrl/*, int cantidad = 1*/)
        {
            //var botin = _serviciosBotines.GetBotinPorId(botinId);
            TalleBotin talleBotin = _serviciosTallesBotines.GetTalleBotinPorId2(botinId, talleId);
            //if (productoDto.Stock >= cantidad)
            //{
                ItemCarrito itemCarrito = new ItemCarrito
                {
                    NombreBotin = talleBotin.Botin.NombreBotin,
                    UserName = User.Identity.Name,
                    TalleBotinId = talleBotin.TalleBotinId,
                    PrecioUnitario = talleBotin.Botin.PrecioCosto,
                    Cantidad = +1,
                    Estado = false

                };
                _carrito = GetCarrito();
                _carrito.AddToCart(itemCarrito);
                Session["carrito"] = _carrito;
                _serviciosCarritos.Guardar(itemCarrito);
                //    _serviciosProductos.ActualizarUnidadesEnPedido(productoId, cantidad);
                //    return RedirectToAction("Index", new { returnUrl });

                //}
                //else
                //{
                //    TempData["Error"] = "Cantidad mayor al stock";
                //    return RedirectToAction("Index", new { returnUrl });
                //}
            
                return RedirectToAction("Index", new { returnUrl });
        }

        public ActionResult RemoveFromCart(int botinId, string returnUrl)
        {
            _carrito = GetCarrito();
            _carrito.RemoveFromCart(botinId);
            Session["carrito"] = _carrito;
            _serviciosCarritos.Borrar(User.Identity.Name, botinId);
            return RedirectToAction("Index", new { returnUrl });

        }

        public ActionResult CancelOrder()
        {
            //TODO: Hacer lo que falta de la cancelación
            _carrito = GetCarrito();
            foreach (var item in _carrito.GetItems())
            {
                _serviciosCarritos.Borrar(User.Identity.Name, item.TalleBotinId);
            }
            _carrito.Clear();
            Session["carrito"] = _carrito;

            return View();
        }
        [HttpPost]
        public ActionResult ConfirmOrder(CarritoVm carritoVm)
        {
            Venta venta = new Venta();
            venta.Total = carritoVm.Items.Sum(i => i.PrecioTotal);
            venta.Estado = Estado.Paga;
            Cliente cliente = _serviciosClientes.GetClientePorCorreoElectronico(User.Identity.Name);
            venta.ClienteId = cliente.ClienteId;
            venta.Fecha = DateTime.Now;
            List<VentaCarrito> ventasCarritos = new List<VentaCarrito>();
            foreach (var icVm in carritoVm.Items)
            {
                VentaCarrito ventaCarrito = new VentaCarrito();
                ventaCarrito.Cantidad = icVm.Cantidad;
                ventaCarrito.PrecioVenta = icVm.PrecioVenta;
                ventaCarrito.Total = venta.Total;
                ventaCarrito.ItemCarritoId = icVm.ItemCarritoId;
                ventasCarritos.Add(ventaCarrito);
            }
            venta.Detalles = ventasCarritos;
            _serviciosVentas.Guardar(venta);
            _carrito = GetCarrito();
            _carrito.Clear();
            Session["carrito"] = _carrito;
            //        //TODO: Hacer la confirmación de la orden
            //        _carrito = GetCarrito();
            //        if (_carrito.GetCantidad() == 0)
            //        {
            //            return View("EmptyCart");
            //        }
            //        var listaItem = _carrito.GetItems();
            //        var listaItemVm = _mapper.Map<List<ItemCarritoVm>>(listaItem);

            //        CarritoVm carritoVm = new CarritoVm
            //        {
            //            Items = listaItemVm,
            //            returnUrl = returnUrl
            //        };
            //        CheckOutVm model = new CheckOutVm()
            //        {
            //            Carrito = carritoVm,
            //            //Meses = PaymentHelper.GetMeses(),
            //            //Anios = PaymentHelper.GetAnios(),
            //        };
            //        //model.Meses = PaymentHelper.GetMeses();
            //        //model.Anios = PaymentHelper.GetAnios();
            //        return View(model);

            return View(/*"Index","Home"*//*, new { returnUrl }*/);
        }

        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult ConfirmOrder(CheckOutVm model)
        //    {
        //        _carrito = GetCarrito();
        //        var listaItem = _carrito.GetItems();
        //        var listaItemVm = _mapper.Map<List<ItemCarritoVm>>(listaItem);

        //        CarritoVm carritoVm = new CarritoVm
        //        {
        //            Items = listaItemVm,
        //        };
        //        model.Carrito = carritoVm;
        //        if (!ModelState.IsValid)
        //        {
        //            return View(model);
        //        }

        //        PaymentResult resultado = CheckOut(model);
        //        if (resultado.Exitoso)
        //        {
        //            TempData["TransaccionId"] = resultado.TransaccionId;
        //            return RedirectToAction("Complete");
        //        }
        //        ModelState.AddModelError(string.Empty, resultado.Mensaje);
        //        return View(model);

        //    }
        //    private PaymentResult CheckOut(CheckOutVm model)
        //    {
        //        Cliente cliente = _serviciosClientes.GetClientePorEmail(User.Identity.Name);
        //        var venta = new Venta()
        //        {
        //            ClienteId = cliente.Id,
        //            FechaVenta = DateTime.Now,
        //            Estado = Estado.Impaga,
        //            Total = _carrito.GetTotal()
        //        };
        //        foreach (var item in model.Carrito.Items)
        //        {
        //            DetalleVenta detalleVenta = new DetalleVenta()
        //            {

        //                ProductoId = item.ProductoId,
        //                Cantidad = item.Cantidad,
        //                PrecioUnitario = item.PrecioUnitario,

        //            };
        //            venta.Detalles.Add(detalleVenta);
        //        }
        //        //TODO:Autorizar el pago
        //        var checkout = new CheckOut()
        //        {
        //            Venta = venta,
        //            CardNumber = model.CardNumber,
        //            CVV = model.CVV,
        //            Month = model.Month,
        //            Year = model.Year,
        //        };
        //        var gateway = new PaymentGateway();
        //        var resultado = gateway.ProcesarPago(checkout);
        //        if (resultado.Exitoso)
        //        {
        //            _carrito = GetCarrito();
        //            _carrito.Clear();
        //            Session["carrito"] = _carrito;

        //            venta.TransaccionId = resultado.TransaccionId;
        //            _serviciosVentas.Guardar(venta, User.Identity.Name);

        //        }
        //        //TODO:Obtener TransaccionId
        //        return resultado;
        //    }

        //    public ActionResult Complete()
        //    {
        //        ViewBag.TransaccionId = (string)TempData["TransaccionId"];
        //        return View();
        //    }
    }
}