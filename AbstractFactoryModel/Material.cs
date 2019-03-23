using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractFactoryModel
{
    /// <summary>
    /// Компонент, требуемый для изготовления ЖБИ
    /// </summary>
    public class Material
    {
        public int Id { get; set; }
        [Required]
        public string MaterialName { get; set; }
        [ForeignKey("MaterialId")]
        public virtual List<ZBIMaterial> ZBIMaterials { get; set; }
        [ForeignKey("MaterialId")]
        public virtual List<StorageMaterial> StorageMaterials { get; set; }
    }

}

