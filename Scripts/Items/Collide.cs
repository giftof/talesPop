using System.Collections;
using System.Collections.Generic;
using UnityEngine;





namespace TalesPop.Items
{
    internal interface ICollide<T>
    {
        public void Perform(T destination, Item source);
    }

    internal class StackBase : ICollide<Item>
    {
        public void Perform(Item destination, Item source)
        {
            if (destination?.nameId.Equals(source?.nameId) ?? false)
            {

            }
            Debug.Log("[IMPL: ICollide] Perform by Stack");
        }

        private void Stack(Item destination, Item source)
        {
            destination.Increment(source.Decrement(destination.Space));

            if (source.Occupied == 0)
            {
                source.Remove();
            }
        }
    }

    internal class SolidBase : ICollide<Item>
    {
        public void Perform(Item destination, Item source)
        {
            Debug.Log("[IMPL: ICollide] Perform by Solid");
        }
    }
}


