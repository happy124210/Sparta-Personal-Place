using UI;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

namespace Entity
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] private float maxValue;
        public float MaxValue => maxValue;

        [SerializeField] private float passiveValue;
        public float PassiveValue => passiveValue;

        private float curValue;
        public float CurValue
        {
            get => curValue;
            private set => curValue = Mathf.Clamp(value, 0f, maxValue);
        }
        
        public bool IsEmpty => CurValue == 0f;

        public void Init(float startValue)
        {
            CurValue = startValue;
        }

        public void Change(float amount)
        {
            CurValue += amount;
        }
        
        public float GetPercentage() => CurValue / maxValue;
    }
}