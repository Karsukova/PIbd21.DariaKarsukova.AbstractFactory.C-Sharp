using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
namespace AbstractFactoryServiceDAL.BindingModel
{
    [DataContract]
    public class StorageBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string StorageName { get; set; }
    }
}
