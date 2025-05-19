using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Stat : MonoBehaviour
    {
        [SerializeField] private Image uiBar;

        [SerializeField] private float curValue;
        public float CurValue
        {
            get => curValue;
            set => curValue = Mathf.Clamp(value, 0f, maxValue);
        }
        [SerializeField] private float startValue;
        [SerializeField] private float maxValue;
        [SerializeField] private float passiveValue;
        public float PassiveValue => passiveValue;
        
        private void Start()
        {
            curValue = startValue;
        }
        
        
        private void Update()
        {
            uiBar.fillAmount = GetPercentage();
        }

        private float GetPercentage()
        {
            return curValue / maxValue;
        }

        /// <summary>
        /// 스탯에 변화를 주는 함수
        /// </summary>
        /// <param name="value"> 변경할 스탯의 양. 양수는 증가, 음수는 감소를 의미함 </param>
        public void ChangeStat(float value)
        {
            CurValue += value;
        }
    }
}

