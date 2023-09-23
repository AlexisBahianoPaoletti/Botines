using Botines.Datos.Interfaces;
using Botines.Datos;
using Botines.Entidades.Dtos.Cliente;
using Botines.Entidades.Entidades;
using Botines.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Servicios.Servicios
{
    public class ServiciosClientes:IServiciosClientes
    {
        private readonly IRepositorioClientes _repositorioClientes;
        private readonly IUnitOfWork _unitOfWork;

        public ServiciosClientes(IRepositorioClientes repositorioClientes, IUnitOfWork unitOfWork)
        {
            _repositorioClientes = repositorioClientes;
            _unitOfWork = unitOfWork;

        }

        public void Borrar(int clienteId)
        {
            try
            {
                _repositorioClientes.Borrar(clienteId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EstaRelacionada(Cliente cliente)
        {
            try
            {
                return _repositorioClientes.EstaRelacionada(cliente);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe(Cliente cliente)
        {
            try
            {
                return _repositorioClientes.Existe(cliente);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<ClienteListDto> GetClientes()
        {

            try
            {
                return _repositorioClientes.GetClientes();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Cliente GetClientePorId(int clienteId)
        {
            try
            {
                return _repositorioClientes.GetClientePorId(clienteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ClienteListDto> GetClientes(int paisId)
        {
            try
            {
                return _repositorioClientes.GetClientes(paisId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Guardar(Cliente cliente)
        {
            try
            {
                if (cliente.ClienteId == 0)
                {
                    _repositorioClientes.Agregar(cliente);

                }
                else
                {
                    _repositorioClientes.Editar(cliente);
                }
                _unitOfWork.SaveChanges();


            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ClienteListDto> Filtrar(Func<Cliente, bool> predicado, int cantidad, int pagina)
        {
            try
            {
                return _repositorioClientes.Filtrar(predicado, cantidad, pagina);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad()
        {
            try
            {
                return _repositorioClientes.GetCantidad();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ClienteListDto> GetClientesPorPagina(int cantidad, int pagina)
        {
            try
            {
                return _repositorioClientes.GetClientesPorPagina(cantidad, pagina);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetCantidad(Func<Cliente, bool> predicado)
        {
            try
            {
                return _repositorioClientes.GetCantidad(predicado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Cliente GetClientePorCorreoElectronico(string name) 
        { 
            try
            {
                return _repositorioClientes.GetClientePorCorreoElectronico(name);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
