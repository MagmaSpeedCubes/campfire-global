using UnityEngine;

using MagmaLabs;
namespace MagmaLabs.Economy{
    public abstract class SavableBase : ScriptableObject, Savable
    {
        public string id, name; 
        public SerializableDictionary<string> tags = new SerializableDictionary<string>();

        public virtual string Serialize()
        {
            return JsonUtility.ToJson(this);
        }

        public virtual void LoadFromSerialized(string serialized)
        {
            if (string.IsNullOrWhiteSpace(serialized))
            {
                return;
            }

            JsonUtility.FromJsonOverwrite(serialized, this);
            EnsureInitialized();
        }

        protected virtual void OnEnable()
        {
            EnsureInitialized();
        }

        protected virtual void OnValidate()
        {
            EnsureInitialized();
        }

        protected void EnsureInitialized()
        {
            if (tags == null)
            {
                tags = new SerializableDictionary<string>();
            }
        }
    }

}
