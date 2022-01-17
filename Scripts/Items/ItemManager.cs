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
        private Bag currentRootBag;
        private Factory factory;

        public ItemManager(Factory factory = null)
        {
            this.factory = factory ?? new Normal();
            processBag = new Stack<Bag>();
        }

        public Bag CreateBag(string json)
        {
            JObject jObject = JObject.Parse(json);

            currentRootBag = null;
            if (!IsSame(jObject[ItemArgs.itemType]?.Value<string>(), ItemType.Bag.ToString()))
                return null;

            return CreateBag(jObject);
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
            Bag currentBag = new Bag(jObject);

            currentRootBag = currentRootBag ?? currentBag;
            processBag.Push(currentBag);

            foreach (JToken element in currentBag.contents)
            {
                if (CreateItem(element) == null)
                {
                    processBag.Pop();
                    currentRootBag = null;
                    return null;
                }
            }

            currentBag = processBag.Pop();

            if (processBag.Count == 0)
            {
                if (!container.ContainsKey(currentBag.uid))
                    container.Add(currentBag.uid, currentBag);
                else
                    currentBag = null;
            }

            return currentBag;
        }



        private Item CreateItem(JToken token)
        {
            JObject jObject = (JObject)token;
            ItemType itemCategory = StringToEnum<ItemType>(jObject[ItemArgs.itemType].Value<string>());
            Item item = IsSame(ItemType.Bag, itemCategory)
                ? CreateBag(jObject)
                : factory.Create(itemCategory, jObject);
            Bag bag = processBag.Peek();

            if (bag?.Validate(item) == null)
                return null;

            if (IsDeepDuplicated(item.uid))
                return null;

            item.groupId = bag.uid;
            item.remove = RemoveDelegate;
            bag.container.Add(item.uid, item);

            return item;
        }














        private void RemoveDelegate(int key, int uid)
        {
            if (container.ContainsKey(key))
                container[key].Remove(uid);
        }

        private bool HaveItem(int uid, Bag bag, ref Item match)
        {
            match = bag?.container.FirstOrDefault(e => e.Value?.uid.Equals(uid) ?? false).Value;
            return match != null;
        }

        private bool IsDeepDuplicated(int uid)
        {
            return currentRootBag.container.FirstOrDefault(e => IsDuplicatedNestedBag(e.Value, uid)).Value != null;
        }

        private bool IsDuplicatedNestedBag(Item source, int uid)
        {
            if (source == null)
                return false;

            if (source.itemType.Equals(ItemType.Bag))
                return ((Bag)source).container.FirstOrDefault(e => IsDuplicatedNestedBag(e.Value, uid)).Value != null;
            return source.uid.Equals(uid);
        }
    }
}