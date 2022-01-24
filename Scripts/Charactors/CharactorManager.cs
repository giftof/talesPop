using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TalesPop.Objects;
using TalesPop.Objects.Items;
using TalesPop.Datas;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;





namespace TalesPop.Objects.Charactors
{
    using static Common;

    public class CharactorManager
    {
        private static readonly Dictionary<int, Squad> container = new Dictionary<int, Squad>();
        private CharactorFactory factory;

        public CharactorManager()
        {
            factory = new NormalCharactorFactory();
        }

        public static Charactor Selected { get; set; }
        public static Charactor Collided { get; set; }

        /*
         * Behaviours
         */
        public Squad CreateSquad(string json)
        {
            JObject jObject = JObject.Parse(json);
            SquadType squadType = StringToEnum<SquadType>(jObject[SquadArgs.side].Value<string>());

            return CreateSquad(jObject, squadType);
        }

        public void Clear() => container.Clear();

        /*
         * Privates
         */
        private Squad CreateSquad(JObject jObject, SquadType squadType)
        {
            return null;
        }

        private Charactor CreateMembers(JObject jObject)
        {
            return null;
        }


    }



    public static class CharactorManagerExtension
    {
        public static void ToSelected(this Charactor charactor) => CharactorManager.Selected = charactor;
        public static void ToCollided(this Charactor charactor) => CharactorManager.Collided = charactor;
    }
}
