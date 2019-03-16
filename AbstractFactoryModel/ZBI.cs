using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryModel
{
    /// <summary>
    /// Изделие, изготавливаемое на заводе
    /// </summary>
    public class ZBI
    {
        public int Id { get; set; }
        public string ZBIName { get; set; }
        public decimal Price { get; set; }
    }
}
