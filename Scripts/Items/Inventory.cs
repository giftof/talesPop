using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



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
        public Pouch(JObject jObject, ref Dictionary<int, Item> container) : base(jObject, ref container)
        {
            itemType = ItemType.Pouch;
        }
    }

    sealed public class ExtraPouch : Inventory
    {
        public ExtraPouch(JObject jObject, ref Dictionary<int, Item> container) : base(jObject, ref container)
        {
            itemType = ItemType.ExtraPouch;
        }
    }

    public abstract class Inventory : Item
    {
        [JsonIgnore]
        private readonly Dictionary<int, Item> mirrorContainer;
        //[JsonIgnore]
        //private readonly Dictionary<int, Item> container;
        [JsonProperty]
        public JToken[] contents;
        [JsonProperty]
        public InventoryType inventoryType;

        public Inventory(JObject jObject, ref Dictionary<int, Item> container) : base(jObject)
        {
            mirrorContainer = container;
            Initialize();
        }

        /*
         * Behaviours
         */
        //public void AddForce(Item item)
        //{
        //    container.Add(item.uid, item);
        //}

        //public void Insert(Item item)
        //{
        //    if (!container.ContainsKey(item.uid))
        //        AddForce(item);
        //}

        //public void InsertAndSetData(Item item)
        //{
        //    int? emptySlotId = EmptySlotId();

        //    if (emptySlotId == null || Space == 0)
        //        return;

        //    item.slotId = (int)emptySlotId;
        //    item.groupId = uid;
        //    Insert(item);
        //}

        //public void Remove(int uid)
        //{
        //    if (container.ContainsKey(uid))
        //        container.Remove(uid);
        //}

        //public Item Validate(Item item)
        //{
        //    if (Space <= 0 || container.ContainsKey(item.uid))
        //        return null;

        //    return item;
        //}

        //public Item SearchInclude(int uid)
        //{
        //    foreach (KeyValuePair<int, Item> pair in container)
        //    {
        //        if (pair.Value is ExtraPouch extraPouch)
        //        {
        //            Item result = extraPouch.SearchInclude(uid);

        //            if (result != null)
        //                return result;
        //        }

        //        if (pair.Value.uid.Equals(uid))
        //            return pair.Value;
        //    }

        //    return null;
        //}

        //public int? EmptySlotId(int? presetId)
        //{
        //    if (Space == 0)
        //        return null;

        //    if (presetId != null && container.FirstOrDefault(e => e.Value.slotId.Equals(presetId)).Value == null)
        //        return presetId;

        //    return EmptySlotId();
        //}

        //public int? EmptySlotId()
        //{
        //    int slotId = 0;

        //    if (Space == 0)
        //        return null;

        //    if (Occupied == 0)
        //        return slotId;

        //    IEnumerable<int?> slotList = container.Where(e => e.Value.slotId != null).Select(e => e.Value.slotId).OrderBy(e => e);

        //    foreach (int i in slotList)
        //    {
        //        if (slotId != i)
        //            break;
        //        ++slotId;
        //    }

        //    return slotId;
        //}

        //public void TakeItem(Item item)
        //{
        //    if (item is Stackable)
        //        TakeStackable(item);

        //    if (item is Solidable)
        //        TakeSolidable(item);
        //}

        //public Item[] ContentArray
        //{
        //    get { return container.Values.ToArray(); }
        //}

        /*
         * Abstract
         */
        public override int Space
        {
            //get { return (int)capacity - container.Count; }
            get { return (int)capacity - mirrorContainer.Count; }
        }

        public override int Occupied
        {
            //get { return container.Count; }
            get { return mirrorContainer.Count; }
            internal set { }
        }

        /*
         * Private
         */
        private void Initialize()
        {
            //itemType = ItemType.Bag;
            interact = new ToggleBag();
            collide = new InventoryBase();
            inventoryType = StringToEnum<InventoryType>(jObject[InventoryArgs.inventoryType].Value<string>());
            contents = jObject[InventoryArgs.contents]?.Values<JToken>().ToArray();
        }

        //private void TakeSolidable(Item item)
        //{
        //    if (0 < Space)
        //    {
        //        item.Remove();
        //        InsertAndSetData(item);
        //    }
        //}

        //private void TakeStackable(Item item)
        //{
        //    IEnumerable<Item> array = container.Where(e => e.Value.nameId.Equals(item.nameId)).OrderBy(e => e.Value.slotId).Select(e => e.Value);

        //    if (0 < array.Count())
        //    {
        //        foreach (Item i in array)
        //        {
        //            i.Increment(item.Decrement(i.Space));

        //            if (item.Occupied == 0)
        //                return;
        //        }
        //    }

        //    if (0 < Space && 0 < item.Occupied)
        //        InsertAndSetData(item);
        //}

        /*
         * Protection
         */
        public override int Decrement(int _) => 0;
        public override int Increment(int _) => 0;

        /*
         * TEST_CODE
         */
        //public Dictionary<int, Item> CONTAINER => container;
    }

}
