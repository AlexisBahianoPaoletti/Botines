using Botines.Entidades.Dtos.Cliente;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Datos.Interfaces
{
    public interface IRepositorioClientes
    {
        List<ClienteListDto> GetClientes();
        void Agregar(Cliente cliente);
        bool Existe(Cliente cliente);
        void Editar(Cliente cliente);
        void Borrar(int clienteId);
        bool EstaRelacionada(Cliente cliente);
        Cliente GetClientePorId(int clienteId);
        List<ClienteListDto> GetClientes(int paisId);
        List<ClienteListDto> Filtrar(Func<Cliente, bool> predicado, int cantidad, int pagina);
        int GetCantidad();
        List<ClienteListDto> GetClientesPorPagina(int cantidad, int pagina);
        int GetCantidad(Func<Cliente, bool> predicado);
        Cliente GetClientePorCorreoElectronico(string name);
        //List<SelectListItem> GetClientesDropDownList();
    }
}
