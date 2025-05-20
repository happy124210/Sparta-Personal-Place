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
        public ConsumableType Type => type;
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
        [SerializeField] private GameObject dropPrefab;
        public GameObject DropPrefab => dropPrefab;
        
        [Header("Stacking")]
        [SerializeField] private bool canStack;
        public bool CanStack => canStack;
        [SerializeField] private float maxStackAmount;
        public  float MaxStackAmount => maxStackAmount;
        
        [Header("Consumable")]
        [SerializeField] private ItemDataConsumable[] consumables;
        public  ItemDataConsumable[] Consumables => consumables;
    }
}
