using System.Collections;
using System.Collections.Generic;
using UnityEngine;





namespace TalesPop.Items
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
            Bag bagA = a.SearchParentContainer();
            Bag bagB = b.SearchParentContainer();

            if (bagA == null || bagB == null)
                return;

            // need check a or b is wrapping b or a ;;;;;;;;;;;;

            int slotId = (int)a.slotId;
            int groupId = a.groupId;

            a.slotId = b.slotId;
            b.slotId = slotId;

            a.groupId = b.groupId;
            b.groupId = groupId;

            bagA.Remove(a.uid);
            bagB.Remove(b.uid);

            bagA.Add(b);
            bagB.Add(a);
        }
    }

    internal interface ICollide<T>
    {
        public void Perform(T destination, Item source);
    }

    internal class StackBase : ICollide<Item>
    {
        public void Perform(Item destination, Item source)
        {
            if (!destination?.nameId.Equals(source?.nameId) ?? false)
            {
                swap.Perform(destination, source);
            }
            else
            {
                Merge(destination, source);
            }
            //Debug.Log("[IMPL: ICollide] Perform by Stack");
        }

        private void Merge(Item destination, Item source)
        {
            destination.Increment(source.Decrement(destination.Space));

            if (source.Occupied == 0)
            {
                source.Remove();
            }
        }

        private readonly ISwap swap = new Swap();
    }

    internal class SolidBase : ICollide<Item>
    {
        public void Perform(Item destination, Item source)
        {
            swap.Perform(destination, source);
        }

        private readonly ISwap swap = new Swap();
    }
}


