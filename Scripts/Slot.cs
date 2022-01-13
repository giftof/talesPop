using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TalesPop.Items
{

    public abstract class Slot
    {
        internal SlotType slotType = 0;
        internal int slotId;
        internal Item item;

        public Slot(int slotId, SlotType slotType)
        {
            this.slotId = slotId;
            this.slotType = slotType;
        }

        /*
         * Behaviours
         */
        //public bool EnableInclude
        //{
        //    get
        //    {
        //        if (((int)slotType & (int)item.itemType) == (int)item.itemType)
        //            return true;
        //        return false;
        //    }
        //}
        public bool IsEmpty
        {
            get { return item == null; }
        }

        public abstract bool EnableInclude(ItemType itemType);

        /*
         * Privates
         */
        protected int GetUseableSlotTypeNumber
        {
            get
            {
                return IsEmpty ? (int)slotType : 0;
            }
        }

    }

    internal class AnySlot: Slot
    {
        public AnySlot(int slotId, SlotType slotType): base(slotId, slotType)
        {
        }

        /*
         * Override
         */
        public override bool EnableInclude(ItemType _)
        {
            return true;
        }
    }

    internal class HandSlot: Slot
    {
        internal HandSlot pairSlot;

        public HandSlot(int slotId, SlotType slotType, int extraHand = 0): base(slotId, slotType)
        {
            if (0 < extraHand)
            {
                pairSlot = new HandSlot(slotId + 1, slotType ^ 0x00);
                Debug.LogWarning($"pair slotId = {slotId + 1}");
                Debug.LogWarning($"pair slotType = {slotType ^ 0x00}");
            }
        }

        /*
         * Override
         */
        public override bool EnableInclude(ItemType itemType)
        {
            return ((GetUseableSlotTypeNumber | pairSlot.GetUseableSlotTypeNumber) & (int)itemType) == (int)itemType;
        }
    }
}
