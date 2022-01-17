using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace TalesPop.Items
{
    using static Common;

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
        Any         = 0x01,
        UniqueEquip = 0x02,
    }


    sealed public class Bag : Item
    {
        [JsonIgnore]
        public Dictionary<int, Item> container;
        [JsonIgnore]
        public T_DELEGATE_TT<Item, Item, int> searchInclude;

        [JsonProperty]
        public JToken[] contents;
        [JsonProperty]
        public InventoryType inventoryType;



        private void Initialize()
        {
            itemType = ItemType.Bag;
            interact = new ToggleBag();
            collide = new InventoryBase();
            inventoryType = StringToEnum<InventoryType>(jObject[ItemArgs.inventoryType].Value<string>());
            container = new Dictionary<int, Item>
            {
                { NULL_ID, null }
            };
            ++capacity;
        }

        public Bag(JObject jObject) : base(jObject)
        {
            Initialize();
            // something extra
            // enable use 'parsed'
            interact = new ToggleBag();
            contents = jObject[ItemArgs.contents]?.Values<JToken>().ToArray();
        }

        /*
         * Behaviours
         */
        public void AddForce(Item item)
        {
            container.Add(item.uid, item);
        }

        public void Insert(Item item)
        {
            if (!container.ContainsKey(item.uid))
                AddForce(item);
        }

        public void InsertAndSetData(Item item)
        {
            item.slotId = (int)EmptySlotId();
            item.groupId = uid;
            Insert(item);
        }

        public void Remove(int uid)
        {
            if (container.ContainsKey(uid))
                container.Remove(uid);
        }

        public Item Validate(Item item)
        {
            if (item == null)
                return null;

            if (Space <= 0 || container.ContainsKey(item.uid))
                return null;

            if (inventoryType.Equals(InventoryType.UniqueEquip)
                && container.FirstOrDefault(e => IsDuplicateEquipSlot(e.Value, item)).Value != null)
                return null;

            return item;
        }

        public Item SearchInclude(int uid)
        {
            return searchInclude?.Invoke(this, uid);
        }

        public int? EmptySlotId(int? presetId)
        {
            if (Space == 0)
                return null;

            if (presetId != null && container.FirstOrDefault(e => e.Value?.slotId.Equals(presetId) ?? false).Value == null)
                return presetId;

            return EmptySlotId();
        }

        public int? EmptySlotId()
        {
            int slotId = 0;

            if (Space == 0)
                return null;

            IEnumerable<int?> slotList = container.Where(e => e.Value != null && e.Value.slotId != null).Select(e => e.Value.slotId).OrderBy(e => e);

            foreach (int i in slotList)
            {
                if (slotId != i)
                    break;
                ++slotId;
            }

            return slotId;
        }

        public bool EnableTakeItem(Item item)
        {
            if (!(item is Stackable))
                return 0 < Space;

            int amount = item.Occupied;
            var array = container.Where(e => e.Value != null && e.Value.nameId.Equals(item.nameId)).OrderBy(e => e.Value.slotId).Select(e => e.Value);

            foreach (var e in array)
            {
                amount -= e.Space;
            }

            return amount <= 0 || 0 < Space;
        }

        public void TakeItem(Item item)
        {
            if (!(item is Stackable))
            {
                item.Remove();
                InsertAndSetData(item);
                return;
            }

            var array = container.Where(e => e.Value != null && e.Value.nameId.Equals(item.nameId)).OrderBy(e => e.Value.slotId).Select(e => e.Value);
            foreach (var e in array)
            {
                e.Collide(item);
            }

            if (0 < item.Occupied && 0 < Space)
            {
                item.Remove();
                InsertAndSetData(item);
            }
        }

        /*
         * Abstract
         */
        public override int Space
        {
            get { return capacity - container.Count; }
        }

        public override int Occupied
        {
            get { return container.Count; }
            internal set { }
        }

        public override int Decrement(int _)
        {
            return 0;
        }

        public override int Increment(int _)
        {
            return 0;
        }

        /*
         * Privates
         */
        private bool IsDuplicateEquipSlot(Item currentItem, Item newItem)
        {
            if (ITEM_EQUIP_CAP < (currentItem?.itemType ?? ITEM_LAST_ENUM))
                return false;
            return 0 < ((currentItem?.itemType ?? 0) & newItem.itemType);
        }
    }
}
