using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace TalesPop.Items
{
    public enum Category
    {
        Stackable,
        Solid,
        Bag,
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



    public abstract class Item
    {
        [JsonProperty]
        public int uid;
        [JsonProperty]
        public string name;
        [JsonProperty]
        public int nameId;
        [JsonProperty]
        public Category category;
        [JsonProperty]
        public int capacity;
        [JsonProperty]
        public int maxCapacity;

        [JsonIgnore]
        public JObject parsed;

        public Item(string json)
        {
            JsonParse(json);
        }

        public abstract void Operate();

        /********************************/
        /* Privates						*/
        /********************************/

        private void JsonParse(string json)
        {
            JObject jObject = JObject.Parse(json);

            uid = jObject[ItemArgs.uid].Value<int>();
            name = jObject[ItemArgs.name].Value<string>();
            nameId = jObject[ItemArgs.nameId].Value<int>();
            category = Common.StringToEnum(jObject[ItemArgs.category].Value<string>(), category);
            capacity = jObject[ItemArgs.capacity].Value<int>();
            maxCapacity = jObject[ItemArgs.maxCapacity].Value<int>();
            parsed = jObject;
        }
    }



    public class Stackable : Item
    {
        public Stackable(string json) : base(json)
        {
            //jObject.ToObject<Stackable>();
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
            //jObject.ToObject<Solid>();
        }

        public override void Operate()
        {
            // operate common solid
            throw new System.NotImplementedException();
        }
    }



    sealed public class Bag: Item
    {
        private Dictionary<int, Item> bag = new Dictionary<int, Item>();

        public Bag(string json) : base(json)
        {
            //jObject.ToObject<Bag>();
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
            //jObject.ToObject<Stackable>();
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
            //jObject.ToObject<Weapon>();
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
            //jObject.ToObject<Armor>();
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
