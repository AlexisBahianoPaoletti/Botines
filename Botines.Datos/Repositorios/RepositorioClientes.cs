using Botines.Datos.Interfaces;
using Botines.Entidades.Dtos.Cliente;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Datos.Repositorios
{
    public class RepositorioClientes:IRepositorioClientes
    {
        private readonly BotinesDbContext _context;
        public RepositorioClientes(BotinesDbContext context)
        {
            _context = context;
        }

        public void Agregar(Cliente cliente)
        {
            try
            {
                _context.Clientes.Add(cliente);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Borrar(int clienteId)
        {
            try
            {
                var clienteInDb = GetClientePorId(clienteId);
                if (clienteInDb == null)
                {
                    throw new Exception("Registro dado de baja por otro usuario");
                }
                else
                {
                    _context.Entry(clienteInDb).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Editar(Cliente cliente)
        {
            try
            {
                var clienteInDb = _context.Clientes.SingleOrDefault(c => c.ClienteId == cliente.ClienteId);


                //var clienteInDb = GetClientePorId(cliente.ClienteId);

                //if (clienteInDb == null)
                //{
                //    throw new Exception("Registro dado de baja por otro usuario");
                //}


                clienteInDb.Pais = null;
                clienteInDb.Provincia = null;
                clienteInDb.Localidad = null;
                clienteInDb.Nombre = cliente.Nombre;
                clienteInDb.Apellido = cliente.Apellido;
                clienteInDb.PaisId = cliente.PaisId;
                clienteInDb.ProvinciaId = cliente.ProvinciaId;
                clienteInDb.LocalidadId = cliente.LocalidadId;

                clienteInDb.Direccion = cliente.Direccion;
                clienteInDb.TelefonoFijo = cliente.TelefonoFijo;
                clienteInDb.TelefonoMovil = cliente.TelefonoMovil;
                clienteInDb.CorreoElectronico = cliente.CorreoElectronico;


                _context.Entry(clienteInDb).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EstaRelacionada(Cliente cliente)
        {
            try
            {
                return _context.Ventas.Any(v => v.ClienteId == cliente.ClienteId);
                   
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
                if (cliente.ClienteId == 0)
                {
                    return _context.Clientes.Any(c => c.Nombre == cliente.Nombre
                        && c.Apellido == cliente.Apellido
                        && c.PaisId == cliente.PaisId
                        && c.ProvinciaId == cliente.ProvinciaId
                        && c.LocalidadId == cliente.LocalidadId
                        || c.CorreoElectronico == cliente.CorreoElectronico);

                }
                return _context.Clientes.Any(c => (c.Nombre == cliente.Nombre
                        && c.Apellido == cliente.Apellido
                        && c.PaisId == cliente.PaisId
                        && c.ProvinciaId == cliente.ProvinciaId
                        && c.LocalidadId == cliente.LocalidadId 
                        || c.CorreoElectronico == cliente.CorreoElectronico)
                        && c.ClienteId != cliente.ClienteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ClienteListDto> Filtrar(Func<Cliente, bool> predicado, int cantidad, int pagina)
        {
            return _context.Clientes.Include(p => p.Pais).Include(p => p.Provincia).Include(l => l.Localidad)
                 .Where(predicado)
                 .OrderBy(c => c.Apellido)
                 .Skip(cantidad * (pagina - 1))
                 .Take(cantidad)
                 .Select(c => new ClienteListDto
                 {
                     ClienteId = c.ClienteId,
                     Nombre = c.Nombre,
                     Apellido= c.Apellido,
                     Pais = c.Pais.NombrePais,
                     Provincia = c.Provincia.NombreProvincia,
                     Localidad = c.Localidad.NombreLocalidad

                 }).ToList();
        }

        public int GetCantidad()
        {
            return _context.Clientes.Count();
        }

        public int GetCantidad(Func<Cliente, bool> predicado)
        {
            return _context.Clientes.Count(predicado);
        }


        public List<ClienteListDto> GetClientes()
        {
            return _context.Clientes.Include(p => p.Pais).Include(p => p.Provincia).Include(l => l.Localidad)
                .Select(c => new ClienteListDto
                {
                    ClienteId = c.ClienteId,
                    Nombre = c.Nombre,
                    Apellido = c.Apellido,
                    Pais = c.Pais.NombrePais,
                    Provincia = c.Provincia.NombreProvincia,
                    Localidad = c.Localidad.NombreLocalidad
                }).AsNoTracking()
                .ToList();
        }

        public List<ClienteListDto> GetClientes(int paisId)
        {
            try
            {
                return _context.Clientes.Include(p => p.Pais)
                    .Where(p => p.PaisId == paisId)
                    .Select(c => new ClienteListDto
                    {
                        ClienteId = c.ClienteId,
                        Nombre = c.Nombre,
                        Apellido = c.Apellido,
                        Pais = c.Pais.NombrePais,
                        //Provincia = c.Provincia.NombreProvincia,
                        //Localidad = c.Localidad.NombreLocalidad
                    }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ClienteListDto> GetClientesPorPagina(int cantidad, int pagina)
        {
            return _context.Clientes.Include(p => p.Pais).Include(p => p.Provincia).Include(l => l.Localidad)
                .OrderBy(c => c.Apellido)
                .Skip(cantidad * (pagina - 1))
                .Take(cantidad)
                .Select(c => new ClienteListDto
                {
                    ClienteId = c.ClienteId,
                    Nombre = c.Nombre,
                    Apellido = c.Apellido,
                    Pais = c.Pais.NombrePais,
                    Provincia = c.Provincia.NombreProvincia,
                    Localidad = c.Localidad.NombreLocalidad
                }).ToList();
        }
        public Cliente GetClientePorId(int clienteId)
        {
            try
            {
                return _context.Clientes.Include(p => p.Pais).Include(p => p.Provincia).Include(l => l.Localidad)

                    .SingleOrDefault(p => p.ClienteId == clienteId);

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
                return _context.Clientes.SingleOrDefault(c=>c.CorreoElectronico==name);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
