using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TalesPop.Datas;



namespace TalesPop.Objects.Items
{
    using static Common;

    public abstract class Factory
    {
        public abstract Item Create(ItemType itemType, JObject jObject, TalesPopContainer<int, Item> container);
        public abstract Item Create(ItemType itemType, JObject jObject);
    }

    //internal class Normal : Factory
    sealed public class NormalItemFactory : Factory
    {
        public override Item Create(ItemType itemType, JObject jObject, TalesPopContainer<int, Item> container)
        {
            return itemType switch
            {
                ItemType.Pouch => new Pouch(jObject, container),
                ItemType.ExtraPouch => new ExtraPouch(jObject, container),
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

            List<TwoHand> twoHandList = new List<TwoHand>();
            twoHandList.Add(new TwoHand(jObject));
            // ...
            twoHandList.GroupBy
        }
    }

    sealed internal class BlessedItemFactory : Factory
    {
        public override Item Create(ItemType itemType, JObject jObject, TalesPopContainer<int, Item> container) => null;
        public override Item Create(ItemType itemType, JObject jObject) => null;
    }

    sealed internal class CursedItemFactory : Factory
    {
        public override Item Create(ItemType itemType, JObject jObject, TalesPopContainer<int, Item> container) => null;
        public override Item Create(ItemType itemType, JObject jObject) => null;
    }
}