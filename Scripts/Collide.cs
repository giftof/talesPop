using UnityEngine;
using Newtonsoft.Json;



namespace TalesPop.Objects
{
    public interface IObject
    {
        [JsonProperty]
        public int Uid { get; set; }
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public int NameId { get; set; }
        [JsonProperty]
        public int GroupId { get; set; }
        [JsonProperty]
        public int? SlotId { get; set; }
        public T2 ParentObject<T2>();
    }

    public interface ISwapableObject<T> : IObject
    {
        public void Remove(int UID);
        public void Add(T obj);
        public bool EnableDestination { get; set; }
        public bool EnableSource { get; set; }
        public void TakeItem(T item);
        /*public ISwap Swap();*/
    }


/*
    internal interface ISwap
    {
        public void Perform(Item a, Item b);
    }
*/
    public interface ISwap
    {
        public void Perform<T>(T a, T b) where T : ISwapableObject<T>;
    }
/*
    internal class Swap: ISwap
    {
        public void Perform(Item a, Item b)
        {
            if (a == null || b == null)
                return;

            Inventory inventory1 = a.SearchParentContainer();
            Inventory inventory2 = b.SearchParentContainer();

            if (inventory1 == null || inventory2 == null)
                return;

            inventory1.Remove(a.uid);
            inventory2.Remove(b.uid);

            int slotId = (int)a.slotId;
            int groupId = a.groupId;

            a.slotId = b.slotId;
            b.slotId = slotId;

            a.groupId = b.groupId;
            b.groupId = groupId;

            inventory1.Add(b);
            inventory2.Add(a);
        }
    }
*/
    internal class Swap : ISwap
    {
        public void Perform<T>(T a, T b) where T : ISwapableObject<T>
        {
            if (a == null || b == null)
                return;

            if (!(a.ParentObject<T>() is ISwapableObject<T> obj1) || !(b.ParentObject<T>() is ISwapableObject<T> obj2))
                return;

            obj1.Remove(a.Uid);
            obj2.Remove(b.Uid);

            int slotId = (int)a.SlotId;
            int groupId = a.GroupId;

            a.SlotId = b.SlotId;
            b.SlotId = slotId;

            a.GroupId = b.GroupId;
            b.GroupId = groupId;

            obj1.Add(b);
            obj2.Add(a);
        }
    }
/*
    public interface ICollide
    {
        public void Perform(Item destination, Item source);
    }
*/
    public interface ICollide
    {
        public void Perform<T>(T destination, T source) where T : ISwapableObject<T>;
    }
/*
    internal class StackBase : ICollide
    {
        public void Perform(Item destination, Item source)
        {
            if (!source.nameId.Equals(destination?.nameId))
                swap.Perform(destination, source);
            else
                Merge(destination, source);
        }

        private void Merge(Item destination, Item source)
        {
            destination.Increment(source.Decrement(destination.Space));

            if (source.Occupied == 0)
                source.Remove();
        }

        private readonly ISwap swap = new Swap();
    }
*/
    internal class StackBase : ICollide
    {
        public void Perform<T>(T destination, T source) where T : ISwapableObject<T>
        {
            if (!source.NameId.Equals(destination?.NameId))
                swap.Perform(destination, source);
            else
                Merge(destination, source);
        }

        private void Merge<T>(T destination, T source)
        {
            destination.Increment(source.Decrement(destination.Space));

            if (source.Occupied == 0)
                source.Remove();
        }

        private readonly ISwap swap = new Swap();
    }
/*
    internal class SolidBase : ICollide
    {
        public void Perform(Item destination, Item source) => swap.Perform(destination, source);

        private readonly ISwap swap = new Swap();
    }
*/
    internal class SolidBase : ICollide
    {
        public void Perform<T>(T destination, T source) where T : ISwapableObject<T> => swap.Perform(destination, source);

        private readonly ISwap swap = new Swap();
    }
/*
    internal class InventoryBase : ICollide
    {
        public void Perform(Item destination, Item source)
        {
            if (source is Inventory inventory)
            {
                if (destination is ExtraPouch && inventory is ExtraPouch)
                {
                    swap.Perform(destination, source);
                    return;
                }

                Item[] array = inventory.ContentArray;

                foreach (Item item in array)
                    Perform(destination, item);
            }
            else
                (destination as Inventory).TakeItem(source);
        }

        private readonly ISwap swap = new Swap();
    }
*/
    internal class InventoryBase : ICollide
    {
        public void Perform<T>(T destination, T source) where T : ISwapableObject<T>
        {
            if (source.EnableSource)
            {
                destination.
            }


            if (source is Inventory inventory)
            {
                if (destination is ExtraPouch && inventory is ExtraPouch)
                {
                    swap.Perform(destination, source);
                    return;
                }

                Item[] array = inventory.ContentArray;

                foreach (Item item in array)
                    Perform(destination, item);
            }
            else
                (destination as Inventory).TakeItem(source);
        }

        private readonly ISwap swap = new Swap();
    }
}


