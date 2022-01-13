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



    sealed public class Bag : Item
    {
        [JsonIgnore]
        public Dictionary<int, Item> bag;
        [JsonIgnore]
        public Item parent = null;
        [JsonProperty]
        public string[] contents;



        private void Initialize()
        {
            itemType = ItemType.Bag;
            interact = new ToggleBag();
            bag = new Dictionary<int, Item>
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

        public void Add(Item item)
        {
            if (!bag.ContainsKey(item.uid) && 0 < Space)
                bag.Add(item.uid, item);
        }

        public void Remove(Item item)
        {
            if (bag.ContainsKey(item.uid))
                bag.Remove(item.uid);
        }

        /*
         * Abstract
         */
        public override int Space
        {
            get { return capacity - bag.Count; }
        }

        public override int Occupied
        {
            get { return bag.Count; }
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
    }
}
