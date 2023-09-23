using AutoMapper;
using Botines.Entidades.Entidades;
using Botines.Servicios.Interfaces;
using Botines.Web.App_Start;
using Botines.Web.ViewModels.Proveedor;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Botines.Web.Controllers
{
    public class ProveedoresController : Controller
    {
        // GET: Proveedores
        private readonly IServiciosProveedores _servicio;
        private readonly IServiciosPaises _serviciosPaises;
        private readonly IServiciosProvincias _serviciosProvincias;
        private readonly IServiciosLocalidades _serviciosLocalidades;
        private readonly IMapper _mapper;


        public ProveedoresController(IServiciosProveedores servicio, IServiciosPaises serviciosPaises, 
                                    IServiciosProvincias serviciosProvincias, IServiciosLocalidades serviciosLocalidades)
        {
            _servicio = servicio;
            _mapper = AutoMapperConfig.Mapper;
            _serviciosPaises = serviciosPaises;
            _serviciosProvincias = serviciosProvincias;
            _serviciosLocalidades = serviciosLocalidades;

        }

        public ActionResult Index(int? page, int? pageSize)
        {
            var lista = _servicio.GetProveedores();
            var listaVm = _mapper.Map<List<ProveedorListVm>>(lista);

            page = page ?? 1;
            pageSize = pageSize ?? 5;
            ViewBag.PageSize = pageSize;

            return View(listaVm.ToPagedList(page.Value, pageSize.Value));
        }

        public ActionResult Create()
        {

            var proveedorVm = new ProveedorEditVm()
            {
                Paises = _serviciosPaises.GetPaisesDropDownList(),
                Provincias=_serviciosProvincias.GetProvinciasDropDownList(),
                Localidades=_serviciosLocalidades.GetLocalidadesDropDownList(),
            };
            return View(proveedorVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProveedorEditVm proveedorVm)
        {
            if (!ModelState.IsValid)
            {

                proveedorVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                proveedorVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();
                proveedorVm.Localidades = _serviciosLocalidades.GetLocalidadesDropDownList();
                return View(proveedorVm);
            }
            var proveedor = _mapper.Map<Proveedor>(proveedorVm);
            if (_servicio.Existe(proveedor))
            {

                proveedorVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                proveedorVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();
                proveedorVm.Localidades = _serviciosLocalidades.GetLocalidadesDropDownList();

                ModelState.AddModelError(string.Empty, "Proveedor existente!!!");
                return View(proveedorVm);
            }
            _servicio.Guardar(proveedor);
            TempData["Msg"] = "Registro guardado satisfactoriamente";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var proveedor = _servicio.GetProveedorPorId(id.Value);
            if (proveedor == null)
            {
                return HttpNotFound("Código de proveedor inexistente!!!");
            }
            var proveedorVm = _mapper.Map<ProveedorListVm>(proveedor);
            return View(proveedorVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var proveedor = _servicio.GetProveedorPorId(id);
            var proveedorVm = _mapper.Map<ProveedorListVm>(proveedor);
            try
            {
                if (!_servicio.EstaRelacionada(proveedor))
                {
                    _servicio.Borrar(proveedor.ProveedorId);
                    TempData["Msg"] = "Registro borrado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Proveedor relacionada... Baja denegada!!");
                    return View(proveedorVm);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Error al intentar borrar un registro de proveedores");
                return View(proveedorVm);

            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var proveedor = _servicio.GetProveedorPorId(id.Value);
            if (proveedor == null)
            {
                return HttpNotFound("Código de proveedor inexistente!!!");
            }
            var proveedorVm = _mapper.Map<ProveedorEditVm>(proveedor);


            proveedorVm.Paises = _serviciosPaises.GetPaisesDropDownList();
            proveedorVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();
            proveedorVm.Localidades = _serviciosLocalidades.GetLocalidadesDropDownList();
            return View(proveedorVm);

        }
        [HttpPost]
        public ActionResult Edit(ProveedorEditVm proveedorVm)
        {
            if (!ModelState.IsValid)
            {
                proveedorVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                proveedorVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();
                proveedorVm.Localidades = _serviciosLocalidades.GetLocalidadesDropDownList();
                return View(proveedorVm);
            }
            try
            {
                var proveedor = _mapper.Map<Proveedor>(proveedorVm);
                if (!_servicio.Existe(proveedor))
                {
                    _servicio.Guardar(proveedor);
                    TempData["Msg"] = "Registro editado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    proveedorVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                    proveedorVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();
                    proveedorVm.Localidades = _serviciosLocalidades.GetLocalidadesDropDownList();

                    ModelState.AddModelError(string.Empty, "Proveedor existente!!!");
                    return View(proveedorVm);
                }
            }
            catch (Exception)
            {
                proveedorVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                proveedorVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();
                proveedorVm.Localidades = _serviciosLocalidades.GetLocalidadesDropDownList();

                ModelState.AddModelError(string.Empty, "Proveedor existente!!!");
                return View(proveedorVm);
            }
        }



    }
}