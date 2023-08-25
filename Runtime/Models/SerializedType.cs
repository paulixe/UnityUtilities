using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace Utilities
{
    [Serializable]
    public class SerializedType : ISerializationCallbackReceiver
    {
        public Type type;
        [SerializeField] private string typeNameSerialized;
        private string TypeString
        {
            get
            {

                if (type == null)
                    return null;
                return type.AssemblyQualifiedName;
            }
            set
            {
                if (value == null)
                    type = null;
                else
                {
                    type = Type.GetType(value);
                }
            }
        }

        public SerializedType()
        {
            type = null;
        }
        public SerializedType(Type t)
        {
            type = t;
        }
        public void OnBeforeSerialize()
        {
            typeNameSerialized = TypeString;
        }

        public void OnAfterDeserialize()
        {
            TypeString = typeNameSerialized;
        }
        static public implicit operator Type(SerializedType stype)
        {
            return stype.type;
        }
        static public implicit operator SerializedType(Type t)
        {
            return new SerializedType(t);
        }

        // overload the == and != operators
        public static bool operator ==(SerializedType a, SerializedType b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.type == b.type;
        }
        public static bool operator !=(SerializedType a, SerializedType b)
        {
            return !(a == b);
        }
        // we don't need to overload operators between SerializedType and System.Type because we already enabled them to implicitly convert

        public override int GetHashCode()
        {
            return type.GetHashCode();
        }

        // overload the .Equals method
        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to SerializedType return false.
            SerializedType p = obj as SerializedType;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (type == p.type);
        }
        public bool Equals(SerializedType p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (type == p.type);
        }


    }
}
