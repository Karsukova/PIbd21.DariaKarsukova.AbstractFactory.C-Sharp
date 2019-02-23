using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryModel
{
    /// <summary>
    /// Заказ клиента
    /// </summary>
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ZBIId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
    }
}
