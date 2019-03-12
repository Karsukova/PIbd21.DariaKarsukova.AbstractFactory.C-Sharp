using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryServiceDAL.BindingModel
{
    public class StorageMaterialBindingModel
    {
        public int Id { get; set; }
        public int StorageId { get; set; }
        public int MaterialId { get; set; }
        public int Count { get; set; }
    }
}
