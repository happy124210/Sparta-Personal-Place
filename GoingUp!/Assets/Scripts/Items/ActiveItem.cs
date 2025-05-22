using Entity.Player;
using UnityEngine;

namespace Items
{
    public enum ActiveType
    {
        Speed,
        JumpPower
    }
    
    public class ActiveItem : MonoBehaviour
    {
        
        [SerializeField] private ActiveType activeType;
        public ActiveType ActiveType => activeType;
        [SerializeField] private float value;
        public float Value => value;
        [SerializeField] private float duration;
        public float Duration => duration;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            CharacterManager.Instance.Player.controller.ApplyActiveEffect(this);
            Debug.Log($"{activeType} +{value} for {duration}s");
            Destroy(gameObject);
        }
    }
}
