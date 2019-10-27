[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SocialNetwork_PL.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SocialNetwork_PL.App_Start.NinjectWebCommon), "Stop")]

namespace SocialNetwork_PL.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Modules;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using SocialNetwork_BLL.Infrastructure;
    using SocialNetwork_BLL.Interfaces;
    using SocialNetwork_BLL.Services;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
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
            var pr = new INinjectModule[] { new ServiceModule("name=SocialNetwork_FinalProject") };

            var kernel = new StandardKernel(pr);
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                GlobalConfiguration.Configuration.DependencyResolver = new Ninject.Web.WebApi.NinjectDependencyResolver(kernel);
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
            kernel.Bind<IMessageHeaderService>().To<MessageHeaderService>();
            kernel.Bind<IMessageService>().To<MessageService>();
            kernel.Bind<IPublicationService>().To<PublicationService>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IAdditionalService>().To<AdditionalService>();
        }        
    }
}