using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryServiceDAL.ViewModel
{
    public class StoragesViewModel
    {
        public int Id { get; set; }
        public string StorageName { get; set; }
        public List<StorageMaterialViewModel> StorageMaterials { get; set; }
    }
}
