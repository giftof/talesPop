using UnityEngine;
using Newtonsoft.Json;



namespace TalesPop.Objects
{
    public interface ISwapableObject : IObject
    {
        public void Remove(int UID);
        public void Add<T>(T obj) where T : IObject;
        public bool EnableDestination { get; set; }
        public bool EnableSource { get; set; }
        public void TakeItem<T>(T item);
    }

    public interface ISwap
    {
        public void Perform<T>(T a, T b) where T : IObject;
    }

    public class Swap : ISwap
    {
        public void Perform<T>(T a, T b) where T : IObject
        {
            if (a == null || b == null)
                return;

            if (!(a.ParentObject() is ISwapableObject obj1)
             || !(b.ParentObject() is ISwapableObject obj2))
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

    public interface ICollide
    {
        public void Perform<T>(T destination, T source) where T : IObject;
    }

    public class StackBase : ICollide
    {
        public void Perform<T>(T destination, T source) where T : IObject
        {
            if (!source.NameId.Equals(destination?.NameId))
                swap.Perform(destination, source);
            else
                if (destination is IResizeable dest && source is IResizeable src)
                    Merge(dest, src);
        }

        private void Merge<T>(T destination, T source) where T : IResizeable
        {
            destination.Increment(source.Decrement(destination.Space));

            if (source.Occupied == 0)
                source.Suicide();
        }

        private readonly ISwap swap = new Swap();
    }

    public class SolidBase : ICollide
    {
        public void Perform<T>(T destination, T source) where T : IObject => swap.Perform(destination, source);

        private readonly ISwap swap = new Swap();
    }

    public class InventoryBase : ICollide
    {
        public void Perform<T>(T destination, T source) where T : IObject
        {
            if (!(source is ISwapableObject obj) || !obj.EnableSource)
                return;

            Debug.Log("Before Implement");
            /*
             * If source is EnableSource
             * make empty item from destination
             * swap empty <-> source
             */
        }

        private readonly ISwap swap = new Swap();
    }
}


