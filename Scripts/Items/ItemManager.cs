using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;



namespace TalesPop.Items
{
    using static Common;

    public class ItemManager
    {
        private readonly Dictionary<int, Bag> container;
        private Stack<Bag> processQueue;

        public ItemManager()
        {
            processQueue = new Stack<Bag>();
            container = new Dictionary<int, Bag>
            {
                { NULL_ID, null }
            };
        }

        public Bag CreateBag(string json)
        {
            return CreateBag(JObject.Parse(json));
        }

        /*
         * Behaviours
         */
        public Item SearchByUID(int uid)
        {
            //container.FirstOrDefault((e, result) =>
            //{
            //    result = e.Value.SearchByUID(uid);
            //    return result != null;
            //}).result;

            Item result = null;
            container.FirstOrDefault(e1 => HaveItem(uid, e1.Value, ref result));
            return result;
        }

        /*
         * Privates
         */
        private Bag CreateBag(JObject jObject)
        {
            if (!IsSame(jObject[ItemArgs.itemType]?.Value<string>(), ItemType.Bag.ToString()))
                return null;

            processQueue.Push(new Bag(jObject));
            JArray jArray = (JArray)jObject[ItemArgs.contents];
            IEnumerable<JToken> itemList = jArray.Select(e => e);

            foreach (JToken token in itemList)
            {
                if (CreateItem(token) == null)
                    return null;
            }

            Bag currentBag = processQueue.Pop();

            if (!container.ContainsKey(currentBag.uid))
                container.Add(currentBag.uid, currentBag);
            else
                currentBag = null;

            return currentBag;
        }

        private Item CreateItem(JToken token)
        {
            JObject jObject = (JObject)token;
            ItemType itemCategory = StringToEnum<ItemType>(jObject[ItemArgs.itemType].Value<string>());
            Item item = IsSame(ItemType.Bag, itemCategory)
                ? CreateBag(jObject)
                : Item.Factory(itemCategory, jObject);

            if (!processQueue.Peek()?.Validate(item) ?? false)
                return null;

            processQueue.Peek().container.Add(item.uid, item);
            return item;
        }

        private bool HaveItem(int uid, Bag bag, ref Item match)
        {
            match = bag?.container.FirstOrDefault(e => e.Value?.uid.Equals(uid) ?? false).Value;
            return match != null;
        }
    }
}