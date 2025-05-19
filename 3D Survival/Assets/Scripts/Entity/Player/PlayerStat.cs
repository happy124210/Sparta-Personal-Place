using UI;
using Unity.VisualScripting;
using UnityEngine;


namespace Entity.Player
{
    public class PlayerStat : MonoBehaviour
    {
        public StatUI statUI;

        private Stat Health { get { return statUI.Health; } }
        private Stat Hunger { get { return statUI.Hunger; } }
        private Stat Stamina { get { return statUI.Stamina; } }

        [SerializeField] private float noHungerHealthDecay;

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


        private void Heal(float amount)
        {
            Health.ChangeStat(amount);
        }
        
        
        private void Eat(float amount)
        {
            Hunger.ChangeStat(amount);
        }
        
        
        private void Die()
        {
            Debug.Log("Die");
        }
        
    }
}
