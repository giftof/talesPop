using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TalesPop.Items
{
    internal interface IInteraction
    {
        public void Perform();
    }

    internal class Use : IInteraction
    {
        public void Perform()
        {
            Debug.Log("[IMPL: IItemInteraction] Perform by Use");
        }
    }

    internal class ToggleBag : IInteraction
    {
        public void Perform()
        {
            Debug.Log("[IMPL: IItemInteraction] Perform by ToggleBag");
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
