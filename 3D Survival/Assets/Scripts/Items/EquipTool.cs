using System;
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
        
        private static readonly int Attack = Animator.StringToHash("Attack");
        
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
            if (animator == null) Debug.LogError("No animator attached to " + name);
        }


        public override void OnAttackInput()
        {
            if (attacking) return;
            attacking = true;
            animator.SetTrigger(Attack);
            Invoke(nameof(OnCanAttack), attackRate);
        }


        private void OnCanAttack()
        {
            attacking = false;
        }
    }
}
