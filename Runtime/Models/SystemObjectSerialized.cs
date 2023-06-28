using System;
using UnityEngine;

namespace Historisation
{
    [Serializable]
    public struct SystemObjectSerialized : ISerializationCallbackReceiver
    {
        public SystemObjectSerialized(object _val)
        {
            Val = _val;
            valSerialized = ToString(Val);
        }

        // our value of interest
        public object Val { get; private set; }

        [SerializeField, HideInInspector]
        private string valSerialized;


        public object GetValue()
        {
            return Val;
        }

        public void OnBeforeSerialize()
        {
            //valSerialized = ToString(Val);
        }

        public void OnAfterDeserialize()
        {
            Val = ToObject(valSerialized);
        }
        public static object ToObject(string objectSerialized)
        {
            if (objectSerialized.Length == 0)
                return null;
            char type = objectSerialized[0];
            if (type == 'n')
                return null;
            else if (type == 'i')
                return int.Parse(objectSerialized.Substring(1));
            else if (type == 's')
                return objectSerialized.Substring(1);
            else if (type == 'f')
                return float.Parse(objectSerialized.Substring(1));
            else if (type == 'b')
            {
                return bool.Parse(objectSerialized.Substring(1));
            }
            else if (type == 'w')
            {
                string[] v = objectSerialized.Substring(1).Split('|');
                return new Vector2(float.Parse(v[0]), float.Parse(v[1]));
            }
            else if (type == 'v')
            {
                string[] v = objectSerialized.Substring(1).Split('|');
                return new Vector3(float.Parse(v[0]), float.Parse(v[1]), float.Parse(v[2]));
            }

            return null;
        }
        public static string ToString(object obj)
        {
            string res = "";
            if (obj == null)
            {
                res = "n";
                return res;
            }
            var type = obj.GetType();
            if (type == typeof(int))
                res = "i" + obj.ToString();
            else if (type == typeof(string))
                res = "s" + obj.ToString();
            else if (type == typeof(float))
                res = "f" + obj.ToString();
            else if (type == typeof(Color))
            {
                Color32 c = (Color)obj;
                uint v = (uint)c.r + ((uint)c.g << 8) + ((uint)c.b << 16) + ((uint)c.a << 24);
                res = "c" + v;
            }
            else if (type == typeof(Vector2))
            {
                Vector2 v = (Vector2)obj;
                res = "w" + v.x + "|" + v.y;
            }
            else if (type == typeof(Vector3))
            {
                Vector3 v = (Vector3)obj;
                res = "v" + v.x + "|" + v.y + "|" + v.z;
            }
            else if (type == typeof(bool))
            {
                bool v = (bool)obj;
                res = "b" + v;
            }
            return res;
        }
    }
}
