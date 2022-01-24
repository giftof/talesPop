using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TalesPop.Objects.Items;
using TalesPop.Datas;



namespace TalesPop.Objects.Charactors
{
    using static Common;

    public abstract class CharactorFactory
    {
        public abstract Charactor Create(CharactorType itemType, JObject jObject, TalesPopContainer<int, Item> pouch);
    }



    sealed public class NormalCharactorFactory : CharactorFactory
    {
        public override Charactor Create(CharactorType charactorType, JObject jObject, TalesPopContainer<int, Item> pouch)
        {
            return charactorType switch
            {
                CharactorType.knight => new Knight(jObject),
                _ => null,
            };
        }
    }

    sealed public class DummyCharactorFactory : CharactorFactory
    {
        public override Charactor Create(CharactorType charactorType, JObject jObject, TalesPopContainer<int, Item> pouch)
        {
            return charactorType switch
            {
                CharactorType.knight => new Knight(jObject),
                _ => null,
            };
        }
    }

    sealed public class EliteCharactorFactory : CharactorFactory
    {
        public override Charactor Create(CharactorType charactorType, JObject jObject, TalesPopContainer<int, Item> pouch)
        {
            return charactorType switch
            {
                CharactorType.knight => new Knight(jObject),
                _ => null,
            };
        }
    }





    sealed public class SquadFactory
    {
        public Squad Create(SquadType squadType, JObject jObject)
        {
            return squadType switch
            {
                SquadType.Allie => new Allie(jObject),
                SquadType.Enemy => new Enemy(jObject),
                _ => null,
            };
        }
    }
}
