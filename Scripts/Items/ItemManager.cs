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
        private static readonly Dictionary<int, Bag> container = new Dictionary<int, Bag>
        {
            { NULL_ID, null }
        };
        private readonly Stack<Bag> processBag;
        private Factory factory;

        public ItemManager(Factory factory = null)
        {
            this.factory = factory ?? new Normal();
            processBag = new Stack<Bag>();
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
            Item result = null;
            container.FirstOrDefault(e1 => HaveItem(uid, e1.Value, ref result));
            return result;
        }

        public Factory Factory
        {
            get { return factory; }
            set { factory = value; }
        }

        /*
         * Privates
         */
        private Bag CreateBag(JObject jObject)
        {
            if (!IsSame(jObject[ItemArgs.itemType]?.Value<string>(), ItemType.Bag.ToString()))
                return null;
            Bag currentBag = new Bag(jObject);

            processBag.Push(currentBag);
            foreach (JToken element in currentBag.contents)
            {
                if (CreateItem(element) == null)
                    return null;
            }

            currentBag = processBag.Pop();

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
                : factory.Create(itemCategory, jObject);

            if (processBag.Peek()?.Validate(item) == null)
                return null;

            processBag.Peek().container.Add(item.uid, item);
            return item;
        }

        private bool HaveItem(int uid, Bag bag, ref Item match)
        {
            match = bag?.container.FirstOrDefault(e => e.Value?.uid.Equals(uid) ?? false).Value;
            return match != null;
        }
    }
}