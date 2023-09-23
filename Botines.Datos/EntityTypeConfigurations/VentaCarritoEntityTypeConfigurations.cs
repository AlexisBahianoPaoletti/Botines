using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Datos.EntityTypeConfigurations
{
    public class VentaCarritoEntityTypeConfigurations : EntityTypeConfiguration<VentaCarrito>
    {
        public VentaCarritoEntityTypeConfigurations()
        {
            ToTable("VentasCarritos");

        }
    }
}
