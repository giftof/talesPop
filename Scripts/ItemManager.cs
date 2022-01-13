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
        // private Bag currentProcess;
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
            // JObject jObject = JObject.Parse(json);
            // string itemType = jObject[ItemArgs.itemType]?.Value<string>() ?? EMPTY_STRING;
            // string bag = ItemType.Bag.ToString();

            // if (!itemType.Equals(bag))
            //     return null;

            // currentProcess = new Bag(jObject);
            // JArray itemJsonArray = (JArray)jObject[ItemArgs.contents];
            // IEnumerable<JToken> itemList = itemJsonArray.Select(e => e);

            // foreach (JToken element in itemList)
            // {
            //     if (CreateItem(element) == null)
            //     {
            //         return null;
            //     }
            // }

            // return Validate();
        }

        private Bag CreateBag(JObject jObject)
        {
            Debug.Log("CREATE BAG");
            string itemType = jObject[ItemArgs.itemType]?.Value<string>() ?? EMPTY_STRING;
            string bag = ItemType.Bag.ToString();

            if (!itemType.Equals(bag))
                return null;

            processQueue.Push(new Bag(jObject));
            // currentProcess = new Bag(jObject);
            JArray itemJsonArray = (JArray)jObject[ItemArgs.contents];
            IEnumerable<JToken> itemList = itemJsonArray.Select(e => e);

            foreach (JToken element in itemList)
            {
                if (CreateItem(element) == null)
                {
                    return null;
                }
            }

            return Validate();
        }

        private Item CreateItem(JToken token)
        {
            JObject jObject = (JObject)token;
            ItemType itemCategory = StringToEnum<ItemType>(jObject[ItemArgs.itemType].Value<string>());
            Item item = null;

            if (itemCategory.Equals(ItemType.Bag))
            {
                item = CreateBag(jObject);
            }
            else if (GetTypeFromEnumName($"{TP_ITEMS}.{itemCategory}", out Type type))
            {
                item = (Item)Activator.CreateInstance(type, jObject);
            }

            return Validate(item);
        }

        /*
         * Privates
         */
        private Item Validate(Item item)
        {
            Bag currentProcess = this.processQueue.Peek();
            
            if (item == null || currentProcess == null)
                return null;

            if (currentProcess.Validate(item) == null)
                return null;

            currentProcess.container.Add(item.uid, item);

            return item;
        }

        private Bag Validate()
        {
            Bag currentProcess = this.processQueue.Pop();
            
            if (!container.ContainsKey(currentProcess.uid))
            {
                container.Add(currentProcess.uid, currentProcess);
                return currentProcess;
            }

            return null;
        }
    }
}