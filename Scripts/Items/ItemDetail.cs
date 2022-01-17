using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace TalesPop.Items
{
    using static Common;

    internal abstract class Stackable : Item
    {
        [JsonProperty]
        public int amount;

        public Stackable(JObject jObject) : base(jObject)
        {
            // something extra
            // enable use 'parsed'
            amount = jObject[ItemArgs.amount].Value<int>();
            collide = new StackBase();
            interact = new Use();
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

    internal abstract class Mergeable: Stackable
    {
        public Mergeable(JObject jObject): base(jObject)
        {
            interact = new Craft();
        }
    }


    internal abstract class Solid : Item
    {
        [JsonProperty]
        public int[] spellUIDArray;

        public Solid(JObject jObject) : base(jObject)
        {
            // something extra
            // enable use 'parsed'
            spellUIDArray = jObject[ItemArgs.spellUIDArray]?.Values<int>().ToArray();
            collide = new SolidBase();
            interact = null;
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



    internal abstract class Equipable : Solid
    {

        public Equipable(JObject jObject) : base(jObject)
        {
            interact = new Equip();
        }

    }












    sealed internal class Potion : Stackable
    {
        private void Initialize()
        {
            itemType = ItemType.Potion;
        }

        public Potion(JObject jObject) : base(jObject)
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
        }

        public Material(JObject jObject) : base(jObject)
        {
            Initialize();
        }
    }



    sealed internal class Weapon : Equipable
    {

        private void Initialize()
        {
            itemType = ItemType.Weapon;
        }

        public Weapon(JObject jObject) : base(jObject)
        {
            Initialize();
            // something extra
            // enable use 'parsed'
        }
    }



    sealed internal class Armor : Equipable
    {
        private void Initialize()
        {
            itemType = ItemType.Armor;
        }

        public Armor(JObject jObject) : base(jObject)
        {
            Initialize();
            // something extra
            // enable use 'parsed'
        }
    }



    sealed internal class Amulet : Equipable
    {
        private void Initialize()
        {
            itemType = ItemType.Amulet;
        }

        public Amulet(JObject jObject) : base(jObject)
        {
            Initialize();
            // something extra
            // enable use 'parsed'
        }
    }



    sealed internal class Helmet : Equipable
    {
        private void Initialize()
        {
            itemType = ItemType.Helmet;
        }

        public Helmet(JObject jObject) : base(jObject)
        {
            Initialize();
            // something extra
            // enable use 'parsed'
        }
    }



    sealed internal class Shield : Equipable
    {
        private void Initialize()
        {
            itemType = ItemType.Shield;
        }

        public Shield(JObject jObject) : base(jObject)
        {
            Initialize();
            // something extra
            // enable use 'parsed'
        }
    }



    sealed internal class TwoHand : Equipable
    {
        private void Initialize()
        {
            itemType = ItemType.TwoHand;
        }

        public TwoHand(JObject jObject) : base(jObject)
        {
            Initialize();
            // something extra
            // enable use 'parsed'
        }
    }

}