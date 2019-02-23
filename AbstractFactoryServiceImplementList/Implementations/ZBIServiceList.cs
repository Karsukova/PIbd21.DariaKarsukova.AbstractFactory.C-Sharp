using AbstractFactoryServiceDAL.BindingModel;
using AbstractFactoryServiceDAL.ViewModel;
using AbstractFactoryServiceDAL.Interfaces;
using AbstractFactoryModel;
using System;
using System.Collections.Generic;
using System.Text;

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
            List<ZBIViewModel> result = new List<ZBIViewModel>();
            for (int i = 0; i < source.ZBIs.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<ZBIMaterialViewModel> zbiMaterials = new
    List<ZBIMaterialViewModel>();
                for (int j = 0; j < source.ZBIMaterials.Count; ++j)
                {
                    if (source.ZBIMaterials[j].ZBIId == source.ZBIs[i].Id)
                    {
                        string materialName = string.Empty;
                        for (int k = 0; k < source.Materials.Count; ++k)
                        {
                            if (source.ZBIMaterials[j].MaterialId ==
                           source.Materials[k].Id)
                            {
                                materialName = source.Materials[k].MaterialName;
                                break;
                            }
                        }
                        zbiMaterials.Add(new ZBIMaterialViewModel
                        {
                            Id = source.ZBIMaterials[j].Id,

                            ZBIId = source.ZBIMaterials[j].ZBIId,
                            MaterialId = source.ZBIMaterials[j].MaterialId,
                            MaterialName = materialName,
                            Count = source.ZBIMaterials[j].Count
                        });
                    }
                }
                result.Add(new ZBIViewModel
                {
                    Id = source.ZBIs[i].Id,
                    ZBIName = source.ZBIs[i].ZBIName,
                    Price = source.ZBIs[i].Price,
                    ZBIMaterials = zbiMaterials
                });
            }
            return result;
        }
        public ZBIViewModel GetElement(int id)
        {
            for (int i = 0; i < source.ZBIs.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их  количество
                List<ZBIMaterialViewModel> zbiMaterials = new
    List<ZBIMaterialViewModel>();
                for (int j = 0; j < source.ZBIMaterials.Count; ++j)
                {
                    if (source.ZBIMaterials[j].ZBIId == source.ZBIs[i].Id)
                    {
                        string materialName = string.Empty;
                        for (int k = 0; k < source.Materials.Count; ++k)
                        {
                            if (source.ZBIMaterials[j].MaterialId ==
                           source.Materials[k].Id)
                            {
                                materialName = source.Materials[k].MaterialName;
                                break;
                            }
                        }
                        zbiMaterials.Add(new ZBIMaterialViewModel
                        {
                            Id = source.ZBIMaterials[j].Id,
                            ZBIId = source.ZBIMaterials[j].ZBIId,
                            MaterialId = source.ZBIMaterials[j].MaterialId,
                            MaterialName = materialName,
                            Count = source.ZBIMaterials[j].Count
                        });
                    }
                }
                if (source.ZBIs[i].Id == id)
                {
                    return new ZBIViewModel
                    {
                        Id = source.ZBIs[i].Id,
                        ZBIName = source.ZBIs[i].ZBIName,
                        Price = source.ZBIs[i].Price,
                        ZBIMaterials = zbiMaterials
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ZBIBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.ZBIs.Count; ++i)
            {
                if (source.ZBIs[i].Id > maxId)
                {
                    maxId = source.ZBIs[i].Id;
                }
                if (source.ZBIs[i].ZBIName == model.ZBIName)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            source.ZBIs.Add(new ZBI
            {
                Id = maxId + 1,
                ZBIName = model.ZBIName,
                Price = model.Price
            });
            // компоненты для изделия
            int maxPCId = 0;
            for (int i = 0; i < source.ZBIMaterials.Count; ++i)
            {
                if (source.ZBIMaterials[i].Id > maxPCId)
                {
                    maxPCId = source.ZBIMaterials[i].Id;
                }
            }
            // убираем дубли по компонентам
            for (int i = 0; i < model.ZBIMaterials.Count; ++i)
            {
                for (int j = 1; j < model.ZBIMaterials.Count; ++j)
                {
                    if (model.ZBIMaterials[i].MaterialId ==
                    model.ZBIMaterials[j].MaterialId)
                    {
                        model.ZBIMaterials[i].Count +=
                        model.ZBIMaterials[j].Count;
                        model.ZBIMaterials.RemoveAt(j--);
                    }
                }
            }
            // добавляем компоненты
            for (int i = 0; i < model.ZBIMaterials.Count; ++i)
            {
                source.ZBIMaterials.Add(new ZBIMaterial
                {
                    Id = ++maxPCId,
                    ZBIId = maxId + 1,
                    MaterialId = model.ZBIMaterials[i].MaterialId,
                    Count = model.ZBIMaterials[i].Count
                });
            }
        }
        public void UpdElement(ZBIBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.ZBIs.Count; ++i)
            {
                if (source.ZBIs[i].Id == model.Id)
                {

                    index = i;
                }
                if (source.ZBIs[i].ZBIName == model.ZBIName &&
                source.ZBIs[i].Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.ZBIs[index].ZBIName = model.ZBIName;
            source.ZBIs[index].Price = model.Price;
            int maxPCId = 0;
            for (int i = 0; i < source.ZBIMaterials.Count; ++i)
            {
                if (source.ZBIMaterials[i].Id > maxPCId)
                {
                    maxPCId = source.ZBIMaterials[i].Id;
                }
            }
            // обновляем существуюущие компоненты
            for (int i = 0; i < source.ZBIMaterials.Count; ++i)
            {
                if (source.ZBIMaterials[i].ZBIId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.ZBIMaterials.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.ZBIMaterials[i].Id ==
                       model.ZBIMaterials[j].Id)
                        {
                            source.ZBIMaterials[i].Count =
                           model.ZBIMaterials[j].Count;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if (flag)
                    {
                        source.ZBIMaterials.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            for (int i = 0; i < model.ZBIMaterials.Count; ++i)
            {
                if (model.ZBIMaterials[i].Id == 0)
                {
                    // ищем дубли
                    for (int j = 0; j < source.ZBIMaterials.Count; ++j)
                    {
                        if (source.ZBIMaterials[j].ZBIId == model.Id &&
                        source.ZBIMaterials[j].MaterialId ==
                       model.ZBIMaterials[i].MaterialId)
                        {
                            source.ZBIMaterials[j].Count +=
                           model.ZBIMaterials[i].Count;
                            model.ZBIMaterials[i].Id =
                           source.ZBIMaterials[j].Id;
                            break;

                        }
                    }
                    // если не нашли дубли, то новая запись
                    if (model.ZBIMaterials[i].Id == 0)
                    {
                        source.ZBIMaterials.Add(new ZBIMaterial
                        {
                            Id = ++maxPCId,
                            ZBIId = model.Id,
                            MaterialId = model.ZBIMaterials[i].MaterialId,
                            Count = model.ZBIMaterials[i].Count
                        });
                    }
                }
            }
        }
        public void DelElement(int id)
        {
            // удаяем записи по компонентам при удалении изделия
            for (int i = 0; i < source.ZBIMaterials.Count; ++i)
            {
                if (source.ZBIMaterials[i].ZBIId == id)
                {
                    source.ZBIMaterials.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.ZBIs.Count; ++i)
            {
                if (source.ZBIs[i].Id == id)
                {
                    source.ZBIs.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
