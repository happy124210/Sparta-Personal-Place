using System;
using UI;
using UnityEngine;


namespace Entity.Player
{
    public interface IDamageable
    {
        void TakeHealthDamage(int damage);
    }
    
    
    public class PlayerStat : MonoBehaviour, IDamageable
    {
        public StatUI statUI;

        private Stat Health { get { return statUI.Health; } }
        private Stat Hunger { get { return statUI.Hunger; } }
        private Stat Stamina { get { return statUI.Stamina; } }

        [SerializeField] private float noHungerHealthDecay;
        public event Action OnTakeDamage;

        private void Update()
        {
            Hunger.ChangeStat(Hunger.PassiveValue * Time.deltaTime);
            Stamina.ChangeStat(Stamina.PassiveValue * Time.deltaTime);

            if (Hunger.CurValue == 0f)
            {
                Health.ChangeStat(noHungerHealthDecay * Time.deltaTime);
            }

            if (Health.CurValue == 0f)
            {
                Die();
            }
        }


        public void Heal(float amount)
        {
            Health.ChangeStat(amount);
        }
        
        
        public void Eat(float amount)
        {
            Hunger.ChangeStat(amount);
        }
        
        
        public void Die()
        {
            Debug.Log("Die");
        }

        public void TakeHealthDamage(int damage)
        {
            Health.ChangeStat(damage);
            OnTakeDamage?.Invoke();
        }
    }
}
