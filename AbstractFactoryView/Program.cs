using AbstractFactoryServiceDAL.Interfaces;
using AbstractFactoryModel;
using AbstractFactoryServiceImplementList.Implementations;
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
            currentContainer.RegisterType<ICustomerService, CustomerServiceList>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMaterialService, MaterialServiceList>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IZBIService, ZBIServiceList>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceList>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStorageService, StorageServiceList>(new
           HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
