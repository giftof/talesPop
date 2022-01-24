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
    //internal static class 



    public abstract class Charactor : TalesObject
    {
        [JsonProperty]
        public Item equip;
        [JsonProperty]
        public Item pouch;
        [JsonProperty]
        public int size;



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

    public class Knight : Charactor
    {
        internal Knight(JObject jObject) : base(jObject)
        {
            //this->pouch = 
        }
    }
}
