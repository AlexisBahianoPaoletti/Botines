using Botines.Entidades.Dtos.VentaBotin;
using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Datos.Interfaces
{
    public interface IRepositorioVentasBotines
    {
        void Agregar(VentaBotin ventabotin);
        void Borrar(int id);
        void Editar(VentaBotin ventabotin);
        bool EstaRelacionado(VentaBotin ventabotin);
        bool Existe(VentaBotin ventabotin);
        VentaBotin GetVentaBotinPorId(int id);
        List<VentaBotinListDto> GetVentasBotines(int ventaId);
    }
}
