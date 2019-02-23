using System;
using System.Collections.Generic;
using System.Text;
using AbstractFactoryServiceDAL.BindingModel;
using AbstractFactoryServiceDAL.ViewModel;

namespace AbstractFactoryServiceDAL.Interfaces
{
    public interface ICustomerService
    {
        List<CustomerViewModel> GetList();
        CustomerViewModel GetElement(int id);
        void AddElement(CustomerBindingModel model);
        void UpdElement(CustomerBindingModel model);
        void DelElement(int id);
    }
}
