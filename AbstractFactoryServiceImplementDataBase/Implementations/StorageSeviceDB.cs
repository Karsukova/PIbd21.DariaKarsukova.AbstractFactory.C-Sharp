using AbstractFactoryServiceDAL.BindingModel;
using AbstractFactoryServiceDAL.ViewModel;
using AbstractFactoryServiceDAL.Interfaces;
using AbstractFactoryModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractFactoryServiceImplementDataBase.Implementations
{
    public class StorageServiceDB : IStorageService
    {
        private AbstractDbContext context;

        public StorageServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }

        public void AddElement(StorageBindingModel model)
        {
            Storage element = context.Storages.FirstOrDefault(rec => rec.StorageName == model.StorageName);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            context.Storages.Add(new Storage
            {
                StorageName = model.StorageName
            });
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Storage element = context.Storages.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Storages.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public StoragesViewModel GetElement(int id)
        {
            Storage element = context.Storages.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new StoragesViewModel
                {
                    Id = element.Id,
                    StorageName = element.StorageName
                };
            }
            throw new Exception("Элемент не найден");
        }

        public List<StoragesViewModel> GetList()
        {
            List<StoragesViewModel> result = context.Storages.Select(rec => new StoragesViewModel
            {
                Id = rec.Id,
                StorageName = rec.StorageName
            })
            .ToList();
            return result;
        }

        public void UpdElement(StorageBindingModel model)
        {
            Storage element = context.Storages.FirstOrDefault(rec => rec.StorageName == model.StorageName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.Storages.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.StorageName = model.StorageName;
            context.SaveChanges();
        }
    }
}
