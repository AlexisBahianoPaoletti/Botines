using AutoMapper;
using Botines.Entidades.Entidades;
using Botines.Servicios.Interfaces;
using Botines.Web.App_Start;
using Botines.Web.ViewModels.Modelo;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Botines.Web.Controllers
{
    public class ModelosController : Controller
    {
        // GET: Modelos
        private readonly IServiciosModelos _servicio;
        private readonly IServiciosMarcas _serviciosMarcas;
        private readonly IMapper _mapper;


        public ModelosController(IServiciosModelos servicio, IServiciosMarcas serviciosMarcas)
        {
            _servicio = servicio;
            _mapper = AutoMapperConfig.Mapper;
            _serviciosMarcas = serviciosMarcas;
        }

        public ActionResult Index(int? page, int? pageSize)
        {
            var lista = _servicio.GetModelos();
            var listaVm = _mapper.Map<List<ModeloListVm>>(lista);

            page = page ?? 1;
            pageSize = pageSize ?? 3;
            ViewBag.PageSize = pageSize;

            return View(listaVm.ToPagedList(page.Value, pageSize.Value));
        }

        public ActionResult Create()
        {

            var modeloVm = new ModeloEditVm()
            {
                Marcas = _serviciosMarcas.GetMarcasDropDownList(),
            };
            return View(modeloVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ModeloEditVm modeloVm)
        {
            if (!ModelState.IsValid)
            {

                modeloVm.Marcas = _serviciosMarcas.GetMarcasDropDownList();
                return View(modeloVm);
            }
            var modelo = _mapper.Map<Modelo>(modeloVm);
            if (_servicio.Existe(modelo))
            {

                modeloVm.Marcas = _serviciosMarcas.GetMarcasDropDownList();

                ModelState.AddModelError(string.Empty, "Modelo existente!!!");
                return View(modeloVm);
            }
            _servicio.Guardar(modelo);
            TempData["Msg"] = "Registro guardado satisfactoriamente";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var modelo = _servicio.GetModeloPorId(id.Value);
            if (modelo == null)
            {
                return HttpNotFound("Código de modelo inexistente!!!");
            }
            var modeloVm = _mapper.Map<ModeloListVm>(modelo);
            return View(modeloVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var modelo = _servicio.GetModeloPorId(id);
            var modeloVm = _mapper.Map<ModeloListVm>(modelo);
            try
            {
                if (!_servicio.EstaRelacionada(modelo))
                {
                    _servicio.Borrar(modelo.ModeloId);
                    TempData["Msg"] = "Registro borrado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Modelo relacionada... Baja denegada!!");
                    return View(modeloVm);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Error al intentar borrar un registro de modelos");
                return View(modeloVm);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var modelo = _servicio.GetModeloPorId(id.Value);
            if (modelo == null)
            {
                return HttpNotFound("Código de modelo inexistente!!!");
            }
            var modeloVm = _mapper.Map<ModeloEditVm>(modelo);


            modeloVm.Marcas = _serviciosMarcas.GetMarcasDropDownList();
            return View(modeloVm);

        }
        [HttpPost]
        public ActionResult Edit(ModeloEditVm modeloVm)
        {
            if (!ModelState.IsValid)
            {
                modeloVm.Marcas = _serviciosMarcas.GetMarcasDropDownList();
                return View(modeloVm);
            }
            try
            {
                var modelo = _mapper.Map<Modelo>(modeloVm);
                if (!_servicio.Existe(modelo))
                {
                    _servicio.Guardar(modelo);
                    TempData["Msg"] = "Registro editado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    modeloVm.Marcas = _serviciosMarcas.GetMarcasDropDownList();

                    ModelState.AddModelError(string.Empty, "Modelo existente!!!");
                    return View(modeloVm);
                }
            }
            catch (Exception)
            {
                modeloVm.Marcas = _serviciosMarcas.GetMarcasDropDownList();

                ModelState.AddModelError(string.Empty, "Modelo existente!!!");
                return View(modeloVm);
            }
        }

    }
}