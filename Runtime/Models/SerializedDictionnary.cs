using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Historisation
{
    [Serializable]
    public class SerializedDictionnary<TKey, TValue> : ISerializationCallbackReceiver
    {

        [SerializeField] List<Entry> entries = new List<Entry>();

        public Dictionary<TKey, TValue> Dic = new Dictionary<TKey, TValue>();



        [Serializable]
        struct Entry
        {
            public TKey Key;
            public TValue Value;

            public Entry(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }

        public void OnBeforeSerialize()
        {
            entries.Clear();

            foreach (var pair in Dic)
            {
                entries.Add(new(pair.Key, pair.Value));
            }
        }

        public void OnAfterDeserialize()
        {
            Dic.Clear();
            var duplicates = entries.GroupBy(e => e.Key);
            foreach (var group in duplicates)
            {
                if (group.Count() > 1)
                    Debug.Log("There are several entries with the same key");
                var e = group.First();
                Dic.Add(e.Key, e.Value);

            }


        }
    }
}
