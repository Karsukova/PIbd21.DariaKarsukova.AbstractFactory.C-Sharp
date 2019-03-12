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
    public class ZBIServiceList : IZBIService
    {
        private DataListSingleton source;
        public ZBIServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<ZBIViewModel> GetList()
        {
            List<ZBIViewModel> result = source.ZBIs.Select(rec => new ZBIViewModel
            {
                Id = rec.Id,
                ZBIName = rec.ZBIName,
                Price = rec.Price,
                ZBIMaterials = source.ZBIMaterials.Where(recPC => recPC.ZBIId == rec.Id).Select(recPC => new ZBIMaterialViewModel
                {
                    Id = recPC.Id,
                    ZBIId = recPC.ZBIId,
                    MaterialId = recPC.MaterialId,
                    MaterialName = source.Materials.FirstOrDefault(recC =>
                   recC.Id == recPC.MaterialId)?.MaterialName,
                    Count = recPC.Count
                }).ToList()
            }).ToList();

            return result;
        }
        public ZBIViewModel GetElement(int id)
        {
            ZBI element = source.ZBIs.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ZBIViewModel
                {
                    Id = element.Id,
                    ZBIName = element.ZBIName,
                    Price = element.Price,
                    ZBIMaterials = source.ZBIMaterials.Where(recPC => recPC.ZBIId == element.Id).Select(recPC => new ZBIMaterialViewModel
                    {
                        Id = recPC.Id,
                        ZBIId = recPC.ZBIId,
                        MaterialId = recPC.MaterialId,
                        MaterialName = source.Materials.FirstOrDefault(recC => recC.Id == recPC.MaterialId)?.MaterialName,
                        Count = recPC.Count
                    })
               .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ZBIBindingModel model)
        {
            ZBI element = source.ZBIs.FirstOrDefault(rec => rec.ZBIName == model.ZBIName);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            int maxId = source.ZBIs.Count > 0 ? source.ZBIs.Max(rec => rec.Id) :
           0;
            source.ZBIs.Add(new ZBI
            {
                Id = maxId + 1,
                ZBIName = model.ZBIName,
                Price = model.Price
            });
            // компоненты для изделия
            int maxPCId = source.ZBIMaterials.Count > 0 ?
 source.ZBIMaterials.Max(rec => rec.Id) : 0;
            // убираем дубли по компонентам
            var groupComponents = model.ZBIMaterials.GroupBy(rec => rec.MaterialId).Select(rec => new
            {
                MaterialId = rec.Key,
                Count = rec.Sum(r => r.Count)
            });

            // добавляем компоненты
            foreach (var groupMaterial in groupComponents)
            {
                source.ZBIMaterials.Add(new ZBIMaterial
                {
                    Id = ++maxPCId,
                    ZBIId = maxId + 1,
                    MaterialId = groupMaterial.MaterialId,
                    Count = groupMaterial.Count
                });
            }
        }
        public void UpdElement(ZBIBindingModel model)
        {
            ZBI element = source.ZBIs.FirstOrDefault(rec => rec.ZBIName ==
model.ZBIName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            element = source.ZBIs.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ZBIName = model.ZBIName;
            element.Price = model.Price;
            int maxPCId = source.ZBIMaterials.Count > 0 ?
           source.ZBIMaterials.Max(rec => rec.Id) : 0;

            // обновляем существуюущие компоненты
            var compIds = model.ZBIMaterials.Select(rec =>
 rec.MaterialId).Distinct();
            var updateMaterials = source.ZBIMaterials.Where(rec => rec.ZBIId ==
           model.Id && compIds.Contains(rec.MaterialId));
            foreach (var updateMaterial in updateMaterials)
            {
                updateMaterial.Count = model.ZBIMaterials.FirstOrDefault(rec =>
               rec.Id == updateMaterial.Id).Count;
            }
            source.ZBIMaterials.RemoveAll(rec => rec.ZBIId == model.Id &&
           !compIds.Contains(rec.MaterialId));
            // новые записи
            var groupMaterials = model.ZBIMaterials.Where(rec => rec.Id == 0).GroupBy(rec => rec.MaterialId)
            .Select(rec => new
            {
                MaterialId = rec.Key,
                Count = rec.Sum(r => r.Count)
            });
            foreach (var groupMaterial in groupMaterials)
            {
                ZBIMaterial elementPC = source.ZBIMaterials.FirstOrDefault(rec
               => rec.ZBIId == model.Id && rec.MaterialId == groupMaterial.MaterialId);
                if (elementPC != null)
                {
                    elementPC.Count += groupMaterial.Count;
                }
                else
                {

                    source.ZBIMaterials.Add(new ZBIMaterial
                    {
                        Id = ++maxPCId,
                        ZBIId = model.Id,
                        MaterialId = groupMaterial.MaterialId,
                        Count = groupMaterial.Count
                    });
                }
            }
        }

        public void DelElement(int id)
        {
            ZBI element = source.ZBIs.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // удаяем записи по компонентам при удалении изделия
                source.ZBIMaterials.RemoveAll(rec => rec.MaterialId == id);
                source.ZBIs.Remove(element);
            }
            else
            {

                throw new Exception("Элемент не найден");
            }
        }
    }
}

