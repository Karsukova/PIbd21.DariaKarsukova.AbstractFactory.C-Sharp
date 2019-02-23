using System;
using System.Collections.Generic;
using System.Text;
using AbstractFactoryServiceDAL.BindingModel;
using AbstractFactoryServiceDAL.ViewModel;

namespace AbstractFactoryServiceDAL.Interfaces
{
    public interface IMaterialService
    {
        List<MaterialViewModel> GetList();
        MaterialViewModel GetElement(int id);
        void AddElement(MaterialBindingModel model);
        void UpdElement(MaterialBindingModel model);
        void DelElement(int id);
    }
}
