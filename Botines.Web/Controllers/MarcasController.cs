using AutoMapper;
using Botines.Entidades.Entidades;
using Botines.Servicios.Interfaces;
using Botines.Utilidades;
using Botines.Web.App_Start;
using Botines.Web.ViewModels.Marca;
using Botines.Web.ViewModels.Modelo;
using Botines.Web.ViewModels.Pais;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Botines.Web.Controllers
{
    public class MarcasController : Controller
    {
        // GET: Marcas
        private readonly IServiciosMarcas _servicios;
        private readonly IServiciosModelos _serviciosModelos;
        private readonly IMapper _mapper;

        public MarcasController(IServiciosMarcas servicios, IServiciosModelos serviciosModelos)
        {
            _servicios = servicios;
            _serviciosModelos = serviciosModelos;
            _mapper = AutoMapperConfig.Mapper;
        }

        public ActionResult Index(int? page, int? pageSize)
        {
            var lista = _servicios.GetMarcas();

            var listaVm = _mapper.Map<List<MarcaListVm>>(lista);

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
        public ActionResult Create(MarcaEditVm marcaVm)
        {
            if (ModelState.IsValid)
            {
                var marca = _mapper.Map<Marca>(marcaVm);
                if (_servicios.Existe(marca))
                {
                    ModelState.AddModelError(string.Empty, "Marca existente!!!");
                    return View(marca);
                }
                if (marcaVm.imagenFile != null)
                {
                    string extension = Path.GetExtension(marcaVm.imagenFile.FileName);
                    string filename = Guid.NewGuid().ToString();

                    var file = $"{filename}{extension}";
                    var response = FileHelper.UploadPhoto(marcaVm.imagenFile, WC.MarcasImagenesFolder, file);
                    marca.Imagen = file;
                }

                _servicios.Guardar(marca);
                TempData["Msg"] = "Registro guardado satisfactoriamente";
                return RedirectToAction("Index");
            }
            else
            {
                return View(marcaVm);
            }
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var marca = _servicios.GetMarcaPorId(id.Value);
            if (marca == null)
            {
                return HttpNotFound("Código de marca inesistente!!!");
            }
            var marcaVm = _mapper.Map<MarcaListVm>(marca);
            return View(marcaVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfim(int id)
        {
            var marca = _servicios.GetMarcaPorId(id);
            if (_servicios.EstaRelacionado(marca))
            {
                var marcaVm = _mapper.Map<MarcaListVm>(marca);
                ModelState.AddModelError(string.Empty, "Marca relacionado... Baja denegada!!!");
                return View(marcaVm);

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
            var marca = _servicios.GetMarcaPorId(id.Value);
            if (marca == null)
            {
                return HttpNotFound("Código de marca inesistente!!!");
            }
            var marcaVm = _mapper.Map<MarcaEditVm>(marca);
            return View(marcaVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MarcaEditVm marcaVm)
        {
            if (!ModelState.IsValid)
            {
                return View(marcaVm);
            }
            var marca = _mapper.Map<Marca>(marcaVm);
            if (_servicios.Existe(marca))
            {
                ModelState.AddModelError(string.Empty, "Marca existente!!!");
                return View(marcaVm);
            }
            if (marcaVm.imagenFile != null)
            {
                string extension = Path.GetExtension(marcaVm.imagenFile.FileName);
                string filename = Guid.NewGuid().ToString();

                var file = $"{filename}{extension}";
                var response = FileHelper.UploadPhoto(marcaVm.imagenFile, WC.MarcasImagenesFolder, file);
                marca.Imagen = file;
            }
            _servicios.Guardar(marca);
            TempData["Msg"] = "Registro editado satisfactoriamente!!!";
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var marca = _servicios.GetMarcaPorId(id.Value);
            if (marca == null)
            {
                return HttpNotFound("Código de marca inesistente!!!");
            }
            var marcaVm = _mapper.Map<MarcaListVm>(marca);
            marcaVm.CantidadModelos = _serviciosModelos
                .GetCantidad(c => c.MarcaId == marcaVm.MarcaId);
            var marcaDetailVm = new MarcaDetailVm()
            {
                Marca = marcaVm,
                Modelos = _mapper.Map<List<ModeloListVm>>(
                _serviciosModelos.GetModelos(marca.MarcaId))
            };
            return View(marcaDetailVm);
        }


    }
}