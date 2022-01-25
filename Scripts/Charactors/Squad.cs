using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;





namespace TalesPop.Objects.Charactors
{
    using static Common;

    internal static class SquadArgs
    {
        public const string capacity = "capacity";
        public const string contents = "contents";
        public const string side     = "side";
    }

    public enum SquadType
    {
        Allie = 0x01,
        Enemy = 0x02,
    }

    public abstract class Squad : TalesObject
    {
        [JsonIgnore]
        internal Dictionary<int, Charactor> container;
        [JsonProperty]
        public int capacity;
        [JsonProperty]
        public JToken[] contents;



        public Squad(string json) : base(json)
        {
            Initialize();
        }

        public Squad(JObject jObject) : base(jObject)
        {
            Initialize();
        }


        public override IObject ParentObject()
        {
            throw new System.NotImplementedException();
        }

        /*
         * Private
         */
        private void Initialize()
        {
            capacity = jObject[SquadArgs.capacity].Value<int>();
            contents = jObject[SquadArgs.contents]?.Values<JToken>().ToArray();
            container = new Dictionary<int, Charactor>();
        }
    }



    public class Allie : Squad
    {
        public Allie(JObject jObject) : base(jObject)
        {
            foreach (JObject element in contents)
            {
                //container.Add(0, new Charactor(element));
            }
        }

        /*
         * Privates
         */
        private void Initialize()
        {

        }
    }

    public class Enemy : Squad
    {
        public Enemy(JObject jObject) : base(jObject)
        {
            foreach (JObject element in contents)
            {

            }
        }

        /*
         * Privates
         */
        private void Initialize()
        {

        }
    }
}


