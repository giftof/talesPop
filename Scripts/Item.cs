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
    public delegate bool EnableSwap(Item item1, Item item2);

    /*
     * enum names MUST SAME with class(type) name
     * enum value MUST MATCH with slotType value
     */
    public enum ItemType
    {
        Weapon      = 0x0001,
        Shield      = 0x0002,
        TwoHand     = 0x0003,
        Armor       = 0x0004,
        Helmet      = 0x0008,
        Amulet      = 0x0010,

        Material    = 0x0100,
        Potion      = 0x0200,
        Bag         = 0x1FFF,
    }

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
        public const string inventoryType   = "inventoryType";
    }



    internal interface IInteraction
    {
        public void Perform();
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

    internal class Use: IInteraction
    {
        public void Perform()
        {
            Debug.Log("[IMPL: IItemInteraction] Perform by Use");
        }
    }

    internal class ToggleBag: IInteraction
    {
        public void Perform()
        {
            Debug.Log("[IMPL: IItemInteraction] Perform by ToggleBag");
        }
    }

    internal class Equip: IInteraction
    {
        public void Perform()
        {
            Debug.Log("[IMPL: IItemInteraction] Perform by Equip");
        }
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

        public void Perform()
        {
            interact?.Perform();
        }

        public void Collide(Item source)
        {
           collide?.Perform(this, source);
        }

        public int Increment(int amount)
        {
            int increment = Math.Min(amount, Space);

            Occupied += increment;
            return increment;
        }

        public int Decrement(int amount)
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



    internal abstract class Stackable : Item
    {
        [JsonProperty]
        public int amount;

        // public Stackable(string json): base(json)
        // {
        //     // something extra
        //     // enable use 'parsed'
        //     amount = jObject[ItemArgs.amount].Value<int>();
        // }

        public Stackable(JObject jObject): base(jObject)
        {
            // something extra
            // enable use 'parsed'
            amount = jObject[ItemArgs.amount].Value<int>();
        }

        /*
         * Abstract
         */
        public override int Space
        {
            get { return capacity - amount; }
        }
        public override int Occupied
        {
            get { return amount; }
            internal set { amount = value; }
        }
    }



    internal abstract class Solid : Item
    {
        // public Solid(string json): base(json)
        // {
        //     // something extra
        //     // enable use 'parsed'
        // }

        public Solid(JObject jObject): base(jObject)
        {
            // something extra
            // enable use 'parsed'
        }

        /*
         * Abstract
         */
        public override int Space // fix [return spell remain count]
        {
            get { return 0; }
        }
        public override int Occupied
        {
            get { return 0; }
            internal set { }
        }
    }



    sealed internal class Potion : Stackable
    {
        private void Initialize()
        {
            itemType = ItemType.Potion;
            interact = new Use();
            collide = new Stack();
        }
        
        // public Potion(string json): base(json)
        // {
        //     Initialize();
        //     // something extra
        //     // enable use 'parsed'
        // }

        public Potion(JObject jObject): base(jObject)
        {
            Initialize();
            // something extra
            // enable use 'parsed'
        }
    }



    sealed internal class Weapon : Solid
    {

        private void Initialize()
        {
            itemType = ItemType.Weapon;
            interact = new Equip();
            //collide = new Charge();
        }

        // public Weapon(string json): base(json)
        // {
        //     Initialize();
        //     // something extra
        //     // enable use 'parsed'
        // }

        public Weapon(JObject jObject): base(jObject)
        {
            Initialize();
            // something extra
            // enable use 'parsed'
        }
    }



    sealed internal class Armor: Solid
    {
        private void Initialize()
        {
            itemType = ItemType.Armor;
            interact = new Equip();
            //collide = new Charge();
        }

        // public Armor(string json): base(json)
        // {
        //     Initialize();
        //     // something extra
        //     // enable use 'parsed'
        // }

        public Armor(JObject jObject): base(jObject)
        {
            Initialize();
            // something extra
            // enable use 'parsed'
        }
    }



    sealed internal class Material: Stackable
    {
        private void Initialize()
        {
            itemType = ItemType.Material;
            interact = new Use();
            //collide = new Blend();
        }

        // public Material(string json): base(json)
        // {
        //     Initialize();
        // }

        public Material(JObject jObject): base(jObject)
        {
            Initialize();
        }
    }
}
