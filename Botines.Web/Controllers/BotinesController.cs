using AutoMapper;
using Botines.Entidades.Dtos.Botin;
using Botines.Entidades.Dtos.Talle;
using Botines.Entidades.Entidades;
using Botines.Servicios.Interfaces;
using Botines.Utilidades;
using Botines.Web.App_Start;
using Botines.Web.ViewModels.Botin;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Botines.Web.Controllers
{
    public class BotinesController : Controller
    {
        // GET: Botines
        private readonly IServiciosBotines _servicio;
        private readonly IServiciosMarcas _serviciosMarcas;
        private readonly IServiciosModelos _serviciosModelos;
        private readonly IServiciosProveedores _serviciosProveedores;
        private readonly IMapper _mapper;


        public BotinesController(IServiciosBotines servicio, IServiciosMarcas serviciosMarcas, 
                    IServiciosModelos serviciosModelos, IServiciosProveedores serviciosProveedores)
        {
            _servicio = servicio;
            _mapper = AutoMapperConfig.Mapper;
            _serviciosMarcas = serviciosMarcas;
            _serviciosModelos = serviciosModelos;
            _serviciosProveedores = serviciosProveedores;
        }

        public ActionResult Index(int? page, int? pageSize)
        {
            var lista = _servicio.GetBotines();
            var listaVm = _mapper.Map<List<BotinListVm>>(lista);

            page = page ?? 1;
            pageSize = pageSize ?? 3;
            ViewBag.PageSize = pageSize;


            return View(listaVm.ToPagedList(page.Value, pageSize.Value));
        }

        public ActionResult Create()
        {

            var botinVm = new BotinEditVm()
            {
                Marcas = _serviciosMarcas.GetMarcasDropDownList(),
                Modelos = _serviciosModelos.GetModelosDropDownList(),
                Proveedores = _serviciosProveedores.GetProveedoresDropDownList(),
            };
            return View(botinVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BotinEditVm botinVm)
        {
            if (!ModelState.IsValid)
            {

                botinVm.Marcas = _serviciosMarcas.GetMarcasDropDownList();
                botinVm.Modelos = _serviciosModelos.GetModelosDropDownList();
                botinVm.Proveedores = _serviciosProveedores.GetProveedoresDropDownList();
                
                return View(botinVm);
            }
            var botin = _mapper.Map<Botin>(botinVm);
            if (_servicio.Existe(botin))
            {

                botinVm.Marcas = _serviciosMarcas.GetMarcasDropDownList();
                botinVm.Modelos = _serviciosModelos.GetModelosDropDownList();
                botinVm.Proveedores = _serviciosProveedores.GetProveedoresDropDownList();

                ModelState.AddModelError(string.Empty, "Botin existente!!!");
                return View(botinVm);
            }
            if (botinVm.imagenFile != null)
            {
                string extension = Path.GetExtension(botinVm.imagenFile.FileName);
                string filename = Guid.NewGuid().ToString();

                var file = $"{filename}{extension}";
                var response = FileHelper.UploadPhoto(botinVm.imagenFile, WC.BotinesImagenesFolder, file);
                botin.Imagen = file;
            }


            _servicio.Guardar(botin);
            TempData["Msg"] = "Registro guardado satisfactoriamente";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var botin = _servicio.GetBotinPorId(id.Value);
            if (botin == null)
            {
                return HttpNotFound("Código de botin inexistente!!!");
            }
            var botinVm = _mapper.Map<BotinListVm>(botin);
            return View(botinVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var botin = _servicio.GetBotinPorId(id);
            var botinVm = _mapper.Map<BotinListVm>(botin);
            try
            {
                if (!_servicio.EstaRelacionada(botin))
                {
                    _servicio.Borrar(botin.BotinId);
                    TempData["Msg"] = "Registro borrado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Botin relacionado... Baja denegada!!");
                    return View(botinVm);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Error al intentar borrar un registro de botines");
                return View(botinVm);

            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var botin = _servicio.GetBotinPorId(id.Value);
            if (botin == null)
            {
                return HttpNotFound("Código de botin inexistente!!!");
            }
            var botinVm = _mapper.Map<BotinEditVm>(botin);


            botinVm.Marcas = _serviciosMarcas.GetMarcasDropDownList();
            botinVm.Modelos = _serviciosModelos.GetModelosDropDownList();
            botinVm.Proveedores = _serviciosProveedores.GetProveedoresDropDownList();
            return View(botinVm);

        }
        [HttpPost]
        public ActionResult Edit(BotinEditVm botinVm)
        {
            if (!ModelState.IsValid)
            {
                botinVm.Marcas = _serviciosMarcas.GetMarcasDropDownList();
                botinVm.Modelos = _serviciosModelos.GetModelosDropDownList();
                botinVm.Proveedores = _serviciosProveedores.GetProveedoresDropDownList();
                return View(botinVm);
            }
            try
            {
                var botin = _mapper.Map<Botin>(botinVm);
                if (!_servicio.Existe(botin))
                {

                    if (botinVm.imagenFile != null)
                    {
                        string extension = Path.GetExtension(botinVm.imagenFile.FileName);
                        string filename = Guid.NewGuid().ToString();

                        var file = $"{filename}{extension}";
                        var response = FileHelper.UploadPhoto(botinVm.imagenFile, WC.BotinesImagenesFolder, file);
                        botin.Imagen = file;
                    }

                    _servicio.Guardar(botin);
                    TempData["Msg"] = "Registro editado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    botinVm.Marcas = _serviciosMarcas.GetMarcasDropDownList();
                    botinVm.Modelos = _serviciosModelos.GetModelosDropDownList();
                    botinVm.Proveedores = _serviciosProveedores.GetProveedoresDropDownList();

                    ModelState.AddModelError(string.Empty, "Botin existente!!!");
                    return View(botinVm);
                }
            }
            catch (Exception)
            {
                botinVm.Marcas = _serviciosMarcas.GetMarcasDropDownList();
                botinVm.Modelos = _serviciosModelos.GetModelosDropDownList();
                botinVm.Proveedores = _serviciosProveedores.GetProveedoresDropDownList();

                ModelState.AddModelError(string.Empty, "Botin existente!!!");
                return View(botinVm);
            }
        }


        public ActionResult BotinTalles(int? id)
        {
            var lista = _servicio.GetTallesBotines();
            BotinListDto botinList = new BotinListDto();
            foreach (var b in lista)
            {
                if (b.BotinId == id)
                {
                    botinList = b;
                }
            }

            return View(botinList);
        }


    }
}