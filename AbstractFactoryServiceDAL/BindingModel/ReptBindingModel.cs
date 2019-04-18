using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
namespace AbstractFactoryServiceDAL.BindingModel
{
    [DataContract]
    public class ReptBindingModel
    {
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public DateTime? DateFrom { get; set; }
        [DataMember]
        public DateTime? DateTo { get; set; }
    }
}
