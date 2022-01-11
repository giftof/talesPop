using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace TalesPop.Items
{
    public enum ItemCategory
    {
        Bag,
        Potion,
        Weapon,
        Armor,
    }



    public static class ItemArgs
    {
        public const string uid         = "uid";
        public const string name        = "name";
        public const string nameId      = "nameId";
        public const string category    = "category";
        public const string capacity    = "capacity";
        public const string maxCapacity = "maxCapacity";
    }



    public interface IItem
    {
        public abstract void Operate();
    }



    public abstract class Item : IItem
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
        public int maxCapacity;

        [JsonIgnore]
        protected JObject parsed;

        public Item(string json)
        {
            this.parsed = JObject.Parse(json);
            SetData(GetCategory(parsed));
        }

        public Item(JObject jObject, ItemCategory itemCategory)
        {
            this.parsed = jObject;
            SetData(itemCategory);
        }

        public abstract void Operate();

        /********************************/
        /* Privates						*/
        /********************************/

        protected void SetData(ItemCategory itemCategory)
        {
            uid = parsed[ItemArgs.uid].Value<int>();
            name = parsed[ItemArgs.name].Value<string>();
            nameId = parsed[ItemArgs.nameId].Value<int>();
            capacity = parsed[ItemArgs.capacity].Value<int>();
            maxCapacity = parsed[ItemArgs.maxCapacity].Value<int>();
            category = itemCategory;
        }

        private ItemCategory GetCategory(JObject jObject)
        {
            return Common.StringToEnum<ItemCategory>(jObject[ItemArgs.category].Value<string>());
        }
    }



    public class Stackable : Item
    {
        public Stackable(string json) : base(json)
        {
            // something extra
            // enable use 'parsed'
        }

        public Stackable(JObject jObject, ItemCategory itemCategory) : base(jObject, itemCategory)
        {
            // something extra
            // enable use 'parsed'
        }

        public override void Operate()
        {
            // operate common stackable
            throw new System.NotImplementedException();
        }
    }



    public class Solid : Item
    {
        public Solid(string json) : base(json)
        {
            // something extra
            // enable use 'parsed'
        }

        public Solid(JObject jObject, ItemCategory itemCategory) : base(jObject, itemCategory)
        {
            // something extra
            // enable use 'parsed'
        }

        public override void Operate()
        {
            // operate common solid
            throw new System.NotImplementedException();
        }
    }



    sealed public class Bag: Item
    {
        private readonly Dictionary<int, Item> bag;

        public Bag(string json) : base(json)
        {
            bag = new Dictionary<int, Item>();
            // something extra
            // enable use 'parsed'
        }

        public Bag(JObject jObject, ItemCategory itemCategory) : base(jObject, itemCategory)
        {
            bag = new Dictionary<int, Item>();
            // something extra
            // enable use 'parsed'
        }

        public override void Operate()
        {
            OpenBag();
        }

        private void OpenBag()
        {

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
    }



    sealed public class Potion : Stackable
    {
        public Potion(string json): base(json)
        {
            // something extra
            // enable use 'parsed'
        }

        public Potion(JObject jObject, ItemCategory itemCategory) : base(jObject, itemCategory)
        {
            // something extra
            // enable use 'parsed'
        }

        public override void Operate()
        {
            base.Operate();
            UsePotion();
        }

        private void UsePotion()
        {

        }
    }



    sealed public class Weapon : Solid
    {
        public Weapon(string json): base(json)
        {
            // something extra
            // enable use 'parsed'
        }

        public Weapon(JObject jObject, ItemCategory itemCategory) : base(jObject, itemCategory)
        {
            // something extra
            // enable use 'parsed'
        }

        public override void Operate()
        {
            base.Operate();
            EquipToggle();
        }

        private void EquipToggle()
        {

        }
    }



    sealed public class Armor: Solid
    {
        public Armor(string json): base(json)
        {
            // something extra
            // enable use 'parsed'
        }

        public Armor(JObject jObject, ItemCategory itemCategory) : base(jObject, itemCategory)
        {
            // something extra
            // enable use 'parsed'
        }

        public override void Operate()
        {
            base.Operate();
            EquipToggle();
        }

        private void EquipToggle()
        {

        }
    }
}
