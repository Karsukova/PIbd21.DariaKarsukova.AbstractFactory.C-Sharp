using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryServiceDAL.BindingModel
{
    public class OrderBindingModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ZBIId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
