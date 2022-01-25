using UnityEngine.Events;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;



namespace TalesPop.Objects.Items
{
    using static Common;

    internal static class ItemArgs
    {
        public const string itemType    = "itemType";
        public const string capacity    = "capacity";
        public const string slotId      = "slotId";
    }

    public abstract class Item : TalesObject, IResizeable
    {
        [JsonProperty]
        public ItemType itemType;
        [JsonProperty]
        public int? capacity;
        [JsonProperty]
        public int materialId;

        public Item(string json) : base(json) => SetProperties();

        public Item(JObject jObject) : base(jObject)
        {
            this.jObject = jObject;
            SetProperties();
        }

        //~Item()
        //{
        //    Debug.LogError($"uid ({uid}) DESTROY!");
        //}

        /*
         * Behaviours
         */
        [JsonIgnore]
        internal UnityAction<int, int> remove;
        [JsonIgnore]
        internal T_DELEGATE_T<Item, int> searchBag;

        public void Interact() => interact?.Perform();

        public void Collide(Item source) => collide?.Perform(this, source);

        public virtual int Increment(int amount)
        {
            int increment = Math.Min(amount, Space);

            Occupied += increment;
            return increment;
        }

        public virtual int Decrement(int amount)
        {
            int decrement = Math.Min(amount, Occupied);

            Occupied -= decrement;
            return decrement;
        }

        public void Suicide() => remove?.Invoke(GroupId, Uid);


        /*
         * Implement IObject
         */
        public override IObject ParentObject()
        {
            if (searchBag?.Invoke(GroupId) is Inventory inventory)
                return inventory;
            return null;
        }
/*
        public Inventory SearchParentContainer()
        {
            if (searchBag?.Invoke(GroupId) is Inventory inventory)
                return inventory;
            return null;
        }
*/
        [JsonIgnore]
        public abstract int Space { get; }
        [JsonIgnore]
        public abstract int Occupied { get; internal set; }
        [JsonIgnore]
        public int GetGroupId => GroupId;

        /*
         * Privates
         */
        private void SetProperties()
        {
            capacity = jObject[ItemArgs.capacity]?.Value<int>();
            SlotId = jObject[ItemArgs.slotId]? .Value<int>();
        }
    }
}
