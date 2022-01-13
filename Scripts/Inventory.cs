using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace TalesPop.Items
{
    using static Common;

    public enum SlotType
    {
        Any         = 0x1FFF,
        RightHand   = 0x0001,
        LeftHand    = 0x0002,
        Chest       = 0x0004,
        Head        = 0x0008,
        Neck        = 0x0010,

        Some1       = 0x0020,
        Some2       = 0x0040,
        Some3       = 0x0080,
        Some4       = 0x0100,
    }

    public enum InventoryType
    {
        Any         = 0x01,
        UniqueEquip = 0x02,
    }


    sealed public class Bag : Item
    {
        [JsonIgnore]
        public Dictionary<int, Item> container;
        [JsonIgnore]
        public Item parent = null;
        [JsonProperty]
        public string[] contents;
        [JsonProperty]
        public InventoryType inventoryType;



        private void Initialize()
        {
            itemType = ItemType.Bag;
            interact = new ToggleBag();
            inventoryType = StringToEnum<InventoryType>(jObject[ItemArgs.inventoryType].Value<string>());
            container = new Dictionary<int, Item>
            {
                { NULL_ID, null }
            };
            ++capacity;

            //collide = new Add();
        }

        public Bag(string json) : base(json)
        {
            Initialize();
            // something extra
            // enable use 'parsed'
        }

        public Bag(JObject jObject) : base(jObject)
        {
            Initialize();
            // something extra
            // enable use 'parsed'
        }

        /*
         * Brhaviours
         */
        public void Add(Item item)
        {
            if (!container.ContainsKey(item.uid) && 0 < Space)
                container.Add(item.uid, item);
        }

        public void Remove(Item item)
        {
            if (container.ContainsKey(item.uid))
                container.Remove(item.uid);
        }

        public Item Validate(Item item)
        {
            if (item == null)
                return null;

            if (inventoryType.Equals(InventoryType.UniqueEquip)
                && container.FirstOrDefault(e => IsDuplicatedSlot(e.Value, item)).Value != null)
                return null;

            if (container.ContainsKey(item.uid))
                return null;

            if (Space <= 0)
                return null;

            return item;
        }

        /*
         * Abstract
         */
        public override int Space
        {
            get { return capacity - container.Count; }
        }

        public override int Occupied
        {
            get { return container.Count; }
            internal set { }
        }

        public int Decrement()
        {
            return 0;
        }

        public int Increment()
        {
            return 0;
        }

        /*
         * Privates
         */
        private bool IsDuplicatedSlot(Item currentItem, Item newItem)
        {
            if (ITEM_EQUIP_CAP < (currentItem?.itemType ?? ITEM_LAST_ENUM))
                return false;
            return 0 < ((currentItem?.itemType ?? 0) & newItem.itemType);
        }
    }
}
