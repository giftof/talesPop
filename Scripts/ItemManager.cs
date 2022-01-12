using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;



namespace TalesPop.Items
{
    using static Common;

    public class ItemManager
    {
        private Dictionary<int, Item> container;

        public ItemManager()
        {
            container = new Dictionary<int, Item>();
        }

        public Item CreateItem(string json)
        {
            JObject jObject = JsonParse(json);
            ItemCategory itemCategory = GetCategory(jObject);
            Item item = null;

            if (GetTypeFromEnumName($"{TP_ITEMS}.{itemCategory}", out Type type))
                // item = (Item)Activator.CreateInstance(type, jObject, itemCategory);
                item = (Item)Activator.CreateInstance(type, jObject);

            return Validate(item);
        }

        /*
         * Privates
         */
        private Item Validate(Item item)
        {
            if (item == null)
                return null;

            if (!container.ContainsKey(item.uid))
            {
                container.Add(item.uid, item);
                return item;
            }
            return null;
        }

        private JObject JsonParse(string json)
        {
            return JObject.Parse(json);
        }

        private ItemCategory GetCategory(JObject jObject)
        {
            return StringToEnum<ItemCategory>(jObject[ItemArgs.category].Value<string>());
        }
    }
}