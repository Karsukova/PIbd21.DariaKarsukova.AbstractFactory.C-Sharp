using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryServiceDAL.ViewModel
{
    public class CustomerOrdersViewModel
    {
        public string CustomerName { get; set; }
        public string DateCreate { get; set; }
        public string ZBIName { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public string Status { get; set; }
    }
}
