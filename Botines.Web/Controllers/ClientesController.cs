using AutoMapper;
using Botines.Entidades.Entidades;
using Botines.Servicios.Interfaces;
using Botines.Web.App_Start;
using Botines.Web.ViewModels.Cliente;
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
    public class ClientesController : Controller
    {
        // GET: Clientes
        private readonly IServiciosClientes _servicio;
        private readonly IServiciosPaises _serviciosPaises;
        private readonly IServiciosProvincias _serviciosProvincias;
        private readonly IServiciosLocalidades _serviciosLocalidades;
        private readonly IMapper _mapper;


        public ClientesController(IServiciosClientes servicio, IServiciosPaises serviciosPaises,
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
            var lista = _servicio.GetClientes();
            var listaVm = _mapper.Map<List<ClienteListVm>>(lista);

            page = page ?? 1;
            pageSize = pageSize ?? 5;
            ViewBag.PageSize = pageSize;

            return View(listaVm.ToPagedList(page.Value, pageSize.Value));
        }

        public ActionResult Create()
        {

            var clienteVm = new ClienteEditVm()
            {
                Paises = _serviciosPaises.GetPaisesDropDownList(),
                Provincias = _serviciosProvincias.GetProvinciasDropDownList(),
                Localidades = _serviciosLocalidades.GetLocalidadesDropDownList(),
            };
            return View(clienteVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteEditVm clienteVm)
        {
            if (!ModelState.IsValid)
            {

                clienteVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                clienteVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();
                clienteVm.Localidades = _serviciosLocalidades.GetLocalidadesDropDownList();
                return View(clienteVm);
            }
            var cliente = _mapper.Map<Cliente>(clienteVm);
            if (_servicio.Existe(cliente))
            {

                clienteVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                clienteVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();
                clienteVm.Localidades = _serviciosLocalidades.GetLocalidadesDropDownList();

                ModelState.AddModelError(string.Empty, "Cliente existente!!!");
                return View(clienteVm);
            }
            _servicio.Guardar(cliente);
            TempData["Msg"] = "Registro guardado satisfactoriamente";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cliente = _servicio.GetClientePorId(id.Value);
            if (cliente == null)
            {
                return HttpNotFound("Código de cliente inexistente!!!");
            }
            var clienteVm = _mapper.Map<ClienteListVm>(cliente);
            return View(clienteVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var cliente = _servicio.GetClientePorId(id);
            var clienteVm = _mapper.Map<ClienteListVm>(cliente);
            try
            {
                if (!_servicio.EstaRelacionada(cliente))
                {
                    _servicio.Borrar(cliente.ClienteId);
                    TempData["Msg"] = "Registro borrado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Cliente relacionado... Baja denegada!!");
                    return View(clienteVm);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Error al intentar borrar un registro de clientes");
                return View(clienteVm);

            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cliente = _servicio.GetClientePorId(id.Value);
            if (cliente == null)
            {
                return HttpNotFound("Código de cliente inexistente!!!");
            }
            var clienteVm = _mapper.Map<ClienteEditVm>(cliente);


            clienteVm.Paises = _serviciosPaises.GetPaisesDropDownList();
            clienteVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();
            clienteVm.Localidades = _serviciosLocalidades.GetLocalidadesDropDownList();
            return View(clienteVm);

        }
        [HttpPost]
        public ActionResult Edit(ClienteEditVm clienteVm)
        {
            if (!ModelState.IsValid)
            {
                clienteVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                clienteVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();
                clienteVm.Localidades = _serviciosLocalidades.GetLocalidadesDropDownList();
                return View(clienteVm);
            }
            try
            {
                var cliente = _mapper.Map<Cliente>(clienteVm);
                if (!_servicio.Existe(cliente))
                {
                    _servicio.Guardar(cliente);
                    TempData["Msg"] = "Registro editado satisfactoriamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    clienteVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                    clienteVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();
                    clienteVm.Localidades = _serviciosLocalidades.GetLocalidadesDropDownList();

                    ModelState.AddModelError(string.Empty, "Cliente existente!!!");
                    return View(clienteVm);
                }
            }
            catch (Exception)
            {
                clienteVm.Paises = _serviciosPaises.GetPaisesDropDownList();
                clienteVm.Provincias = _serviciosProvincias.GetProvinciasDropDownList();
                clienteVm.Localidades = _serviciosLocalidades.GetLocalidadesDropDownList();

                ModelState.AddModelError(string.Empty, "Cliente existente!!!");
                return View(clienteVm);
            }
        }


    }
}