using AutoMapper;
using Botines.Entidades.Entidades;
using Botines.Servicios.Interfaces;
using Botines.Web.App_Start;
using Botines.Web.ViewModels.Provincia;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Botines.Web.Controllers
{
    public class ProvinciasController : Controller
    {
        // GET: Provincias
        private readonly IServiciosProvincias _servicio;
        private readonly IServiciosPaises _serviciosPaises;
        private readonly IMapper _mapper;


        public ProvinciasController(IServiciosProvincias servicio, IServiciosPaises serviciosPaises)
        {
            _servicio = servicio;
            _mapper = AutoMapperConfig.Mapper;
            _serviciosPaises = serviciosPaises;
        }

        public ActionResult Index(int? page, int? pageSize)
        {
            var lista = _servicio.GetProvincias();
            var listaVm = _mapper.Map<List<ProvinciaListVm>>(lista);

            page = page ?? 1;
            pageSize = pageSize ?? 3;
            ViewBag.PageSize = pageSize;

            return View(listaVm.ToPagedList(page.Value, pageSize.Value));
        }

        public ActionResult Create()
        {

            var provinciaVm = new ProvinciaEditVm()
            {
                Paises = _serviciosPaises.GetPaisesDropDownList(),
            };
            return View(provinciaVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProvinciaEditVm provinciaVm)
        {
            if (!ModelState.IsValid)
            {

                provinciaVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                return View(provinciaVm);
            }
            var provincia = _mapper.Map<Provincia>(provinciaVm);
            if (_servicio.Existe(provincia))
            {

                provinciaVm.Paises = _serviciosPaises.GetPaisesDropDownList();

                ModelState.AddModelError(string.Empty, "Provincia existente!!!");
                return View(provinciaVm);
            }
            _servicio.Guardar(provincia);
            TempData["Msg"] = "Registro guardado satisfactoriamente";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var provincia = _servicio.GetProvinciaPorId(id.Value);
            if (provincia == null)
            {
                return HttpNotFound("Código de provincia inexistente!!!");
            }
            var provinciaVm = _mapper.Map<ProvinciaListVm>(provincia);
            return View(provinciaVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var provincia = _servicio.GetProvinciaPorId(id);
            var provinciaVm = _mapper.Map<ProvinciaListVm>(provincia);
            try
            {
                if (!_servicio.EstaRelacionada(provincia))
                {
                    _servicio.Borrar(provincia.ProvinciaId);
                    TempData["Msg"] = "Registro borrado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Provincia relacionada... Baja denegada!!");
                    return View(provinciaVm);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Error al intentar borrar un registro de provincias");
                return View(provinciaVm);

            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var provincia = _servicio.GetProvinciaPorId(id.Value);
            if (provincia == null)
            {
                return HttpNotFound("Código de provincia inexistente!!!");
            }
            var provinciaVm = _mapper.Map<ProvinciaEditVm>(provincia);


            provinciaVm.Paises = _serviciosPaises.GetPaisesDropDownList();
            return View(provinciaVm);

        }
        [HttpPost]
        public ActionResult Edit(ProvinciaEditVm provinciaVm)
        {
            if (!ModelState.IsValid)
            {
                provinciaVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                return View(provinciaVm);
            }
            try
            {
                var provincia = _mapper.Map<Provincia>(provinciaVm);
                if (!_servicio.Existe(provincia))
                {
                    _servicio.Guardar(provincia);
                    TempData["Msg"] = "Registro editado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    provinciaVm.Paises = _serviciosPaises.GetPaisesDropDownList();

                    ModelState.AddModelError(string.Empty, "Provincia existente!!!");
                    return View(provinciaVm);
                }
            }
            catch (Exception)
            {
                provinciaVm.Paises = _serviciosPaises.GetPaisesDropDownList();

                ModelState.AddModelError(string.Empty, "Provincia existente!!!");
                return View(provinciaVm);
            }
        }

    }
}