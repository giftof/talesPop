using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using TalesPop.Datas;



namespace TalesPop.Objects.Items
{
    using static Common;

    public class GroupManager
    {
        private static readonly Dictionary<int, int> groupContainer = new Dictionary<int, int>();

        // key < uid
        // value < groupId
    }

    public class ItemManager
    {
        private static readonly MainContainer<int, Item> popContainer = new MainContainer<int, Item>(GetUID, GetGroupId, GetChildrenId);
        private readonly Stack<Inventory> processInventory;
        private Factory factory;



        public ItemManager(Factory factory = null)
        {
            this.factory = factory ?? new NormalItemFactory();
            processInventory = new Stack<Inventory>();
        }

        public Inventory CreateInventory(string json)
        {
            JObject jObject = JObject.Parse(json);
            string typeString = jObject[ItemArgs.itemType].Value<string>();
            ItemType itemType = StringToEnum<ItemType>(typeString);

            if (IsInventoryType(itemType))
                return CreateInventory(itemType, jObject, true);

            return null;
        }

        /*
         * Behaviours
         */
        public static Item Selected { get; set; }
        public static Item Collided { get; set; }
        public Item SearchItem(int itemUID) => popContainer.Search(itemUID);
        public Item SearchItem(int inventoryUID, int itemUID) => popContainer.Search(inventoryUID, itemUID);
        public Factory Factory
        {
            get { return factory; }
            set { factory = value; }
        }

        public int Size => popContainer.Count;
        public void Clear() => popContainer.Clear();
        public void Remove(int uid) => popContainer.Remove(uid);
        public void Add(Item item) => popContainer.Add(item.Uid, item);

        public void Interact() => Selected?.Interact();
        public void Collide() => Selected?.Collide(Collided);

        /*
         * Privates
         */
        private bool IsInventoryType(ItemType type) => ItemType.Pouch.Equals(type) || ItemType.ExtraPouch.Equals(type);

        private Inventory CreateInventory(ItemType type, JObject jObject, bool first = false)
        {
            int uid = jObject[ObjectArgs.uid].Value<int>();
            MirrorContainer<int, Item> mirrorContainer = popContainer.GenerateMirrorContainer(uid);
            Inventory inventory = factory.Create(type, jObject, mirrorContainer) as Inventory;

            if (first)
                popContainer.Add(inventory.Uid, inventory);
            processInventory.Push(inventory);

            foreach (JToken element in inventory.contents)
            {
                if (CreateItem(element) == null)
                {
                    processInventory.Pop();
                    return null;
                }
            }

            return processInventory.Pop();
        }

        private Item CreateItem(JToken token)
        {
            JObject  jObject    = (JObject)token;
            ItemType itemType   = StringToEnum<ItemType>(jObject[ItemArgs.itemType].Value<string>());
            Inventory inventory = processInventory.Peek();
            Item     item       = IsInventoryType(itemType)
                                        ? CreateInventory(itemType, jObject)
                                        : factory.Create(itemType, jObject);
            int?     slotId     = inventory.EmptySlotId(item?.SlotId);

            if (slotId == null || item == null || inventory?.Space == 0)
                throw new Exception($"[Error: ItemManager: CreateItem] Item is null. something wrong. slotId: {slotId}, item: {item}, space: {inventory?.Space}");

            item.GroupId = inventory.Uid;
            item.SlotId = (int)slotId;
            item.remove = RemoveDelegate;
            item.searchBag = SearchItem;
            inventory.Add(item);

            return item;
        }

        private void RemoveDelegate(int key, int uid)
        {
            Debug.Log($"key = {key}, uid = {uid}");

            if (popContainer[key] is Inventory inventory)
                inventory.Remove(uid);
        }

        private static int GetUID(Item item) => item.Uid;
        private static int GetGroupId(Item item) => item.GroupId;
        private static int[] GetChildrenId(Item item)
        {
            if (item is Inventory inventory)
                return inventory.KeyArray;
            return null;
        }

        /*
         * TEST_CODE
         */
        public IReadOnlyDictionary<int, Item> CONTAINER() => popContainer.C;
        public void SHOW_BAG_CONTENTS(int key)
        {
            if (SearchItem(key) is Inventory inventory)
            {
                Debug.LogWarning($"SHOW BAG CONTENTS -- inventory [uid = {key}] [name = {inventory.Name}]");
                foreach (KeyValuePair<int, Item> pair in inventory.CONTAINER)
                    Debug.Log($"item = {pair.Value.Name}, uid = {pair.Value.Uid}");
            }
        }
        public MainContainer<int, Item> POP_CONTAINER() => popContainer;
    }



    public static class ItemManagerExtension
    {
        public static void ToSelected(this Item item) => ItemManager.Selected = item;
        public static void ToCollided(this Item item) => ItemManager.Collided = item;
    }
}