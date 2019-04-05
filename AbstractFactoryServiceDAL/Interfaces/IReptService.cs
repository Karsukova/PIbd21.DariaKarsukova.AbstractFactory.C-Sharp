using System;
using System.Collections.Generic;
using System.Text;

using AbstractFactoryServiceDAL.BindingModel;
using AbstractFactoryServiceDAL.ViewModel;

namespace AbstractFactoryServiceDAL.Interfaces
{
    public interface IReptService
    {
        void SaveZBIPrice(ReptBindingModel model);
        List<StoragesLoadViewModel> GetStoragesLoad();
        void SaveStoragesLoad(ReptBindingModel model);
        List<CustomerOrdersViewModel> GetCustomerOrders(ReptBindingModel model);
        void SaveCustomerOrders(ReptBindingModel model);
    }
}
