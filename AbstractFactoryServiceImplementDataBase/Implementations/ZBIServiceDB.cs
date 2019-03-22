using AbstractFactoryServiceDAL.BindingModel;
using AbstractFactoryServiceDAL.ViewModel;
using AbstractFactoryServiceDAL.Interfaces;
using AbstractFactoryModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractFactoryServiceImplementDataBase.Implementations
{
    public class ZBIServiceDB : IZBIService
    {
        private AbstractDbContext context;
        public ZBIServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }
        public List<ZBIViewModel> GetList()
        {
            List<ZBIViewModel> result = context.ZBIs.Select(rec => new
           ZBIViewModel
            {
                Id = rec.Id,
                ZBIName = rec.ZBIName,
                Price = rec.Price,
                ZBIMaterials = context.ZBIMaterials
            .Where(recPC => recPC.ZBIId == rec.Id)
           .Select(recPC => new ZBIMaterialViewModel
           {
               Id = recPC.Id,
               ZBIId = recPC.ZBIId,
               MaterialId = recPC.MaterialId,
               MaterialName = recPC.Material.MaterialName,
               Count = recPC.Count
           })
           .ToList()
            })
            .ToList();
            return result;
        }
        public ZBIViewModel GetElement(int id)
        {
            ZBI element = context.ZBIs.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ZBIViewModel

                {
                    Id = element.Id,
                    ZBIName = element.ZBIName,
                    Price = element.Price,
                    ZBIMaterials = context.ZBIMaterials
 .Where(recPC => recPC.ZBIId == element.Id)
 .Select(recPC => new ZBIMaterialViewModel
 {
     Id = recPC.Id,
     ZBIId = recPC.ZBIId,
     MaterialId = recPC.MaterialId,
     MaterialName = recPC.Material.MaterialName,
     Count = recPC.Count
 })
 .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(ZBIBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    ZBI element = context.ZBIs.FirstOrDefault(rec =>
                   rec.ZBIName == model.ZBIName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = new ZBI
                    {
                        ZBIName = model.ZBIName,
                        Price = model.Price
                    };
                    context.ZBIs.Add(element);
                    context.SaveChanges();
                    // убираем дубли по компонентам
                    var groupComponents = model.ZBIMaterials
                     .GroupBy(rec => rec.MaterialId)
                    .Select(rec => new
                    {
                        ComponentId = rec.Key,
                        Count = rec.Sum(r => r.Count)
                    });
                    // добавляем компоненты
                    foreach (var groupComponent in groupComponents)
                    {
                        context.ZBIMaterials.Add(new ZBIMaterial
                        {
                            ZBIId = element.Id,
                            MaterialId = groupComponent.ComponentId,
                            Count = groupComponent.Count
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                    
                }
            }
        }
        public void UpdElement(ProductBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Product element = context.Products.FirstOrDefault(rec =>
                   rec.ProductName == model.ProductName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.ZBIs.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.ProductName = model.ProductName;
                    element.Price = model.Price;
                    context.SaveChanges();
                    // обновляем существуюущие компоненты
                    var compIds = model.ProductComponents.Select(rec =>
                   rec.ComponentId).Distinct();
                    var updateComponents = context.ZBIMaterials.Where(rec =>
                   rec.ProductId == model.Id && compIds.Contains(rec.ComponentId));
                    foreach (var updateComponent in updateComponents)
                    {
                        updateComponent.Count =
                       model.ProductComponents.FirstOrDefault(rec => rec.Id == updateComponent.Id).Count;
                    }
                    context.SaveChanges();
                    context.ZBIMaterials.RemoveRange(context.ZBIMaterials.Where(rec =>
                    rec.ProductId == model.Id && !compIds.Contains(rec.ComponentId)));
                    context.SaveChanges();
                    // новые записи
                    var groupComponents = model.ProductComponents
                    .Where(rec => rec.Id == 0)
                   .GroupBy(rec => rec.ComponentId)
                   .Select(rec => new
                   {
                       ComponentId = rec.Key,
                       Count = rec.Sum(r => r.Count)
                   });
                    foreach (var groupComponent in groupComponents)
                    {
                        ZBIMaterial elementPC =
                       context.ZBIMaterials.FirstOrDefault(rec => rec.ProductId == model.Id &&
                       rec.ComponentId == groupComponent.ComponentId);
                        if (elementPC != null)
                        {
                            elementPC.Count += groupComponent.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.ProductComponents.Add(new ProductComponent
                            {
                                ProductId = model.Id,
                                
                            ComponentId = groupComponent.ComponentId,
                                Count = groupComponent.Count
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    ZBI element = context.ZBIs.FirstOrDefault(rec => rec.Id ==
                   id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.ProductComponents.RemoveRange(context.ProductComponents.Where(rec =>
                        rec.ProductId == id));
                        context.ZBIs.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
