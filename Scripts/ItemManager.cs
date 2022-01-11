using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;



namespace TalesPop.Items
{
    public class ItemManager
    {
        private Dictionary<int, Item> container;
        private int size;
        //private JObject jObject;

        public ItemManager(int size)
        {
            this.size = size;
            container = new Dictionary<int, Item>();
        }

        public Item CreateItem(string json)
        {
            JObject jObject = JsonParse(json);
            Type type = GetItemType(jObject, GetCategory(jObject));

            return Activator.CreateInstance<>;
        }




        private JObject JsonParse(string json)
        {
            return JObject.Parse(json);
        }

        private ItemCategory GetCategory(JObject jObject)
        {
            return Common.StringToEnum<ItemCategory>(jObject[ItemArgs.category].Value<string>());
        }

        private Type GetItemType(JObject jObject, ItemCategory itemCategory)
        {
            switch (itemCategory)
            {
                case ItemCategory.Bag:
                    return typeof(Bag);
                case ItemCategory.Potion:
                    return typeof(Potion);
                case ItemCategory.Armor:
                    return typeof(Armor);
                case ItemCategory.Weapon:
                    return typeof(Weapon);
                default:
                    return null;
            }
        }
    }
}