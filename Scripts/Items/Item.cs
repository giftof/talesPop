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
    
    public abstract class Item : TalesObject
    {
        [JsonProperty]
        public ItemType itemType;
        [JsonProperty]
        public int? capacity;
        [JsonProperty]
        public int groupId;
        [JsonProperty]
        public int? slotId;
        [JsonProperty]
        public int materialId;

        public Item(string json) : base(json) => SetProperties();

        public Item(JObject jObject) : base(jObject)
        {
            this.jObject = jObject;
            SetProperties();
        }

        /*
         * Behaviours
         */
        [JsonIgnore]
        internal IInteraction interact;
        [JsonIgnore]
        internal ICollide collide;
        [JsonIgnore]
        internal UnityAction<int, int> remove;
        [JsonIgnore]
        internal T_DELEGATE_T<Item, int> searchBag;

        //public void Interact() => interact?.Perform();
        public void Interact()
        {
Debug.Log($"interact = {interact}");
            interact?.Perform();
        }

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

        public void Remove() => remove?.Invoke(groupId, uid);

        public Inventory SearchParentContainer()
        {
            if (searchBag?.Invoke(groupId) is Inventory inventory)
                return inventory;
            return null;
        }

        [JsonIgnore]
        public abstract int Space { get; }
        [JsonIgnore]
        public abstract int Occupied { get; internal set; }
        [JsonIgnore]
        public int GetGroupId => groupId;
        /*
         * Privates
         */
        private void SetProperties()
        {
            capacity = jObject[ItemArgs.capacity]?.Value<int>();
            slotId = jObject[ItemArgs.slotId]? .Value<int>();
        }
    }
}
