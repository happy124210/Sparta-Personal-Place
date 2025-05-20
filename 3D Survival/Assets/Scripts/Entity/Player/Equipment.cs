using System;
using Items;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity.Player
{
    public class Equipment : MonoBehaviour
    {
        [SerializeField] private Equip curEquip;
        [SerializeField] private Transform equipParent;
        
        private PlayerController controller;
        private PlayerStat stat;

        private void Start()
        {
            controller = GetComponent<PlayerController>();
            if (controller == null) Debug.LogError("No Player Controller found");
            stat = controller.GetComponent<PlayerStat>();
            if (stat == null) Debug.LogError("No Player Stat found");
        }


        public void EquipNew(ItemData data)
        {
            UnEquip();
            curEquip = Instantiate(data.EquipPrefab, equipParent).GetComponent<Equip>();
        }

        public void UnEquip()
        {
            if (curEquip == null) return;
            
            Destroy(curEquip.gameObject);
            curEquip = null;
        }


        public void OnAttackInput(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed && curEquip != null && controller.CanLook == true)
            {
                curEquip.OnAttackInput();
            }
        }
    }
}
