using AutoMapper;
using Botines.Entidades.Entidades;
using Botines.Servicios.Interfaces;
using Botines.Servicios.Servicios;
using Botines.Web.App_Start;
using Botines.Web.ViewModels.Localidad;
using Botines.Web.ViewModels.Provincia;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Botines.Web.Controllers
{
    public class LocalidadesController : Controller
    {
        // GET: Localidades
        private readonly IServiciosLocalidades _servicio;
        private readonly IServiciosProvincias _serviciosProvincias;
        private readonly IMapper _mapper;

        public LocalidadesController(IServiciosLocalidades servicio, IServiciosProvincias serviciosProvincias)
        {
            _servicio = servicio;
            _mapper = AutoMapperConfig.Mapper;
            _serviciosProvincias = serviciosProvincias;
        }
        public ActionResult Index(int? page, int? pageSize)
        {
            var lista = _servicio.GetLocalidades();
            var listaVm = _mapper.Map<List<LocalidadListVm>>(lista);

            page = page ?? 1;
            pageSize = pageSize ?? 3;
            ViewBag.PageSize = pageSize;

            return View(listaVm.ToPagedList(page.Value, pageSize.Value));
        }

        public ActionResult Create()
        {

            var localidadVm = new LocalidadEditVm()
            {
                Provincias = _serviciosProvincias.GetProvinciasDropDownList(),
            };
            return View(localidadVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LocalidadEditVm localidadVm)
        {
            if (!ModelState.IsValid)
            {

                localidadVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();
                return View(localidadVm);
            }
            var localidad = _mapper.Map<Localidad>(localidadVm);
            if (_servicio.Existe(localidad))
            {

                localidadVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();

                ModelState.AddModelError(string.Empty, "Localidad existente!!!");
                return View(localidadVm);
            }
            _servicio.Guardar(localidad);
            TempData["Msg"] = "Registro guardado satisfactoriamente";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var localidad = _servicio.GetLocalidadPorId(id.Value);
            if (localidad == null)
            {
                return HttpNotFound("Código de localidad inexistente!!!");
            }
            var localidadVm = _mapper.Map<LocalidadListVm>(localidad);
            return View(localidadVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var localidad = _servicio.GetLocalidadPorId(id);
            var localidadVm = _mapper.Map<LocalidadListVm>(localidad);
            try
            {
                if (!_servicio.EstaRelacionada(localidad))
                {
                    _servicio.Borrar(localidad.LocalidadId);
                    TempData["Msg"] = "Registro borrado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Localidad relacionada... Baja denegada!!");
                    return View(localidadVm);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Error al intentar borrar un registro de localidades");
                return View(localidadVm);

            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var localidad = _servicio.GetLocalidadPorId(id.Value);
            if (localidad == null)
            {
                return HttpNotFound("Código de localidad inexistente!!!");
            }
            var localidadVm = _mapper.Map<LocalidadEditVm>(localidad);


            localidadVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();
            return View(localidadVm);

        }
        [HttpPost]
        public ActionResult Edit(LocalidadEditVm localidadVm)
        {
            if (!ModelState.IsValid)
            {
                localidadVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();
                return View(localidadVm);
            }
            try
            {
                var localidad = _mapper.Map<Localidad>(localidadVm);
                if (!_servicio.Existe(localidad))
                {
                    _servicio.Guardar(localidad);
                    TempData["Msg"] = "Registro editado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    localidadVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();

                    ModelState.AddModelError(string.Empty, "Localidad existente!!!");
                    return View(localidadVm);
                }
            }
            catch (Exception)
            {
                localidadVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();

                ModelState.AddModelError(string.Empty, "Localidad existente!!!");
                return View(localidadVm);
            }
        }


    }
}