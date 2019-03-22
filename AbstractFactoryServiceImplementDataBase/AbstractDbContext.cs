using AbstractFactoryModel;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryServiceImplementDataBase
{
    public class AbstractDbContext
    {


        public AbstractDbContext() : base("AbstractDatabase")
        {
            //настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied =
           System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<ZBI> ZBIs { get; set; }
        public virtual DbSet<ZBIMaterial> ZBIMaterials { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }
        public virtual DbSet<StorageMaterial> StorageMaterials { get; set; }
    }


}
