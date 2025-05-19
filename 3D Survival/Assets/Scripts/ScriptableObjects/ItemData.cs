using System;
using UnityEngine;

namespace ScriptableObjects
{
    public enum ItemType
    {
        Equipable,
        Consumable,
        Resource
    }


    public enum ConsumableType
    {
        Health,
        Hunger
    }

    [Serializable]
    public class ItemDataConsumable
    {
        [SerializeField] private ConsumableType type;
        [SerializeField] private float value;
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
        [SerializeField] private Sprite icon;
        [SerializeField] private GameObject dropPrefab;
        
        [Header("Stacking")]
        [SerializeField] private bool canStack;
        [SerializeField] private float maxStackAmount;
        
        [Header("Consumable")]
        [SerializeField] private ItemDataConsumable[] consumables;
        
        
    }
}
