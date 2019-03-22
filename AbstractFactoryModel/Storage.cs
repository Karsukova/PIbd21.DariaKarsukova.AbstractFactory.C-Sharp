using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractFactoryModel
{
    /// <summary>
    /// Хранилиище материалов завода
    /// </summary>
    public class Storage
    {
        public int Id { get; set; }
        [Required]
        public string StorageName { get; set; }
        [ForeignKey("StorageMaterialId")]
        public virtual List<StorageMaterial> StorageMaterials { get; set; }

    }
}
