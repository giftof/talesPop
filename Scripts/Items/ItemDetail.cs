using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace TalesPop.Items
{
    using static Common;

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

        public Stackable(JObject jObject) : base(jObject)
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
        [JsonProperty]
        public int[] spellUIDArray;
        // public Solid(string json): base(json)
        // {
        //     // something extra
        //     // enable use 'parsed'
        // }

        public Solid(JObject jObject) : base(jObject)
        {
            // something extra
            // enable use 'parsed'
            spellUIDArray = jObject[ItemArgs.spellUIDArray]?.Values<int>().ToArray();
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

        public Potion(JObject jObject) : base(jObject)
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

        public Weapon(JObject jObject) : base(jObject)
        {
            Initialize();
            // something extra
            // enable use 'parsed'
        }
    }



    sealed internal class Armor : Solid
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

        public Armor(JObject jObject) : base(jObject)
        {
            Initialize();
            // something extra
            // enable use 'parsed'
        }
    }



    sealed internal class Amulet : Solid
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

        public Amulet(JObject jObject) : base(jObject)
        {
            Initialize();
            // something extra
            // enable use 'parsed'
        }
    }



    sealed internal class Helmet : Solid
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

        public Helmet(JObject jObject) : base(jObject)
        {
            Initialize();
            // something extra
            // enable use 'parsed'
        }
    }



    sealed internal class Shield : Solid
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

        public Shield(JObject jObject) : base(jObject)
        {
            Initialize();
            // something extra
            // enable use 'parsed'
        }
    }



    sealed internal class TwoHand : Solid
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

        public TwoHand(JObject jObject) : base(jObject)
        {
            Initialize();
            // something extra
            // enable use 'parsed'
        }
    }



    sealed internal class Material : Stackable
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

        public Material(JObject jObject) : base(jObject)
        {
            Initialize();
        }
    }

}