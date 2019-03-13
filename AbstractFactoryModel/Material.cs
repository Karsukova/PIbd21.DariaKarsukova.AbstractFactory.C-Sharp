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
        public string MaterialName { get; set; }
    }

}

