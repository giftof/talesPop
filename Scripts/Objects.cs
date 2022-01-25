using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TalesPop.Objects;


namespace TalesPop.Objects
{
    public delegate T1 T_DELEGATE_T<T1, T2>(T2 _);
    public delegate T1 T_DELEGATE_TT<T1, T2, T3>(T2 _, T3 __);

    public static class ObjectArgs
    {
        public const string uid     = "uid";
        public const string name    = "name";
        public const string nameId  = "nameId";
    }

    public abstract class TalesObject : IObject
    {
        [JsonProperty]
        public int Uid { get; set; }
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public int NameId { get; set; }
        [JsonProperty]
        public int GroupId { get; set; }
        [JsonProperty]
        public int? SlotId { get; set; }

        [JsonIgnore]
        protected JObject jObject;
        [JsonIgnore]
        protected IInteraction interact;
        [JsonIgnore]
        protected ICollide collide;

        public TalesObject(string json)
        {
            jObject = JObject.Parse(json);
            SetProperties();
        }

        public TalesObject(JObject jObject)
        {
            this.jObject = jObject;
            SetProperties();
        }

        /*
         * Behaviours
         */
        // [Implement] get groupId object [Method]

        /*
         * Privates
         */
        private void SetProperties()
        {
            Uid = jObject[ObjectArgs.uid].Value<int>();
            Name = jObject[ObjectArgs.name].Value<string>();
            NameId = jObject[ObjectArgs.nameId].Value<int>();
        }

    }
}
