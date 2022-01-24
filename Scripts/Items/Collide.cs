using UnityEngine;



namespace TalesPop.Objects.Items
{
    public interface ISwapable<T>
    {
        public void Remove(int UID);
        public void Add(T obj);
    }

    internal interface ISwap
    {
        public void Perform(Item a, Item b);
    }

    //internal class Swap: ISwap
    //{
    //    public void Perform(Item a, Item b)
    //    {
    //        if (a == null || b == null)
    //            return;

    //        Inventory inventory1 = a.SearchParentContainer();
    //        Inventory inventory2 = b.SearchParentContainer();

    //        if (inventory1 == null || inventory2 == null)
    //            return;

    //        inventory1.Remove(a.uid);
    //        inventory2.Remove(b.uid);

    //        int slotId = (int)a.slotId;
    //        int groupId = a.groupId;

    //        a.slotId = b.slotId;
    //        b.slotId = slotId;

    //        a.groupId = b.groupId;
    //        b.groupId = groupId;

    //        inventory1.Add(b);
    //        inventory2.Add(a);
    //    }
    //}
    internal class Swap : ISwap
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

    internal interface ICollide
    {
        public void Perform(Item destination, Item source);
    }

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

    internal class SolidBase : ICollide
    {
        public void Perform(Item destination, Item source) => swap.Perform(destination, source);

        private readonly ISwap swap = new Swap();
    }

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
}


