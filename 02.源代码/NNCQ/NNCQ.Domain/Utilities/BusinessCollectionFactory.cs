using NNCQ.Domain.Core;
using NNCQ.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Utilities
{
    /// <summary>
    /// 用于处理一些自动化提取转换的数据字典集合，或者更改集合的外观
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BusinessCollectionFactory<T> where T : class, IEntity, new() 
    {
        public static List<PlainFacadeItem> GetPlainFacadeItemCollection()
        {
            var service = new EntityRepository<T>(new EntityDbContext());
            var sourceItems = service.GetAll().OrderBy(s => s.SortCode);

            var items = new List<PlainFacadeItem>();
            foreach (var pItem in sourceItems)
            {
                var item = new PlainFacadeItem() { ID = pItem.ID.ToString(), Name = pItem.Name, Description = pItem.Description, SortCode = pItem.SortCode };
                items.Add(item);
            }
            return items;

        }

        public static List<PlainFacadeItem> GetPlainFacadeItemCollectionWithEnum()
        {
            var items = new List<PlainFacadeItem>();
            foreach (var eItem in Enum.GetValues(typeof(T)))
            {
                var item = new PlainFacadeItem()
                {
                    ID = Enum.GetName(typeof(T), eItem),
                    Name = eItem.ToString(),
                    Description = "",
                    SortCode = Enum.GetName(typeof(T), eItem)
                };

            }
            return items;
        }

        /// <summary>
        /// 根据指定的类型，直接提取对应的持久层数据，并转换为 List<SelfReferentialItem>。
        /// </summary>
        /// <param name="useSpace">返回的数据中，是否在 Name 属性前面，根据层次添加空格</param>
        /// <returns></returns>
        public static List<SelfReferentialItem> GetSelfReferentialItemCollection(bool useSpace)
        {
            var service = new EntityRepository<T>(new EntityDbContext());
            PropertyInfo[] properties = typeof(T).GetProperties();
            var parentProperty = properties.Where(x => x.PropertyType.Name == typeof(T).Name).FirstOrDefault();

            var pCollection = service.GetAll().OrderBy(s => s.SortCode).ToList();
            var pItems = new List<SelfReferentialItem>();
            foreach (var item in pCollection)
            {
                var pItem = new SelfReferentialItem();
                pItem.ID = item.ID.ToString();
                pItem.ItemName = item.Name;
                pItem.SortCode = item.SortCode;

                var pObject = (T)parentProperty.GetValue(item);
                pItem.ParentID = pObject.ID.ToString();

                pItems.Add(pItem);
            }
            if (useSpace)
                return HierarchicalFacadeItemsFromCustomCollection(pItems);
            else
                return pItems;
        }

        public static List<SelfReferentialItem> HierarchicalFacadeItemsFromCustomCollection(List<SelfReferentialItem> source)
        {
            var result = new List<SelfReferentialItem>();
            var rootItems = source.Where(rn => rn.ID == rn.ParentID || rn.ParentID == "");
            foreach (var item in rootItems)
            {
                result.Add(item);
                _GetHierarchicalStyleSubItems(item, 1, source, result);
            }
            return result;
        }

        private static void _GetHierarchicalStyleSubItems(SelfReferentialItem rootNode, int spaceNumber, List<SelfReferentialItem> source, List<SelfReferentialItem> result)
        {
            var subItems = source.Where(sn => sn.ParentID == rootNode.ID && sn.ID != rootNode.ParentID).OrderBy(o => o.SortCode);
            foreach (var subItem in subItems.OrderBy(s => s.SortCode))
            {
                var ttt = _SpaceLength(spaceNumber);
                subItem.ItemName = _SpaceLength(spaceNumber) + subItem.ItemName;
                result.Add(subItem);
                _GetHierarchicalStyleSubItems(subItem, spaceNumber + 1, source, result);
            }
        }

        private static string _SpaceLength(int i)
        {
            string space = "";
            for (int j = 0; j < i; j++)
            {
                space += "　";  // 全角空格符
            }
            return space + "";
        }


    }

    /// <summary>
    /// 专门用于处理枚举类型转换为 List<PlainFacadeItem> 供具体场合使用
    /// </summary>
    public class BusinessCollectionFactoryEnum
    {
        public static List<PlainFacadeItem> GetPlainFacadeItemCollection<T>()
        {
            var items = new List<PlainFacadeItem>();
            foreach (string eItem in Enum.GetNames(typeof(T)))
            {
                var item = new PlainFacadeItem()
                {
                    ID = Enum.Parse(typeof(T), eItem).ToString(),
                    Name = eItem.ToString(),
                    Description = "",
                    SortCode = Enum.Parse(typeof(T), eItem).ToString()
                };
                items.Add(item);
            }
            return items;
        }

    }

}
