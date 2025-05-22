using UnityEngine;

namespace Objects
{
    public class JumpPad : MonoBehaviour
    {
        [SerializeField] private float bouncePower;
        
        private ForceMode forceMode = ForceMode.Impulse;

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log($"JumpPad 충돌 감지: {other.gameObject.name}");
            
            if (!other.gameObject.CompareTag("Player")) return;
            
            Rigidbody rb = other.rigidbody;
            if (rb == null) return;
            
            Debug.Log("Rigidbody 감지됨 → 점프 힘 가함!");
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * bouncePower, forceMode);
        }
    }
}
