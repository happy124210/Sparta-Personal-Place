using System;
using Entity.Player;
using UnityEngine;

namespace UI
{
    public class StatUI : MonoBehaviour
    {
        [SerializeField] private Stat health;
        public Stat Health => health;
        
        [SerializeField] private Stat hunger;
        public Stat Hunger => hunger;
        
        [SerializeField] private Stat stamina;
        public Stat Stamina => stamina;

        private void Start()
        {
            CharacterManager.Instance.Player.stat.statUI = this;
        }
    }
    
    
}
