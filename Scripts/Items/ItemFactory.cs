using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;





namespace TalesPop.Items
{
    using static Common;

    public abstract class Factory
    {
        public abstract Item Create(ItemType itemType, JObject jObject);
    }

    internal class Normal : Factory
    {
        public override Item Create(ItemType itemType, JObject jObject)
        {
            return itemType switch
            {
                ItemType.Amulet => new Amulet(jObject),
                ItemType.Armor => new Armor(jObject),
                ItemType.Bag => new Bag(jObject),
                ItemType.Helmet => new Helmet(jObject),
                ItemType.Material => new Material(jObject),
                ItemType.Potion => new Potion(jObject),
                ItemType.Shield => new Shield(jObject),
                ItemType.TwoHand => new TwoHand(jObject),
                ItemType.Weapon => new Weapon(jObject),
                _ => null,
            };
        }
    }

    internal class Blessed : Factory
    {
        public override Item Create(ItemType itemType, JObject jObject) => null;
    }

    internal class Cursed : Factory
    {
        public override Item Create(ItemType itemType, JObject jObject) => null;
    }
}