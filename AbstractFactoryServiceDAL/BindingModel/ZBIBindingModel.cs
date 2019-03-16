using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryServiceDAL.BindingModel
{
    public class ZBIBindingModel
    {
        public int Id { get; set; }
        public string ZBIName { get; set; }
        public decimal Price { get; set; }
        public List<ZBIMaterialBindingModel> ZBIMaterials { get; set; }
    }
}
