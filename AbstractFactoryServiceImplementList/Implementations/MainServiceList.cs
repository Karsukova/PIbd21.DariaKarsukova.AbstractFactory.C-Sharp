using AbstractFactoryServiceDAL.BindingModel;
using AbstractFactoryServiceDAL.ViewModel;
using AbstractFactoryServiceDAL.Interfaces;
using AbstractFactoryModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AbstractFactoryServiceImplementList.Implementations
{
    public class MainServiceList : IMainService
    {
        private DataListSingleton source;
        public MainServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<OrderViewModel> GetList()
        {
            List<OrderViewModel> result = source.Orders.Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                CustomerId = rec.CustomerId,
                ZBIId = rec.ZBIId,
                DateCreate = rec.DateCreate.ToLongDateString(),
                DateImplement = rec.DateImplement?.ToLongDateString(),
                Status = rec.Status.ToString(),
                Count = rec.Count,
                Sum = rec.Sum,
                CustomerFIO = source.Customers.FirstOrDefault(recC => recC.Id ==
               rec.CustomerId)?.CustomerFIO,
                ZBIName = source.ZBIs.FirstOrDefault(recP => recP.Id ==
              rec.ZBIId)?.ZBIName,
            })
  .ToList();
            return result;
        }
        public void CreateOrder(OrderBindingModel model)
        {
            int maxId = source.Orders.Count > 0 ? source.Orders.Max(rec => rec.Id) : 0;
            source.Orders.Add(new Order
            {
                Id = maxId + 1,
                CustomerId = model.CustomerId,
                ZBIId = model.ZBIId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = OrderStatus.Принят
            });
        }
        public void TakeOrderInWork(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != OrderStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }
            // смотрим по количеству компонентов на складах
            var zbiMaterials = source.ZBIMaterials.Where(rec => rec.ZBIId
           == element.ZBIId);
            foreach (var zbiMaterial in zbiMaterials)
            {
                int countOnStorages = source.StorageMaterials
                .Where(rec => rec.MaterialId ==
               zbiMaterial.MaterialId)
                .Sum(rec => rec.Count);
                if (countOnStorages < zbiMaterial.Count * element.Count)
                {
                    var materialName = source.Materials.FirstOrDefault(rec => rec.Id ==
                   zbiMaterial.MaterialId);
                    throw new Exception("Не достаточно компонента " +
                   materialName?.MaterialName + " требуется " + (zbiMaterial.Count * element.Count) +
                   ", в наличии " + countOnStorages);
                }
            }
            // списываем
            foreach (var zbiMaterial in zbiMaterials)
            {
                int countOnStorages = zbiMaterial.Count * element.Count;
                var storageMaterials = source.StorageMaterials.Where(rec => rec.MaterialId
               == zbiMaterial.MaterialId);
                foreach (var storageMaterial in storageMaterials)
                {
                    // компонентов на одном слкаде может не хватать
                    if (storageMaterial.Count >= countOnStorages)
                    {
                        storageMaterial.Count -= countOnStorages;
                        break;
                    }
                    else
                    {
                        countOnStorages -= storageMaterial.Count;
                        storageMaterial.Count = 0;
                    }
                }
              
            }
            element.DateImplement = DateTime.Now;
            element.Status = OrderStatus.Выполняется;
        }
        public void FinishOrder(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");

            }
            if (element.Status != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Status = OrderStatus.Готов;
        }
        public void PayOrder(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Status = OrderStatus.Оплачен;
        }
        public void PutComponentOnStorage(StorageMaterialBindingModel model)
        {
            StorageMaterial element = source.StorageMaterials.FirstOrDefault(rec =>
rec.StorageId == model.StorageId && rec.MaterialId == model.MaterialId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                int maxId = source.StorageMaterials.Count > 0 ?
               source.StorageMaterials.Max(rec => rec.Id) : 0;
                source.StorageMaterials.Add(new StorageMaterial
                {
                    Id = ++maxId,
                    StorageId = model.StorageId,
                    MaterialId = model.MaterialId,
                    Count = model.Count
                });
            }
        }
    }
}

