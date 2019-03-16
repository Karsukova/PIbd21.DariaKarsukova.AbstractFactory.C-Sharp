using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryServiceDAL.ViewModel
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFIO { get; set; }
        public int ZBIId { get; set; }
        public string ZBIName { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public string Status { get; set; }
        public string DateCreate { get; set; }
        public string DateImplement { get; set; }
    }
}
