using System;
using ScriptableObjects;
using UI;
using UnityEngine;

namespace Entity.Player
{
    public class Player : MonoBehaviour
    {
        public PlayerController controller;
        public PlayerStat stat;
        public QuickSlotUI quickslotUI;
        
        public ItemData itemData;
        public Action addItem;

        private void Awake()
        {
            CharacterManager.Instance.Player = this;
        
            controller = GetComponent<PlayerController>();
            if (controller == null) Debug.LogError("PlayerController not found");
            stat = GetComponent<PlayerStat>();
            if (stat == null) Debug.LogError("PlayerStat not found");
        }
    }
}