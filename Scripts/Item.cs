using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace TalesPop.Items
{
    /*
     * enum value names MUST SAME with class(type) name
     */
    public enum ItemCategory
    {
        Bag,
        Potion,
        Weapon,
        Armor,
        Material,
    }

    internal static class ItemArgs
    {
        public const string uid         = "uid";
        public const string name        = "name";
        public const string nameId      = "nameId";
        public const string category    = "category";
        public const string capacity    = "capacity";
        public const string maxCapacity = "maxCapacity";
    }



    internal interface IItemInteraction
    {
        public void Perform();
    }

    internal interface IItemCollide
    {
        public void Perform<T>(T destination, Item source);
    }

    /*
     * Stackable item
     * only same nameId and have space
     */
    internal class Stack: IItemCollide
    {
        public void Perform<T>(T destination, Item source)
        {
            Debug.Log("[IMPL: IItemCollide] Perform by Stack");
        }
    }

    /*
     * MagicItem charge spell count
     * only same spellid
     */
    internal class Charge: IItemCollide
    {
        public void Perform<T>(T destination, Item source)
        {
            Debug.Log("[IMPL: IItemCollide] Perform by Charge");
        }
    }

    /*
     * Bag only
     */
    internal class Add: IItemCollide
    {
        public void Perform<T>(T destination, Item source)
        {
            Debug.Log("[IMPL: IItemCollide] Perform by Add");
        }
    }

    /*
     * material blend => combine two material item
     */
    internal class Blend: IItemCollide
    {
        public void Perform<T>(T destination, Item source)
        {
            Debug.Log("[IMPL: IItemCollide] Perform by Blend");
        }
    }

    /*
     * Swap is not default export to exception case
     */
    internal class Swap: IItemCollide
    {
        public void Perform<T>(T destination, Item source)
        {
            Debug.Log("[IMPL: IItemCollide] Perform by Swap");
        }
    }

    internal class Use: IItemInteraction
    {
        public void Perform()
        {
            Debug.Log("[IMPL: IItemInteraction] Perform by Use");
        }
    }

    internal class ToggleBag: IItemInteraction
    {
        public void Perform()
        {
            Debug.Log("[IMPL: IItemInteraction] Perform by ToggleBag");
        }
    }

    internal class Equip: IItemInteraction
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
        public ItemCategory category;
        [JsonProperty]
        public int capacity;
        [JsonProperty]
        public int groupId;
        [JsonProperty]
        public int materialId;

        [JsonIgnore]
        protected JObject parsed;

        public Item(string json)
        {
            Debug.Log(">>> json");
            this.parsed = JObject.Parse(json);
            SetProperties();
        }

        public Item(JObject jObject)
        {
            Debug.Log(">>> jObject");
            this.parsed = jObject;
            SetProperties();
        }

        /*
         * Behaviours
         */
        [JsonIgnore]
        internal IItemInteraction interact;
        [JsonIgnore]
        internal IItemCollide collide;

        public void Perform()
        {
            interact.Perform();
        }

        public void Collide(Item source)
        {
            collide.Perform(this, source);
        }

        public int Increment(int amount)
        {
            int increment = 0;

            return increment;
        }

        public int Decrement(int amount)
        {
            int decrement = 0;

            return decrement;
        }

        // public int Space 
        // {
        //     get { return maxCapacity - capacity; }
        // }
        /*
         * Privates
         */
        private void SetProperties()
        {
            uid = parsed[ItemArgs.uid].Value<int>();
            name = parsed[ItemArgs.name].Value<string>();
            nameId = parsed[ItemArgs.nameId].Value<int>();
            capacity = parsed[ItemArgs.capacity].Value<int>();
            // maxCapacity = parsed[ItemArgs.maxCapacity].Value<int>();
            
        }

        // private ItemCategory GetCategory(JObject jObject)
        // {
        //     return StringToEnum<ItemCategory>(jObject[ItemArgs.category].Value<string>());
        // }
    }



    internal abstract class Stackable : Item
    {
        public Stackable(string json): base(json)
        {
            // something extra
            // enable use 'parsed'
        }

        public Stackable(JObject jObject): base(jObject)
        {
            // something extra
            // enable use 'parsed'
        }
    }



    internal abstract class Solid : Item
    {
        public Solid(string json): base(json)
        {
            // something extra
            // enable use 'parsed'
        }

        public Solid(JObject jObject): base(jObject)
        {
            // something extra
            // enable use 'parsed'
        }
    }



    sealed internal class Bag: Item
    {
        private Dictionary<int, Item> bag;

        private void Initialize()
        {
            category = ItemCategory.Bag;
            bag = new Dictionary<int, Item>();
            interact = new ToggleBag();
            collide = new Add();
        }

        public Bag(string json): base(json)
        {
            Initialize();
            // something extra
            // enable use 'parsed'
        }

        public Bag(JObject jObject): base(jObject)
        {
            Initialize();
            // something extra
            // enable use 'parsed'
        }

        public void Add(Item item)
        {
            if (!bag.ContainsKey(item.uid))
                bag.Add(item.uid, item);
        }

        public void Remove(Item item)
        {
            if (bag.ContainsKey(item.uid))
                bag.Remove(item.uid);
        }

        /*
         * Privates
         */
    }



    sealed internal class Potion : Stackable
    {
        private void Initialize()
        {
            category = ItemCategory.Potion;
            interact = new Use();
            collide = new Stack();
        }
        
        public Potion(string json): base(json)
        {
            Initialize();
            // something extra
            // enable use 'parsed'
        }

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
            category = ItemCategory.Weapon;
            interact = new Equip();
            collide = new Charge();
        }

        public Weapon(string json): base(json)
        {
            Initialize();
            // something extra
            // enable use 'parsed'
        }

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
            category = ItemCategory.Armor;
            interact = new Equip();
            collide = new Charge();
        }

        public Armor(string json): base(json)
        {
            Initialize();
            // something extra
            // enable use 'parsed'
        }

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
            category = ItemCategory.Material;
            interact = new Use();
            collide = new Blend();
        }

        public Material(string json): base(json)
        {
            Initialize();
        }

        public Material(JObject jObject): base(jObject)
        {
            Initialize();
        }
    }
}
