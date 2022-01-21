using System;
using System.Collections.Generic;



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

        public Value Find(Key key)
        {
            if (container.ContainsKey(key))
                return container[key];

            throw new Exception($"Dont have such key {key}");
        }

        /*
         * Abstracts
         */
        public abstract void Add(Key key, Value value);
        public abstract void Remove(Key key);
    }





    sealed public class MainContainer<Key, Value> : TalesPopContainer<Key, Value> where Value : class
    {
        private readonly ACQUIRE_KEY_DELEGATE<Key, Value> acquireKeyAction;
        private readonly Dictionary<Key, Dictionary<Key, Value>> mirrorContainer;

        internal MainContainer(ACQUIRE_KEY_DELEGATE<Key, Value> acquireKeyAction) : base()
        {
            this.acquireKeyAction = acquireKeyAction;
            mirrorContainer = new Dictionary<Key, Dictionary<Key, Value>>();
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

        public MirrorContainer<Key, Value> GetMirrorContainer(Key key)
        {
            if (mirrorContainer.ContainsKey(key))
                return new MirrorContainer<Key, Value>(Add, Remove, mirrorContainer[key]);
            return null;
        }

        public void SetMirrorContainer(Key key, Dictionary<Key, Value> mirrorContainer)
        {
            this.mirrorContainer.Add(key, mirrorContainer);

            foreach (var pair in mirrorContainer)
                if (!container.ContainsKey(pair.Key))
                    container.Add(pair.Key, pair.Value);
        }

        public MirrorContainer<Key, Value> MirrorContainer(Key key, Dictionary<Key, Value> mirrorContainer)
        {
            SetMirrorContainer(key, mirrorContainer);
            return GetMirrorContainer(key);
        }

        /*
         * Overrides
         */
        public override void Add(Key key, Value value)
        {
            if (container.ContainsKey(key))
                throw new Exception("[Error] Exist key");

            container.Add(key, value);

            Key dKey = acquireKeyAction(value);

            if (mirrorContainer.ContainsKey(dKey))
                mirrorContainer[dKey].Add(key, value);
        }

        public override void Remove(Key key)
        {
            if (container.ContainsKey(key))
            {
                Key dKey = acquireKeyAction(container[key]);

                container.Remove(key);
                if (mirrorContainer.ContainsKey(dKey))
                    mirrorContainer[dKey].Remove(key);
            }
        }
    }





    sealed public class MirrorContainer<Key, Value> : TalesPopContainer<Key, Value> where Value : class
    {
        internal readonly Dictionary<Key, Value> mirrorContainer;
        internal ADD<Key, Value> add;
        internal REMOVE<Key> remove;

        internal MirrorContainer(ADD<Key, Value> add, REMOVE<Key> remove, Dictionary<Key, Value> mirrorContainer) : base(mirrorContainer)
        {
            this.add = add;
            this.remove = remove;
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
