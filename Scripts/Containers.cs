using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;



namespace TalesPop.Datas
{
    sealed public class TalesPopContainer<Key, Value> where Value : class
    {
        internal readonly Dictionary<Type, int> indexer;
        internal readonly Dictionary<Key, Value> container;
        internal List<Dictionary<Key, Value>> extraContainer;



        public TalesPopContainer()
        {
            container = new Dictionary<Key, Value>();
            extraContainer = new List<Dictionary<Key, Value>>();
            indexer = new Dictionary<Type, int>();
        }

        /*
         * Behaviours
         */
        public void AppendContainerType(Type type)
        {
            if (0 < container.Count)
                throw new Exception("Add ContainerType Before input any data");

            if (indexer.ContainsKey(type))
                return;

            indexer.Add(type, extraContainer.Count);
            extraContainer.Add(new Dictionary<Key, Value>());
        }

        public Value Search(Key key) => container[key];

        public Value SearchBy(Key key, params Type[] containerValueType)
        {
            foreach (Type type in containerValueType)
            {
                if (extraContainer[indexer[type]].ContainsKey(key))
                    return extraContainer[indexer[type]][key];
            }

            return null;
        }

        public void Add(Key key, Value value)
        {
            if (container.ContainsKey(key))
                throw new Exception("Duplicated key");

            var array = from item in indexer
                        where IsSubOrSameType(value, item.Key)
                        select item.Value;

            foreach (int idx in array)
                extraContainer[idx].Add(key, value);

            container.Add(key, value);
        }

        public void Remove(Key key)
        {
            container.Remove(key);

            foreach (var element in extraContainer)
                if (element.ContainsKey(key))
                    element.Remove(key);
        }

        /*
         * Private
         */
        private bool IsSubOrSameType(Value value, Type type) => value.GetType().IsSubclassOf(type) || value.GetType().Equals(type);

        /*
         * TEST_CODE
         */
        public void SHOW_ALL_CONTENTS()
        {
            Debug.Log("----- main container -----");
            foreach (var item in container)
            {
                Debug.Log($"key = {item.Key}, value = {item.Value}");
            }

            foreach (var item in extraContainer)
            {
                Debug.Log("----- new extra container -----");
                foreach (var element in item)
                {
                    Debug.Log($">> key = {element.Key}, value = {element.Value}");
                }
            }
        }
    }
}
