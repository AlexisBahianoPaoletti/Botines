using AutoMapper;
using Botines.Entidades.Entidades;
using Botines.Servicios.Interfaces;
using Botines.Web.App_Start;
using Botines.Web.ViewModels.Pais;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Botines.Web.Controllers
{
    
    public class PaisesController : Controller
    {
        // GET: Paises
        private readonly IServiciosPaises _servicios;
        private readonly IMapper _mapper;

        public PaisesController(IServiciosPaises servicios)
        {
            _servicios = servicios;
            _mapper = AutoMapperConfig.Mapper;
        }
        public ActionResult Index(int? page, int? pageSize)
        {
            var lista = _servicios.GetPaises();
            //var lista = new List<Pais>();
            //var listaVm = GetListaPaisesLstVm(lista);
            var listaVm = _mapper.Map<List<PaisListVm>>(lista);

            page = page ?? 1;
            pageSize = pageSize ?? 2;
            ViewBag.PageSize = pageSize;

            return View(listaVm.ToPagedList(page.Value, pageSize.Value));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaisEditVm paisVm)
        {
            if (ModelState.IsValid)
            {
                var pais = _mapper.Map<Pais>(paisVm);
                if (_servicios.Existe(pais))
                {
                    ModelState.AddModelError(string.Empty, "País existente!!!");
                    return View(pais);
                }
                _servicios.Guardar(pais);
                TempData["Msg"] = "Registro guardado satisfactoriamente";
                return RedirectToAction("Index");
            }
            else
            {
                return View(paisVm);
            }
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var pais = _servicios.GetPaisPorId(id.Value);
            if (pais == null)
            {
                return HttpNotFound("Código de país inesistente!!!");
            }
            var paisVm = _mapper.Map<PaisListVm>(pais);
            return View(paisVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfim(int id)
        {
            var pais = _servicios.GetPaisPorId(id);
            if (_servicios.EstaRelacionado(pais))
            {
                var paisVm = _mapper.Map<PaisListVm>(pais);
                ModelState.AddModelError(string.Empty, "País relacionado... Baja denegada!!!");
                return View(paisVm);

            }
            _servicios.Borrar(id);
            TempData["Msg"] = "Registro borrado satisfactoriamente!!!";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var pais = _servicios.GetPaisPorId(id.Value);
            if (pais == null)
            {
                return HttpNotFound("Código de país inesistente!!!");
            }
            var paisVm = _mapper.Map<PaisEditVm>(pais);
            return View(paisVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PaisEditVm paisVm)
        {
            if (!ModelState.IsValid)
            {
                return View(paisVm);
            }
            var pais = _mapper.Map<Pais>(paisVm);
            if (_servicios.Existe(pais))
            {
                ModelState.AddModelError(string.Empty, "País existente!!!");
                return View(paisVm);
            }
            _servicios.Guardar(pais);
            TempData["Msg"] = "Registro editado satisfactoriamente!!!";
            return RedirectToAction("Index");

        }

    }
}