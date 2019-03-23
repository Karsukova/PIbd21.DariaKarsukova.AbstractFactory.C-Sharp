using System;
using System.Collections.Generic;
using System.Text;

using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractFactoryModel
{
    /// <summary>
    /// Заказ клиента
    /// </summary>
    public class Order
    {

        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public int ZBIId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ZBI ZBI { get; set; }
    }
}
