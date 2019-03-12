using AbstractFactoryServiceDAL.BindingModel;
using AbstractFactoryServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryServiceDAL.Interfaces
{
    public interface IStorageService
    {
        List<StorageViewModel> GetList();
        StorageViewModel GetElement(int id);
        void AddElement(StorageBindingModel model);
        void UpdElement(StorageBindingModel model);
        void DelElement(int id);
    }
}
