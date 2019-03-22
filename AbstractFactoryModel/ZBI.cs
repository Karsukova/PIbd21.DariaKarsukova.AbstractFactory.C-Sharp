using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractFactoryModel
{
    /// <summary>
    /// Изделие, изготавливаемое на заводе
    /// </summary>
    public class ZBI
    {
        public int Id { get; set; }
        [Required]
        public string ZBIName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [ForeignKey("OrderId")]
        public virtual List<Order> Orders { get; set; }
        [ForeignKey("ZBIMaterialId")]
        public virtual List<ZBIMaterial> ZBIMaterials { get; set; }
    }
}
