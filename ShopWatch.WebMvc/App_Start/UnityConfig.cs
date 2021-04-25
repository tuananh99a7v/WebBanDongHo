using ShopWatch.BussinessLogicLayer;
using ShopWatch.BussinessLogicLayer.IGennericRepository;
using ShopWatch.BussinessLogicLayer.IService;
using ShopWatch.BussinessLogicLayer.Services;
using ShopWatch.Model;
using ShopWatch.Model.DataContext;
using System;

using Unity;

namespace ShopWatch.WebMvc
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            container.RegisterType<IGenericRepository<Category>, GenericRepository<Category>>();
            container.RegisterType<ICategoryService, CategoryService>();

            container.RegisterType<IGenericRepository<Publisher>, GenericRepository<Publisher>>();
            container.RegisterType<IPublisherService, PublisherService>();

            container.RegisterType<IGenericRepository<User>, GenericRepository<User>>();
            container.RegisterType<IUserService, UserService>();

            container.RegisterType<IGenericRepository<Promotion>, GenericRepository<Promotion>>();
            container.RegisterType<IPromotionService, PromotionService>();

            container.RegisterType<IGenericRepository<Watch>, GenericRepository<Watch>>();
            container.RegisterType<IWatchService, WatchService>();

            container.RegisterType<IGenericRepository<Order>, GenericRepository<Order>>();
            container.RegisterType<IOrderService, OrderService>();

            container.RegisterType<IGenericRepository<OrderDetail>, GenericRepository<OrderDetail>>();
            container.RegisterType<IOrderDetailService, OrderDetailService>();

            container.RegisterType<ICheckoutService, CheckoutService>();

            container.RegisterSingleton<ShopWatchDataContext, ShopWatchDataContext>();
            container.RegisterSingleton<IDbFactory, DbFactory>();
            container.RegisterSingleton<IUnitOfWork, UnitOfWork>();

            container.RegisterType<IGenericRepository<Account>, GenericRepository<Account>>();
            container.RegisterType<IAccountService, AccountService>();

            container.RegisterType<IGenericRepository<User>, GenericRepository<User>>();
            container.RegisterType<IUserService, UserService>();

            container.RegisterType<IGenericRepository<Promotion>, GenericRepository<Promotion>>();
            container.RegisterType<IPromotionService, PromotionService>();
        }
    }
}