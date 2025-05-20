using System;
using ScriptableObjects;
using UnityEngine;

namespace Entity.Player
{
    public class Player : MonoBehaviour
    {
        public PlayerController controller;
        public PlayerStat stat;
        
        public ItemData itemData;
        public Action addItem;
        public Transform dropPosition;

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