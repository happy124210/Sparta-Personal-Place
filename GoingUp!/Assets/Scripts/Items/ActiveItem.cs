using UnityEngine;

namespace Items
{
    public enum ActiveType
    {
        Speed,
        JumpPower,
        Stamina,
        Heart
    }
    
    public class ActiveItem : MonoBehaviour
    {
        
        [SerializeField] private ActiveType activeType;
        [SerializeField] private float value;
        [SerializeField] private float duration;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            Debug.Log($"{activeType} +{value} for {duration}s");
            // TODO: 효과 적용
            Destroy(gameObject);
        }
    }
}
