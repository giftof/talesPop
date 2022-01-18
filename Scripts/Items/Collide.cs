using System.Collections;
using System.Collections.Generic;
using UnityEngine;





namespace TalesPop.Objects.Items
{
    using static Common;

    internal interface ISwap
    {
        public void Perform(Item a, Item b);
    }

    internal class Swap: ISwap
    {
        public void Perform(Item a, Item b)
        {
            if (a == null || b == null)
                return;

            Bag bagA = a.SearchParentContainer();
            Bag bagB = b.SearchParentContainer();

            if (bagA == null || bagB == null)
                return;

            int slotId = (int)a.slotId;
            int groupId = a.groupId;

            a.slotId = b.slotId;
            b.slotId = slotId;

            a.groupId = b.groupId;
            b.groupId = groupId;

            bagA.Remove(a.uid);
            bagB.Remove(b.uid);

            bagA.Insert(b);
            bagB.Insert(a);
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
            if (!destination?.nameId.Equals(source?.nameId) ?? false)
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
        public void Perform(Item destination, Item source)
        {
            swap.Perform(destination, source);
        }

        private readonly ISwap swap = new Swap();
    }

    internal class InventoryBase : ICollide
    {
        public void Perform(Item destination, Item source)
        {
            if (destination is Bag bag && bag.EnableTakeItem(source))
                bag.TakeItem(source);
            else
                swap.Perform(destination, source);
        }

        private readonly ISwap swap = new Swap();
    }
}


