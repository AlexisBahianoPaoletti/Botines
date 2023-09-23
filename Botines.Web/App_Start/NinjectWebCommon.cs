[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Botines.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Botines.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Botines.Web.App_Start
{
    using System;
    using System.Web;
    using Botines.Datos;
    using Botines.Datos.Interfaces;
    using Botines.Datos.Repositorios;
    using Botines.Servicios.Interfaces;
    using Botines.Servicios.Servicios;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IRepositorioPaises>().To<RepositorioPaises>().InRequestScope();
            kernel.Bind<IServiciosPaises>().To<ServiciosPaises>().InRequestScope();
            kernel.Bind<IRepositorioProvincias>().To<RepositorioProvincias>().InRequestScope();
            kernel.Bind<IServiciosProvincias>().To<ServiciosProvincias>().InRequestScope();
            kernel.Bind<IRepositorioLocalidades>().To<RepositorioLocalidades>().InRequestScope();
            kernel.Bind<IServiciosLocalidades>().To<ServiciosLocalidades>().InRequestScope();
            kernel.Bind<IRepositorioTalles>().To<RepositorioTalles>().InRequestScope();
            kernel.Bind<IServiciosTalles>().To < ServiciosTalles>().InRequestScope();
            kernel.Bind<IRepositorioMarcas>().To<RepositorioMarcas>().InRequestScope();
            kernel.Bind<IServiciosMarcas>().To<ServiciosMarcas>().InRequestScope();
            kernel.Bind<IRepositorioModelos>().To<RepositorioModelos>().InRequestScope();
            kernel.Bind<IServiciosModelos>().To<ServiciosModelos>().InRequestScope();
            kernel.Bind<IRepositorioProveedores>().To<RepositorioProveedores>().InRequestScope();
            kernel.Bind<IServiciosProveedores>().To<ServiciosProveedores>().InRequestScope();
            kernel.Bind<IRepositorioClientes>().To<RepositorioClientes>().InRequestScope();
            kernel.Bind<IServiciosClientes>().To<ServiciosClientes>().InRequestScope();
            kernel.Bind<IRepositorioBotines>().To<RepositorioBotines>().InRequestScope();
            kernel.Bind<IServiciosBotines>().To<ServiciosBotines>().InRequestScope();
            kernel.Bind<IRepositorioTallesBotines>().To<RepositorioTallesBotines>().InRequestScope();
            kernel.Bind<IServiciosTallesBotines>().To<ServiciosTallesBotines>().InRequestScope();
            kernel.Bind<IRepositorioCarritos>().To<RepositorioCarritos>().InRequestScope();
            kernel.Bind<IServiciosCarrito>().To<ServiciosCarritos>().InRequestScope();
            kernel.Bind<IRepositorioVentas>().To<RepositorioVentas>().InRequestScope();
            kernel.Bind<IServiciosVentas>().To<ServiciosVentas>().InRequestScope();
            kernel.Bind<IRepositorioVentasBotines>().To<RepositorioVentasBotines>().InRequestScope();
            
            kernel.Bind<IRepositorioVentasCarritos>().To<RepositorioVentasCarritos>().InRequestScope();
           


            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();

            kernel.Bind<BotinesDbContext>().ToSelf().InRequestScope();
        }
    }
}