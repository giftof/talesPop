using UnityEngine.Events;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace TalesPop.Items
{
    using static Common;

    //public delegate bool EnableSwap(Item item1, Item item2);
    public delegate T1 T_DELEGATE_T<T1, T2>(T2 _);
    public delegate T1 T_DELEGATE_TT<T1, T2, T3>(T2 _, T3 __);

    internal static class ItemArgs
    {
        public const string uid             = "uid";
        public const string name            = "name";
        public const string nameId          = "nameId";
        public const string itemType        = "itemType";
        public const string capacity        = "capacity";
        public const string slotId          = "slotId";

        public const string amount          = "amount";
        public const string charge          = "charge";
        public const string contents        = "contents";
        public const string spellUIDArray   = "spellUIDArray";
        public const string inventoryType   = "inventoryType";
    }
    
    public abstract class Item //: IItem
    {
        [JsonProperty]
        public int uid;
        [JsonProperty]
        public string name;
        [JsonProperty]
        public int nameId;
        [JsonProperty]
        public ItemType itemType;
        [JsonProperty]
        public int capacity;
        [JsonProperty]
        public int groupId;
        [JsonProperty]
        public int? slotId;
        [JsonProperty]
        public int materialId;

        [JsonIgnore]
        protected JObject jObject;

        public Item(string json)
        {
            jObject = JObject.Parse(json);
            SetProperties();
        }

        public Item(JObject jObject)
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
        [JsonIgnore]
        internal T_DELEGATE_T<bool, int> searchWrapper;

        public void Interact()
        {
            interact?.Perform();
        }

        public void Collide(Item source)
        {
           collide?.Perform(this, source);
        }

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

        public void Remove()
        {
            remove?.Invoke(groupId, uid);
        }

        public Bag SearchParentContainer()
        {
            if (searchBag?.Invoke(groupId) is Bag bag)
                return bag;

            return null;
        }

        [JsonIgnore]
        public abstract int Space { get; }
        [JsonIgnore]
        public abstract int Occupied { get; internal set; }

        /*
         * Privates
         */
        private void SetProperties()
        {
            uid      = jObject[ItemArgs.uid]     .Value<int>   ();
            name     = jObject[ItemArgs.name]    .Value<string>();
            nameId   = jObject[ItemArgs.nameId]  .Value<int>   ();
            capacity = jObject[ItemArgs.capacity].Value<int>   ();
            slotId   = jObject[ItemArgs.slotId]? .Value<int>   ();
        }
    }



}
