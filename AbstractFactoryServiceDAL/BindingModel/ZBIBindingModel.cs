using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryServiceDAL.BindingModel
{
    [DataContract]
    public class ZBIBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string ZBIName { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public List<ZBIMaterialBindingModel> ZBIMaterials { get; set; }
    }
}
