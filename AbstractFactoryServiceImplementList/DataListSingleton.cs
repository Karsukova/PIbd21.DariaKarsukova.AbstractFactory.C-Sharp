using System;
using AbstractFactoryModel;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryServiceImplementList
{
    class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Customer> Customers { get; set; }

        public List<Material> Materials { get; set; }
        public List<Order> Orders { get; set; }
        public List<ZBI> ZBIs { get; set; }
        public List<ZBIMaterial> ZBIMaterials { get; set; }
        private DataListSingleton()
        {
            Customers = new List<Customer>();
            Materials = new List<Material>();
            Orders = new List<Order>();
            ZBIs = new List<ZBI>();
            ZBIMaterials = new List<ZBIMaterial>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
