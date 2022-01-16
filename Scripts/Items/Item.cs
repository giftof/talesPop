using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace TalesPop.Items
{
    using static Common;

    public delegate bool EnableSwap(Item item1, Item item2);

    internal static class ItemArgs
    {
        public const string uid             = "uid";
        public const string name            = "name";
        public const string nameId          = "nameId";
        public const string itemType        = "itemType";
        public const string capacity        = "capacity";

        public const string amount          = "amount";
        public const string charge          = "charge";
        public const string contents        = "contents";
        public const string spellUIDArray   = "spellUIDArray";
        public const string inventoryType   = "inventoryType";
    }




    //internal interface ISwap<T>
    //{
    //    public EnableSwap EnableSwap { get; set; }

    //    public void Perform(ref T a, ref Item b);
    //}

    internal interface ICollide<T>
    {
       public void Perform(T destination, Item source);
    }

    /*
     * Stackable item
     * only same nameId and have space
     */
    //internal class Swap : ISwap<Item>
    //{
    //    public EnableSwap EnableSwap { get; set; }

    //    public void Perform(ref Item a, ref Item b)
    //    {
    //        if (!EnableSwap(a, b))
    //            return;

    //        int groupId = a.groupId;
    //        int slotId = a.slotId;

    //        a.groupId = b.groupId;
    //        a.slotId = b.slotId;

    //        b.groupId = groupId;
    //        b.slotId = slotId;
    //    }
    //}

    internal class Stack: ICollide<Item>
    {
    //    ISwap<Item> swap = new Swap();

    //    public void SetSwapEnable(EnableSwap enableSwap)
    //    {
    //        swap.EnableSwap = enableSwap;
    //    }

       public void Perform(Item destination, Item source)
       {
           Debug.Log("[IMPL: ICollide] Perform by Stack");
        //    if (destination.nameId.Equals(source?.nameId))
        //    {
        //        destination.Increment(source.Decrement(destination.Space));
        //    }
        //    else
        //    {
                
        //        if (!swap.EnableSwap(destination, source))
        //            return;

        //        swap.Perform(ref destination, ref source);
        //    }

           //destination.relationObserver?.Invoke(destination, source);
       }
    }

    ///*
    // * MagicItem charge spell count
    // * only same spellid
    // */
    //internal class Charge: ICollide<Item>
    //{
    //    public void Perform<T>(T destination, Item source) where T : Item
    //    {
    //        Debug.Log("[IMPL: ICollide] Perform by Charge");
    //    }

    //    public void Perform(Item destination, Item source)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Swap(ref Item a, ref Item b)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    ///*
    // * Bag only
    // */
    //internal class Add: ICollide<Item>
    //{
    //    public void Perform<T>(T destination, Item source) where T : Item
    //    {
    //        Debug.Log("[IMPL: ICollide] Perform by Add");
    //    }

    //    public void Perform(Item destination, Item source)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Swap(ref Item a, ref Item b)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    ///*
    // * material blend => combine two material item
    // */
    //internal class Blend: ICollide<Item>
    //{
    //    public void Perform<T>(T destination, Item source) where T : Item
    //    {
    //        Debug.Log("[IMPL: ICollide] Perform by Blend");
    //    }

    //    public void Perform(Item destination, Item source)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Swap(ref Item a, ref Item b)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    /*
     * Swap is not default export to exception case
     */
    //internal class Swap: ICollide<Item>
    //{
    //    public void Perform<T>(T destination, Item source) where T : Item
    //    {
    //        Debug.Log("[IMPL: ICollide] Perform by Swap");
    //    }

    //    public void Perform(Item destination, Item source)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    void ICollide<Item>.Swap(ref Item a, ref Item b)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}



    
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
        public int slotId;
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
        internal ICollide<Item> collide;
        //[JsonIgnore]
        //internal UnityAction propertyObserver = null;
        //[JsonIgnore]
        //internal UnityAction<ItemCategory, ItemCategory> relationObserver = null;

        //public void AddPropertyObserver(UnityAction unityAction)
        //{
        //    propertyObserver += unityAction;
        //}

        //public void AddRelationObserver(UnityAction<ItemCategory, ItemCategory> unityAction)
        //{
        //    relationObserver += unityAction;
        //}

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
        }

        // private ItemCategory GetCategory(JObject jObject)
        // {
        //     return StringToEnum<ItemCategory>(jObject[ItemArgs.category].Value<string>());
        // }
    }



}
