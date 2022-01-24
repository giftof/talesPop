using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



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

    public abstract class TalesObject
    {
        [JsonProperty]
        public int uid;
        [JsonProperty]
        public string name;
        [JsonProperty]
        public int nameId;
        [JsonProperty]
        public int groupId;
        [JsonProperty]
        public int? slotId;

        [JsonIgnore]
        protected JObject jObject;

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
            uid = jObject[ObjectArgs.uid].Value<int>();
            name = jObject[ObjectArgs.name].Value<string>();
            nameId = jObject[ObjectArgs.nameId].Value<int>();
        }

    }
}
