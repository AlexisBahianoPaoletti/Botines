using Botines.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Botines.Datos
{
    public class BotinesDbContext : DbContext
    {
        public BotinesDbContext()
        {
            
        }
        public DbSet<Botin> Botines { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Localidad> Localidades { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Talle> Talles { get; set; }
        public DbSet<TalleBotin> TallesBotines { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<VentaBotin> VentasBotines { get; set; }
        public DbSet<ItemCarrito> Carrito { get; set; }
        public DbSet<VentaCarrito> VentasCarritos { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<BotinesDbContext>(null);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
