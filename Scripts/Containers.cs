using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;



namespace TalesPop.Datas
{
    public delegate Key ACQUIRE_KEY_DELEGATE<Key, Value>(Value _);
    public delegate void ADD<Key, Value>(Key _, Value __);
    public delegate void REMOVE<Key>(Key _);





    public abstract class TalesPopContainer<Key, Value> where Value : class
    {
        protected readonly Dictionary<Key, Value> container;

        public TalesPopContainer()
        {
            container = new Dictionary<Key, Value>();
        }

        internal TalesPopContainer(Dictionary<Key, Value> mirrorContainer)
        {
            container = mirrorContainer;
        }

        /*
         * Behaviours
         */
        public Value Search(Key key)
        {
            if (container.ContainsKey(key))
                return container[key];
            return null;
        }

        public void Clear()
        {
            Key[] array = container.Keys.ToArray();

            foreach (Key key in array)
                Remove(key);
        }

        public IReadOnlyDictionary<Key, Value> C => container;

        /*
         * Override
         */
        public bool ContainsKey(Key key)
        {
            return container.ContainsKey(key);
        }

        public bool ContainsValue(Value value)
        {
            return container.ContainsValue(value);
        }

        public int Count
        {
            get { return container.Count; }
        }

        public IEnumerable<Value> Values
        {
            get { return container.Values; }
        }

        public IEnumerable<Key> Keys
        {
            get { return container.Keys; }
        }

        public Value this[Key key]
        {
            get
            {
                if (container.ContainsKey(key))
                    return container[key];

                throw new Exception($"Dont have such key {key}");
            }
        }

        /*
         * Abstracts
         */
        public abstract void Add(Key key, Value value);
        public abstract void Remove(Key key);

        /*
         * Secret
         */
        internal void OriginalAdd(Key key, Value value) => container.Add(key, value);
        internal void OriginalRemove(Key key) => container.Remove(key);
    }





    sealed public class MainContainer<Key, Value> : TalesPopContainer<Key, Value> where Value : class
    {
        private readonly ACQUIRE_KEY_DELEGATE<Key, Value> acquireKeyAction;
        private readonly Dictionary<Key, MirrorContainer<Key, Value>> mirrorContainer;

        public MainContainer(ACQUIRE_KEY_DELEGATE<Key, Value> acquireKeyAction) : base()
        {
            this.acquireKeyAction = acquireKeyAction;
            mirrorContainer = new Dictionary<Key, MirrorContainer<Key, Value>>();
        }

        /*
         * Behaviours
         */
        public Value Search(Key mirrorKey, Key key)
        {
            return (mirrorContainer.ContainsKey(mirrorKey) && mirrorContainer[mirrorKey].ContainsKey(key))
                ? mirrorContainer[mirrorKey][key]
                : null;
        }

        public MirrorContainer<Key, Value> TakeMirrorContainer(Key key)
        {
            if (mirrorContainer.ContainsKey(key))
                return mirrorContainer[key];
            return null;
        }

        public MirrorContainer<Key, Value> GenerateMirrorContainer(Key key)
        {
            MirrorContainer<Key, Value> mirrorContainer = new MirrorContainer<Key, Value>(new Dictionary<Key, Value>())
            {
                SetAdd = Add,
                SetRemove = Remove
            };

            this.mirrorContainer.Add(key, mirrorContainer);
            return mirrorContainer;
        }

        public void RemoveMirrorContainer(Key mirrorKey)
        {
            if (mirrorContainer.ContainsKey(mirrorKey))
            {
                mirrorContainer[mirrorKey].Clear();
                mirrorContainer.Remove(mirrorKey);
            }
        }

        /*
         * Overrides
         */
        public override void Add(Key key, Value value)
        {
            Debug.Log($"container.Count = {container.Count}");
            if (container.ContainsKey(key))
                throw new Exception("[Error] Exist key");

            container.Add(key, value);

            Key dKey = acquireKeyAction(value);

            if (mirrorContainer.ContainsKey(dKey))
                mirrorContainer[dKey].OriginalAdd(key, value);
        }

        public override void Remove(Key key)
        {
            if (container.ContainsKey(key))
            {
                Key dKey = acquireKeyAction(container[key]);

                container.Remove(key);
                if (mirrorContainer.ContainsKey(dKey))
                    mirrorContainer[dKey].OriginalRemove(key);
            }
        }
    }





    sealed public class MirrorContainer<Key, Value> : TalesPopContainer<Key, Value> where Value : class
    {
        internal ADD<Key, Value> add;
        internal REMOVE<Key> remove;

        internal MirrorContainer(Dictionary<Key, Value> mirrorContainer) : base(mirrorContainer)
        {
        }

        /*
         * Behaviours
         */

        /*
         * Internals
         */
        internal ADD<Key, Value> SetAdd
        {
            set { add = value; }
        }

        internal REMOVE<Key> SetRemove
        {
            set { remove = value; }
        }


        /*
         * Overrides
         */
        public override void Add(Key key, Value value)
        {
            add?.Invoke(key, value);
        }

        public override void Remove(Key key)
        {
            remove?.Invoke(key);
        }
    }
}
