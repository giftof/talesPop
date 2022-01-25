using System.Collections;
using System.Collections.Generic;
using UnityEngine;





namespace TalesPop.Objects
{
    public interface IInteraction
    {
        public void Perform();
    }

    public class ToggleBag : IInteraction
    {
        public void Perform()
        {
            Debug.Log("[IMPL: IItemInteraction] Perform by ToggleBag");
        }
    }

    public class Use : IInteraction
    {
        public void Perform()
        {
            Debug.Log("[IMPL: IItemInteraction] Perform by Use");
        }
    }

    public class Craft : IInteraction
    {
        public void Perform()
        {
            Debug.Log("[IMPL: IItemInteraction] Perform by Craft");
        }
    }

    public class Equip : IInteraction
    {
        public void Perform()
        {
            Debug.Log("[IMPL: IItemInteraction] Perform by Equip");
        }
    }
}
