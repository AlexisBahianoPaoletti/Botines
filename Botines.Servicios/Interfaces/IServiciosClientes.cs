using Botines.Entidades.Dtos.Cliente;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Servicios.Interfaces
{
    public interface IServiciosClientes
    {
        List<ClienteListDto> GetClientes();
        bool Existe(Cliente cliente);
        void Guardar(Cliente cliente);
        bool EstaRelacionada(Cliente cliente);
        Cliente GetClientePorId(int clienteId);
        void Borrar(int clienteId);
        List<ClienteListDto> GetClientes(int paisId);
        List<ClienteListDto> Filtrar(Func<Cliente, bool> predicado, int cantidad, int pagina);
        int GetCantidad();
        List<ClienteListDto> GetClientesPorPagina(int cantidad, int pagina);
        int GetCantidad(Func<Cliente, bool> predicado);
        Cliente GetClientePorCorreoElectronico(string name);
        //List<SelectListItem> GetClientesDropDownList();
    }
}
