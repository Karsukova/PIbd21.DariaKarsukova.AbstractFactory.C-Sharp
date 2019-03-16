using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryServiceDAL.ViewModel
{
    public class ZBIMaterialViewModel
    {
        public int Id { get; set; }
        public int ZBIId { get; set; }
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }

        public int Count { get; set; }
    }
}
