using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TalesPop.Datas;
using TalesPop.Objects;
using UnityEngine;





namespace TalesPop.Objects.Items
{
    using static Common;



    internal static class InventoryArgs
    {
        public const string contents        = "contents";
        public const string inventoryType   = "inventoryType";
    }



    public enum SlotType
    {
        Any         = 0x1FFF,
        RightHand   = 0x0001,
        LeftHand    = 0x0002,
        Chest       = 0x0004,
        Head        = 0x0008,
        Neck        = 0x0010,

        Some1       = 0x0020,
        Some2       = 0x0040,
        Some3       = 0x0080,
        Some4       = 0x0100,
    }



    public enum InventoryType
    {
        Pouch       = 0x01,
        ExtraPouch  = 0x02,
    }



    sealed public class Pouch : Inventory
    {
        public Pouch(JObject jObject, TalesPopContainer<int, Item> container) : base(jObject, container)
        {
            itemType = ItemType.Pouch;
        }
    }



    sealed public class ExtraPouch : Inventory
    {
        public ExtraPouch(JObject jObject, TalesPopContainer<int, Item> container) : base(jObject, container)
        {
            itemType = ItemType.ExtraPouch;
        }
    }



    public abstract class Inventory : Item, ISwapableObject
    {
        [JsonIgnore]
        private readonly TalesPopContainer<int, Item> mirrorContainer;
        [JsonProperty]
        public JToken[] contents;
        [JsonProperty]
        public InventoryType inventoryType;

        public Inventory(JObject jObject, TalesPopContainer<int, Item> container) : base(jObject)
        {
            mirrorContainer = container;
            Initialize();
        }

        /*
         * Behaviours
         */
        public int? EmptySlotId(int? presetId)
        {
            if (Space == 0)
                return null;

            if (presetId != null && mirrorContainer.C.FirstOrDefault(e => e.Value.SlotId.Equals(presetId)).Value == null)
                return presetId;
            return EmptySlotId();
        }

        public int? EmptySlotId()
        {
            int slotId = 0;

            if (Space == 0)
                return null;

            if (Occupied == 0)
                return slotId;

            IEnumerable<int?> slotList =
                from item in mirrorContainer.C
                where item.Value.SlotId != null
                orderby item.Value.SlotId ascending
                select item.Value.SlotId;

            foreach (int i in slotList)
            {
                if (slotId != i)
                    break;
                ++slotId;
            }

            return slotId;
        }

        public Item[] ContentArray
        {
            get { return mirrorContainer.Values.ToArray(); }
        }

        public int[] KeyArray
        {
            get { return mirrorContainer.Keys.ToArray(); }
        }

        /*
         * Implement ISwapable
         */
        public void Add<Item>(Item item)
        {
            mirrorContainer.Add(item.Uid, item);
        }

        public void Remove(int uid)
        {
            mirrorContainer.Remove(uid);
        }

        public void TakeItem<Item>(Item item)
        {
            if (item is Stackable)
                TakeStackable(item);

            if (item is Solidable)
                TakeSolidable(item);
        }

        public bool EnableDestination { get; set; }
        public bool EnableSource { get; set; }

        /*
         * Abstract
         */
        public override int Space => (int)capacity - mirrorContainer.Count;
        public override int Occupied
        {
            get { return mirrorContainer.Count; }
            internal set { }
        }

        /*
         * Private
         */
        private void Initialize()
        {
            interact = new ToggleBag();
            collide = new InventoryBase();
            inventoryType = StringToEnum<InventoryType>(jObject[InventoryArgs.inventoryType].Value<string>());
            contents = jObject[InventoryArgs.contents]?.Values<JToken>().ToArray();
        }

        private void TakeSolidable(Item item)
        {
            if (0 < Space)
            {
                item.Suicide();
                item.GroupId = Uid;
                item.SlotId = EmptySlotId();
                Add(item);
            }
        }

        private void TakeStackable(Item item)
        {
            Item[] array = mirrorContainer.C
                .Where(e => e.Value.NameId.Equals(item.NameId))
                .OrderBy(e => e.Value.SlotId)
                .Select(e => e.Value)
                .ToArray();

            foreach (Item element in array)
            {
                element.Increment(item.Decrement(element.Space));
                if (item.Occupied == 0)
                    return;
            }

            if (0 < item.Occupied)
                TakeSolidable(item);
        }

        /*
         * Protection
         */
        public override int Decrement(int _) => 0;
        public override int Increment(int _) => 0;

        /*
         * TEST_CODE
         */
        public IReadOnlyDictionary<int, Item> CONTAINER => mirrorContainer.C;
    }

}
