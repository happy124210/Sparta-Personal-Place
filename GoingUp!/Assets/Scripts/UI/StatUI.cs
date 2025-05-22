using Entity;
using Entity.Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StatUI : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private Image staminaBar;

        private Stat healthStat;
        private Stat staminaStat;
        
        private void Start()
        {
            healthStat = CharacterManager.Instance.Player.stat.Health;
            staminaStat = CharacterManager.Instance.Player.stat.Stamina;
        }

        private void Update()
        {
            healthBar.fillAmount = healthStat.GetPercentage();
            staminaBar.fillAmount = staminaStat.GetPercentage();
        }
    }   
}