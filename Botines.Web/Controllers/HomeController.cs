using Botines.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Botines.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiciosMarcas _serviciosMarcas;
        private readonly IServiciosModelos _serviciosModelos;
        private readonly IServiciosTalles _serviciosTalles;
        private readonly IServiciosTallesBotines _serviciosTallesBotines;
        public HomeController(IServiciosMarcas serviciosMarcas, IServiciosModelos serviciosModelos, 
            IServiciosTalles serviciosTalles, IServiciosTallesBotines serviciosTallesBotines)
        {
            _serviciosMarcas = serviciosMarcas;
            _serviciosModelos = serviciosModelos;
            _serviciosTalles = serviciosTalles;
            _serviciosTallesBotines = serviciosTallesBotines;

        }
        public ActionResult Index()
        {
            var cantidadMarcas = _serviciosMarcas.GetCantidad();
            var cantidadModelos = _serviciosModelos.GetCantidad();
            var cantidadTalles = _serviciosTalles.GetCantidad();
            var cantidadTallesBotines = _serviciosTallesBotines.GetCantidadStock();

            ViewBag.cantidadTallesBotines = cantidadTallesBotines;
            ViewBag.cantidadTalles = cantidadTalles;
            ViewBag.cantidadMarcas = cantidadMarcas;
            ViewBag.cantidadModelos = cantidadModelos;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}