using System;
using System.Collections.Generic;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractFactoryModel
{
    /// <summary>
    /// Заказчик ЖБИ
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string CustomerFIO { get; set; }
        [ForeignKey("CustomerId")]
        public virtual List<Order> Orders { get; set; }
    }
}
