using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TalesPop.Objects;
using TalesPop.Objects.Items;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;





namespace TalesPop.Objects.Charactors
{
    public abstract class Charactor : TalesObject
    {
        [JsonProperty]
        public Item equip;
        [JsonProperty]
        public Item pouch;

        public Charactor(string json) : base(json)
        {

        }

        public Charactor(JObject jObject) : base(jObject)
        {

        }



        /*
         * private
         */
        private void TEST() { }

    }
}
