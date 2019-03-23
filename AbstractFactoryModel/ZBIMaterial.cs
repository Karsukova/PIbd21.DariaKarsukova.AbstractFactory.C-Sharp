using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryModel
{
    /// <summary>
    /// Сколько материала требуется для изготовления ЖБИ 
    /// </summary>
    public class ZBIMaterial
    {
        public int Id { get; set; }
        public int ZBIId { get; set; }
        public string MaterialName { get; set; }
        public int MaterialId { get; set; }
        public int Count { get; set; }
    }
}
