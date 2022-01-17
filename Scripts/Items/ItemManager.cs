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
        public Item SearchItem(int inventoryKey, int uid)
        {
            return SearchItemByUIDFromInventoryKey(inventoryKey, uid);
        }

        public Item SearchItem(Item bag, int uid)
        {
            return SearchItemByUIDFromBag(bag, uid);
        }

        public Item SearchItem(int uid)
        {
            return SearchItemByUID(uid);
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

            currentBag = AddRootBag(processBag.Pop());
            return currentBag;
        }

        private Item CreateItem(JToken token)
        {
            JObject  jObject      = (JObject)token;
            ItemType itemCategory = StringToEnum<ItemType>(jObject[ItemArgs.itemType].Value<string>());
            Bag      bag          = processBag.Peek();
            Item     item         = IsSame(ItemType.Bag, itemCategory)
                                        ? CreateBag(jObject)
                                        : factory.Create(itemCategory, jObject);
            int?     slotId       = bag.EmptySlotId(item?.slotId);

            if (slotId == null)
                return null;

            if (bag?.Validate(item) == null)
                return null;

            if (SearchItemByUID(item.uid) != null)
                return null;

            item.groupId = bag.uid;
            item.slotId = (int)slotId;
            item.remove = RemoveDelegate;
            item.searchBag = SearchItem;
            bag.AddForce(item);

            return item;
        }

        private Bag AddRootBag(Bag bag)
        {
            if (processBag.Count == 0)
            {
                if (!container.ContainsKey(bag.uid))
                    container.Add(bag.uid, bag);
                else
                    return null;
            }
            return bag;
        }

        private void RemoveDelegate(int key, int uid)
        {
            if (SearchItemByUID(key) is Bag bag)
            {
                if (bag.container.ContainsKey(uid))
                    bag.container.Remove(uid);
            }

            //if (container.ContainsKey(key))
            //    container[key].Remove(uid);
        }

        //private bool HaveItem(int uid, Bag bag, ref Item match)
        //{
        //    match = bag?.container.FirstOrDefault(e => e.Value?.uid.Equals(uid) ?? false).Value;
        //    return match != null;
        //}

        //private bool IsDeepDuplicated(int uid)
        //{
        //    return currentRootBag.container.FirstOrDefault(e => IsDuplicatedNestedBag(e.Value, uid)).Value != null;
        //}

        //private bool IsDuplicatedNestedBag(Item source, int uid)
        //{
        //    if (source == null)
        //        return false;

        //    if (source.itemType.Equals(ItemType.Bag))
        //        return ((Bag)source).container.FirstOrDefault(e => IsDuplicatedNestedBag(e.Value, uid)).Value != null;
        //    return source.uid.Equals(uid);
        //}



        private Item SearchItemByUIDFromInventoryKey(int key, int uid)
        {
            if (SearchItemByUID(key) is Bag bag)
                return SearchItemByUIDFromBag(bag, uid);

            return null;
        }

        private Item SearchItemByUID(int uid)
        {
            Item result = null;

            foreach (var e in container)
            {
                result = SearchItemByUIDFromBag(e.Value, uid);
                if (result != null)
                    return result;
            }

            return result;
        }

        private Item SearchItemByUIDFromBag(Item source, int uid)
        {
            Item result;

            if (source == null)
                return source;

            if (source is Bag)
            {
                foreach (var e in (source as Bag).container)
                {
                    result = SearchItemByUIDFromBag(e.Value, uid);
                    if (result != null)
                        return result;
                }
            }

            if (source.uid.Equals(uid))
                return source;

            return null;
        }

        /*
         * TEST_CODE
         */
        public int SIZE() => container.Count;
        public Dictionary<int, Bag> CONTAINER() => container;
        public void SHOW_BAG_CONTENTS(int key)
        {
            Debug.LogError($"SHOW BAG CONTENTS -- [bag uid = {key}]");
            if (SearchItem(key) is Bag bag)
            {
                foreach (var e in bag.container)
                {
                    if (e.Value != null)
                        Debug.Log($"item = {e.Value.name}");
                }
            }
        }
    }
}