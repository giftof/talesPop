using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TalesPop.Objects;



namespace TalesPop.Datas
{
    public delegate Key ACQUIRE_KEY_DELEGATE<Key, Value>(Value _);
    public delegate Key[] ACQUIRE_KEY_ARRAY_DELEGATE<Key, Value>(Value _);
    public delegate void ADD<Key, Value>(Key _, Value __);
    public delegate void REMOVE<Key>(Key _);





    public abstract class TalesPopContainer<Key, Value> where Value : class
    {
        protected readonly Dictionary<Key, Value> container;

        public TalesPopContainer() => container = new Dictionary<Key, Value>();

        internal TalesPopContainer(Dictionary<Key, Value> mirrorContainer) => container = mirrorContainer;

        /*
         * Behaviours
         */
        public Value Search(Key key)
        {
            if (container.ContainsKey(key))
                return container[key];
            return null;
        }

        public IReadOnlyDictionary<Key, Value> C => container;

        /*
         * Override
         */
        public bool ContainsKey(Key key) => container.ContainsKey(key);
        public bool ContainsValue(Value value) => container.ContainsValue(value);
        public IEnumerable<Key> Keys => container.Keys;
        public IEnumerable<Value> Values => container.Values;
        public int Count => container.Count;

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
        public abstract void Clear();

        /*
         * Secret
         */
        internal void OriginalAdd(Key key, Value value) => container.Add(key, value);
        internal void OriginalRemove(Key key) => container.Remove(key);
        internal void OriginalClear() => container.Clear();
    }




    /*
     * Must define belows.
     * acquireUID : Value's uid
     * acquireGroupId : Value's groupId
     * acquireChildrenId : Value's contents id array (Always return null, If recursive stack is not allowed)
     */
    sealed public class MainContainer<Key, Value> : TalesPopContainer<Key, Value> where Value : class
    {
        private readonly ACQUIRE_KEY_DELEGATE<Key, Value> getUID;
        private readonly ACQUIRE_KEY_DELEGATE<Key, Value> getGroupId;
        private readonly ACQUIRE_KEY_ARRAY_DELEGATE<Key, Value> getChildrenId;
        private readonly Dictionary<Key, MirrorContainer<Key, Value>> mirrorContainer;

        public MainContainer(ACQUIRE_KEY_DELEGATE<Key, Value> getUID, ACQUIRE_KEY_DELEGATE<Key, Value> getGroupId, ACQUIRE_KEY_ARRAY_DELEGATE<Key, Value> getChildrenId) : base()
        {
            this.getUID = getUID;
            this.getGroupId = getGroupId;
            this.getChildrenId = getChildrenId;
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

        public MirrorContainer<Key, Value> GenerateMirrorContainer(Key key)
        {
            MirrorContainer<Key, Value> mirrorContainer = new MirrorContainer<Key, Value>(new Dictionary<Key, Value>())
            {
                SetAdd = Add,
                SetRemove = Remove,
                SetKey = key
            };

            this.mirrorContainer.Add(key, mirrorContainer);
            return mirrorContainer;
        }

        /*
         * Overrides
         */
        public void Add(Value value)
        {
            Add(getUID(value), value);
        }

        public override void Add(Key key, Value value)
        {
            if (container.ContainsKey(key))
                throw new Exception($"[Error] Exist key ({key})");

            Key dKey = getGroupId(value);

            container.Add(key, value);
            if (mirrorContainer.ContainsKey(dKey))
                mirrorContainer[dKey].OriginalAdd(key, value);
        }

        public override void Remove(Key key)
        {
            if (!container.ContainsKey(key))
                throw new Exception($"[Error] Have not key ({key})");

            Key dKey = getGroupId(container[key]);

            if (mirrorContainer.ContainsKey(dKey))
                mirrorContainer[dKey].OriginalRemove(key);

            if (mirrorContainer.ContainsKey(key))
                RecursiveRemove(container[key]);

            container.Remove(key);
        }

        public override void Clear()
        {
            container.Clear();
            mirrorContainer.Clear();
        }

        /*
         * Private
         */
        private void RecursiveRemove(Value value)
        {
            Key[] childKeyArray = getChildrenId(value);

            foreach (Key key in childKeyArray)
            {
                if (mirrorContainer.ContainsKey(key))
                    RecursiveRemove(container[key]);
                container.Remove(key);
            }

            Key self = getUID(value);

            if (mirrorContainer.ContainsKey(self))
                mirrorContainer.Remove(self);
        }

        /*
         * TEST_CODE
         */
        public void SHOW_CONTENTS()
        {
            Debug.Log("main content begin");
            foreach (var pair in container)
            {
                TalesObject talesObject = pair.Value as TalesObject;
                Debug.Log($"uid = {talesObject.Uid}, name = {talesObject.Name}");
            }
            Debug.Log("main content end");

            Debug.Log("mirror content begin");
            foreach (var mirror in mirrorContainer)
            {
                Debug.Log($"mirror uid = {mirror.Key}");
                mirror.Value.SHOW_CONTENTS();
            }
            Debug.Log("mirror content end");
        }
    }





    sealed public class MirrorContainer<Key, Value> : TalesPopContainer<Key, Value> where Value : class
    {
        internal ADD<Key, Value> add;
        internal REMOVE<Key> remove;
        internal Key key;

        internal MirrorContainer(Dictionary<Key, Value> mirrorContainer) : base(mirrorContainer) { }

        /*
         * Behaviours
         */

        /*
         * Internals
         */
        internal ADD<Key, Value> SetAdd { set { add = value; } }
        internal REMOVE<Key> SetRemove { set { remove = value; } }
        internal Key SetKey { set { key = value; } }

        /*
         * Overrides
         */
        public override void Add(Key key, Value value) => add?.Invoke(key, value);
        public override void Remove(Key key) => remove?.Invoke(key);
        public override void Clear()
        {
            IEnumerable<Key> keys = Keys;

            foreach (Key key in keys)
                Remove(key);
        }

        /*
         * TEST_CODE
         */
        public void SHOW_CONTENTS()
        {
            Debug.Log($"mirror begin");
            foreach (var pair in container)
            {
                TalesObject talesObject = pair.Value as TalesObject;
                Debug.Log($"uid = {talesObject.Uid}, name = {talesObject.Name}");
            }
            Debug.Log($"mirror end");
        }
    }
}
