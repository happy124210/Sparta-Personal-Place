using UnityEngine;

namespace Items
{
    public class EquipTool : Equip
    {
        [SerializeField] private float attackRate;
        [SerializeField] private float attackDistance;
        
        private bool attacking;
        
        [Header("Resource Gathering")]
        [SerializeField] private bool doesGatherResource;

        [Header("Combat")] 
        [SerializeField] private bool doesDealDamage;
        [SerializeField] private int damage;
    }
}
