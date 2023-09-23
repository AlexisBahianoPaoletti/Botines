using AutoMapper;
using Botines.Entidades.Dtos.Talle;
using Botines.Entidades.Entidades;
using Botines.Servicios.Interfaces;
using Botines.Web.App_Start;
using Botines.Web.ViewModels.Talle;
using Microsoft.Ajax.Utilities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Botines.Web.Controllers
{
    public class TallesController : Controller
    {
        // GET: Talles
        private readonly IServiciosTalles _servicios;
        private readonly IMapper _mapper;

        public TallesController(IServiciosTalles servicios)
        {
            _servicios = servicios;
            _mapper = AutoMapperConfig.Mapper;
        }
        public ActionResult Index(int? page, int? pageSize)
        {
            var lista = _servicios.GetTallesBotines();
            
            var listaVm = _mapper.Map<List<TalleListVm>>(lista);
            foreach (var t in listaVm)
            {
                foreach (var tl in lista)
                {
                    if (t.TalleId==tl.TalleId)
                    {
                        t.Cantidad = tl.Botines.Count();
                    }
                }
            }

            page = page ?? 1;
            pageSize = pageSize ?? 4;
            ViewBag.PageSize = pageSize;


            return View(listaVm.ToPagedList(page.Value, pageSize.Value));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TalleEditVm talleVm)
        {
            if (ModelState.IsValid)
            {
                var talle = _mapper.Map<Talle>(talleVm);
                if (_servicios.Existe(talle))
                {
                    ModelState.AddModelError(string.Empty, "Talle existente!!!");
                    return View(talle);
                }
                _servicios.Guardar(talle);
                TempData["Msg"] = "Registro guardado satisfactoriamente";
                return RedirectToAction("Index");
            }
            else
            {
                return View(talleVm);
            }
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var talle = _servicios.GetTallePorId(id.Value);
            if (talle == null)
            {
                return HttpNotFound("Código de talle inesistente!!!");
            }
            var talleVm = _mapper.Map<TalleListVm>(talle);
            return View(talleVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfim(int id)
        {
            var talle = _servicios.GetTallePorId(id);
            if (_servicios.EstaRelacionado(talle))
            {
                var talleVm = _mapper.Map<TalleListVm>(talle);
                ModelState.AddModelError(string.Empty, "Talle relacionado... Baja denegada!!!");
                return View(talleVm);

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
            var talle = _servicios.GetTallePorId(id.Value);
            if (talle == null)
            {
                return HttpNotFound("Código de talle inesistente!!!");
            }
            var talleVm = _mapper.Map<TalleEditVm>(talle);
            return View(talleVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TalleEditVm talleVm)
        {
            if (!ModelState.IsValid)
            {
                return View(talleVm);
            }
            var talle = _mapper.Map<Talle>(talleVm);
            if (_servicios.Existe(talle))
            {
                ModelState.AddModelError(string.Empty, "Talle existente!!!");
                return View(talleVm);
            }
            _servicios.Guardar(talle);
            TempData["Msg"] = "Registro editado satisfactoriamente!!!";
            return RedirectToAction("Index");
        }


        public ActionResult TalleBotines(int? id)
        {
            var lista = _servicios.GetTallesBotines();
            TalleListDto talleList = new TalleListDto();
            foreach (var t in lista)
            {
                if (t.TalleId==id)
                {
                    talleList = t;
                }
            }

            return View(talleList);
        }


    }
}