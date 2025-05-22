using System;
using UnityEngine;

namespace ScriptableObjects
{
    public enum ItemType
    {
        Passive,
        Key
    }

    public enum PassiveType
    {
        Stamina,
        Health
    }
    
    [Serializable]
    public class PassiveEffect
    {
        [SerializeField] private PassiveType type;
        public PassiveType Type => type;

        [SerializeField] private float value;
        public float Value => value;
    }
    
    
    [CreateAssetMenu(fileName = "NewItemData", menuName = "ScriptableObjects/ItemData")]
    public class ItemData : ScriptableObject
    {
        [Header("Info")]
        [SerializeField] private string displayName;
        public string DisplayName => displayName;

        [SerializeField] private string description;
        public string Description => description;

        [SerializeField] private ItemType type;
        public ItemType Type => type;

        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;

        [Header("Passive")]
        [SerializeField] private PassiveEffect[] passiveEffects;
        public PassiveEffect[] PassiveEffects => passiveEffects;
        
        [Header("Key")]
        [SerializeField] private string keyId;
        public string KeyId => keyId;
    }
}