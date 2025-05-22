using UnityEngine;
using UnityEngine.Serialization;

namespace Entity.Player
{
    public class PlayerStat: MonoBehaviour
    {
        [FormerlySerializedAs("heart")]
        [Header("Health")]
        [SerializeField] private Stat health;
        public Stat Health => health;

        [Header("Stamina")]
        [SerializeField] private Stat stamina;
        public Stat Stamina => stamina;
        
        private const float JumpStaminaCost = 10f;


        private void Start()
        {
            health.Init(100f);
            stamina.Init(100f);
        }

        
        private void Update()
        {
            RegenerateStamina();
        }

        
        private void RegenerateStamina()
        {
            if (stamina.CurValue < stamina.MaxValue)
                stamina.Change(stamina.PassiveValue * Time.deltaTime);
        }  

        
        public bool TryUseStamina()
        {
            return (stamina.CurValue >= JumpStaminaCost);
        }
        
        
        public void TakeDamage(float amount)
        {
            health.Change(-amount);
            if (health.IsEmpty)
                Debug.Log("플레이어 사망!");
        }
    }
}