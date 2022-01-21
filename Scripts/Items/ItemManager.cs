using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using TalesPop.Datas;



namespace TalesPop.Objects.Items
{
    using static Common;

    public class ItemManager
    {
        private static readonly MainContainer<int, Item> popContainer = new MainContainer<int, Item>(GetGroupId);
        private static readonly Dictionary<int, Inventory> container = new Dictionary<int, Inventory>();
        private readonly Stack<Inventory> processInventory;
        private Inventory currentRootInventory;
        private Factory factory;

        public ItemManager(Factory factory = null)
        {
            this.factory = factory ?? new Normal();
            processInventory = new Stack<Inventory>();
        }

        public Inventory CreateInventory(string json)
        {
            JObject jObject = JObject.Parse(json);
            string typeString = jObject[ItemArgs.itemType].Value<string>();
            ItemType type = StringToEnum<ItemType>(typeString);

            currentRootInventory = null;

            if (type.Equals(ItemType.Pouch) || type.Equals(ItemType.ExtraPouch))
                return CreateInventory(type, jObject);

            return null;
        }

        /*
         * Behaviours
         */
        public Item SearchItem(int itemUID)
        {
            return popContainer.Search(itemUID);
        }

        public Item SearchItemInInventory(int inventoryUID, int itemUID)
        {
            return popContainer.Search(inventoryUID, itemUID);
        }

        public Dictionary<int, Item> SearchInventoryContainer(int inventoryUID)
        {
            return popContainer.SearchDuplicateContainer(inventoryUID);
        }

        public Factory Factory
        {
            get { return factory; }
            set { factory = value; }
        }

        /*
         * Privates
         */
        private Inventory CreateInventory(ItemType type, JObject jObject)
        {
            Inventory currentInventory = factory.Create(type, jObject) as Inventory;

            currentRootInventory = currentRootInventory ?? currentInventory;
            processInventory.Push(currentInventory);

            foreach (JToken element in currentInventory.contents)
            {
                if (CreateItem(element) == null)
                {
                    processInventory.Pop();
                    currentRootInventory = null;
                    return null;
                }
            }

            currentInventory = AddRootInventory(processInventory.Pop());
            return currentInventory;
        }

        private Item CreateItem(JToken token)
        {
            JObject  jObject      = (JObject)token;
            ItemType itemCategory = StringToEnum<ItemType>(jObject[ItemArgs.itemType].Value<string>());
            Inventory inventory   = processInventory.Peek();
            Item     item         = IsSame(ItemType.Pouch, itemCategory) || IsSame(ItemType.ExtraPouch, itemCategory)
                                        ? CreateInventory(itemCategory, jObject)
                                        : factory.Create(itemCategory, jObject);
            int?     slotId       = inventory.EmptySlotId(item?.slotId);

            if (slotId == null)
                return null;

            if (item == null)
                return null;

            if (inventory?.Validate(item) == null)
                return null;

            if (SearchItemByUID(item.uid) != null)
                return null;

            item.groupId = inventory.uid;
            item.slotId = (int)slotId;
            item.remove = RemoveDelegate;
            item.searchBag = SearchItem;
            inventory.AddForce(item);

            return item;
        }

        private Inventory AddRootInventory(Inventory inventory)
        {
            if (processInventory.Count == 0)
            {
                if (!container.ContainsKey(inventory.uid))
                    container.Add(inventory.uid, inventory);
                else
                    return null;
            }
            return inventory;
        }

        private void RemoveDelegate(int key, int uid)
        {
            if (SearchItemByUID(key) is Inventory inventory)
                inventory.Remove(uid);
        }

        //private Item SearchItemByUIDFromInventoryKey(int key, int uid)
        //{
        //    if (SearchItemByUID(key) is Inventory inventory)
        //        return SearchItemByUIDFromBag(inventory, uid);

        //    return null;
        //}

        private Item SearchItemByUID(int uid)
        {
            Item result = null;

            foreach (KeyValuePair<int, Inventory> pair in container)
            {
                result = SearchItemByUIDFromBag(pair.Value, uid);
                if (result != null)
                    return result;
            }

            return result;
        }

        private Item SearchItemByUIDFromBag(Item source, int uid)
        {
            if (source.uid.Equals(uid))
                return source;

            if (source is Inventory inventory)
                return inventory.SearchInclude(uid);

            return null;
        }

        private static int GetGroupId(Item item)
        {
            return item.groupId;
        }

        /*
         * TEST_CODE
         */
        public int SIZE() => container.Count;
        public Dictionary<int, Inventory> CONTAINER() => container;
        public void SHOW_BAG_CONTENTS(int key)
        {
            
            if (SearchItem(key) is Inventory inventory)
            {
Debug.LogWarning($"SHOW BAG CONTENTS -- inventory [uid = {key}] [name = {inventory.name}]");
                foreach (KeyValuePair<int, Item> pair in inventory.CONTAINER)
                    Debug.Log($"item = {pair.Value.name}, uid = {pair.Value.uid}");
            }
        }
    }
}