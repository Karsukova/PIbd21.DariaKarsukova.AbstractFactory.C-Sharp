using AbstractFactoryServiceDAL.Interfaces;
using AbstractFactoryModel;
using AbstractFactoryServiceImplementDataBase;
using AbstractFactoryServiceImplementDataBase.Implementations;using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;


namespace AbstractFactoryView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, AbstractDbContext>(new
 HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICustomerService, CustomerServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMaterialService, MaterialServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IZBIService, ZBIServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStorageService, StorageServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceDB>(new
                      HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}

