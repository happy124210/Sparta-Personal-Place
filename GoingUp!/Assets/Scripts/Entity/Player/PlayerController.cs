using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity.Player
{
    public class PlayerController : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("Speed");

        [Header("Movement")] 
        [SerializeField] private float moveSpeed;
        private Vector2 curMovementInput;
    
        private Rigidbody _rigidBody;
        private CapsuleCollider _collider;
        private Animator _animator;
        private Camera cam;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            if (_rigidBody == null) Debug.LogError("PlayerController not found");
            
            _collider = GetComponent<CapsuleCollider>();
            if (_collider == null) Debug.LogError("CapsuleCollider not found");
            
            _animator = GetComponentInChildren<Animator>();
            if (_animator == null) Debug.LogError("Animator not found");
        }
    
    
        private void Start()
        {
            cam = Camera.main;
            Cursor.lockState = CursorLockMode.Locked;
        }
    
    
        private void FixedUpdate()
        {
            Move();
        }
    
        // 외부 자료 참고한 이동 함수 - 다시 공부하기
        private void Move()
        {
            // 카메라 기준 방향
            Vector3 camForward = cam.transform.forward;
            Vector3 camRight = cam.transform.right;
            camForward.y = camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            // 입력 기반 방향 벡터
            Vector3 moveDir = camForward * curMovementInput.y + camRight * curMovementInput.x;
            moveDir.Normalize();

            // 이동
            Vector3 velocity = moveDir * moveSpeed;
            velocity.y = _rigidBody.velocity.y;
            _rigidBody.velocity = velocity;

            // 카메라 회전
            if (moveDir.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
            }

            _animator.SetFloat(Speed, moveDir.magnitude, 0.1f, Time.deltaTime);
        }
    
    
        public void OnMove(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    curMovementInput = context.ReadValue<Vector2>();
                    return;

                case InputActionPhase.Canceled:
                    curMovementInput = Vector2.zero;
                    return;
            }
        }
    }
}
