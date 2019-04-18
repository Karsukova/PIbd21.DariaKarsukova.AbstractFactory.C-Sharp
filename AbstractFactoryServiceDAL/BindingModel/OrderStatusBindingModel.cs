using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
namespace AbstractFactoryServiceDAL.BindingModel
{
    [DataContract]
    public enum OrderStatusBindingModel
    {

        Принят = 0,
        Выполняется = 1,
        Готов = 2,
        Оплачен = 3

    }
}
