using System.Collections.Generic;
using Newtonsoft.Json.Linq;



namespace TalesPop.Objects.Items
{
    using static Common;

    public abstract class Factory
    {
        public abstract Item Create(ItemType itemType, JObject jObject, ref Dictionary<int, Item> dictionary);
        public abstract Item Create(ItemType itemType, JObject jObject);
    }

    //internal class Normal : Factory
    public class Normal : Factory
    {
        public override Item Create(ItemType itemType, JObject jObject, ref Dictionary<int, Item> dictionary)
        {
            return itemType switch
            {
                ItemType.Pouch => new Pouch(jObject, ref dictionary),
                ItemType.ExtraPouch => new ExtraPouch(jObject, ref dictionary),
                _ => null,
            };
        }

        public override Item Create(ItemType itemType, JObject jObject)
        {
            return itemType switch
            {
                ItemType.Amulet => new Amulet(jObject),
                ItemType.Armor => new Armor(jObject),
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
        public override Item Create(ItemType itemType, JObject jObject, ref Dictionary<int, Item> dictionary) => null;
        public override Item Create(ItemType itemType, JObject jObject) => null;
    }

    internal class Cursed : Factory
    {
        public override Item Create(ItemType itemType, JObject jObject, ref Dictionary<int, Item> dictionary) => null;
        public override Item Create(ItemType itemType, JObject jObject) => null;
    }
}