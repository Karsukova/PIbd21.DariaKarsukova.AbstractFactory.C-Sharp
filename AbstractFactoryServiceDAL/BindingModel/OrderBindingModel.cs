using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
namespace AbstractFactoryServiceDAL.BindingModel
{
    [DataContract]
    public class OrderBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public int ZBIId { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public decimal Sum { get; set; }
    }
}
