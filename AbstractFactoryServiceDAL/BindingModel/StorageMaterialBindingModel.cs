using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryServiceDAL.BindingModel
{
    [DataContract]
    public class StorageMaterialBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int StorageId { get; set; }
        [DataMember]
        public int MaterialId { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
