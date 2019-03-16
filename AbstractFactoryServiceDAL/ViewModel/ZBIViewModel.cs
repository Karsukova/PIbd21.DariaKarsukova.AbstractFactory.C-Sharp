using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryServiceDAL.ViewModel
{
    public class ZBIViewModel
    {
        public int Id { get; set; }
        public string ZBIName { get; set; }
        public decimal Price { get; set; }
        public List<ZBIMaterialViewModel> ZBIMaterials { get; set; }
    }
}
