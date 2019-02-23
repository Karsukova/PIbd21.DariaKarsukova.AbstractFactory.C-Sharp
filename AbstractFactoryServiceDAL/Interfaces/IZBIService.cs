using System;
using System.Collections.Generic;
using System.Text;
using AbstractFactoryServiceDAL.BindingModel;
using AbstractFactoryServiceDAL.ViewModel;

namespace AbstractFactoryServiceDAL.Interfaces
{
    public interface IZBIService
    {
        List<ZBIViewModel> GetList();
        ZBIViewModel GetElement(int id);
        void AddElement(ZBIBindingModel model);
        void UpdElement(ZBIBindingModel model);
        void DelElement(int id);
    }
}

