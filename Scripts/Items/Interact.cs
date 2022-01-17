using System.Collections;
using System.Collections.Generic;
using UnityEngine;





namespace TalesPop.Items
{
    internal interface IInteraction
    {
        public void Perform();
    }

    internal class ToggleBag : IInteraction
    {
        public void Perform()
        {
            Debug.Log("[IMPL: IItemInteraction] Perform by ToggleBag");
        }
    }

    internal class Use : IInteraction
    {
        public void Perform()
        {
            Debug.Log("[IMPL: IItemInteraction] Perform by Use");
        }
    }

    internal class Craft : IInteraction
    {
        public void Perform()
        {
            Debug.Log("[IMPL: IItemInteraction] Perform by Craft");
        }
    }

    internal class Equip : IInteraction
    {
        public void Perform()
        {
            Debug.Log("[IMPL: IItemInteraction] Perform by Equip");
        }
    }
}
