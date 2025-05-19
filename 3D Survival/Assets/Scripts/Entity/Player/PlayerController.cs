using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")] [SerializeField] private float moveSpeed;
        [SerializeField] private LayerMask groundLayerMask;
        private Vector2 curMovementInput;

        [Header("Look")] [SerializeField] private Transform cameraContainer;
        [SerializeField] private float minXLook;
        [SerializeField] private float maxXLook;
        [SerializeField] private float jumpPower;
        [SerializeField] private float lookSensitivity;

        private float curCamRotX;
        private Vector2 mouseDelta;

        private Rigidbody _rigidBody;
        private CapsuleCollider _collider;


        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            if (_rigidBody == null) Debug.LogError("PlayerController not found");

            _collider = GetComponent<CapsuleCollider>();
            if (_collider == null) Debug.LogError("CapsuleCollider not found");
        }


        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }


        private void FixedUpdate()
        {
            Move();
        }


        private void LateUpdate()
        {
            CameraLook();
        }


        private void Move()
        {
            Vector3 dif = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
            dif *= moveSpeed;
            dif.y = _rigidBody.velocity.y; // 수직방향 이동은 제한

            _rigidBody.velocity = dif;
        }


        private void CameraLook()
        {
            curCamRotX += mouseDelta.y * lookSensitivity;
            curCamRotX = Mathf.Clamp(curCamRotX, minXLook, maxXLook);
            cameraContainer.localEulerAngles = new Vector3(-curCamRotX, 0, 0);

            transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
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


        public void OnLook(InputAction.CallbackContext context)
        {
            mouseDelta = context.ReadValue<Vector2>();
        }


        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started && IsGrounded())
                _rigidBody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }

        /// <summary>
        /// 플레이어가 땅에 있는지 검사한다.
        /// </summary>
        /// <returns>땅에 있으면 true, 아니면 false</returns>
        private bool IsGrounded()
        {
            Vector3 basePosition = transform.position + (transform.up * 0.01f);

            Ray[] rays = new Ray[4]
            {
                new Ray(basePosition + (transform.forward * 0.2f), Vector3.down),
                new Ray(basePosition + (-transform.forward * 0.2f), Vector3.down),
                new Ray(basePosition + (transform.right * 0.2f), Vector3.down),
                new Ray(basePosition + (-transform.right * 0.2f), Vector3.down),
            };

            foreach (Ray ray in rays)
            {
                //Debug.DrawRay(ray.origin, ray.direction * 0.3f, Color.red);

                if (Physics.Raycast(ray, 0.3f, groundLayerMask))
                {
                    //Debug.Log("Grounded");
                    return true;
                }
            }

            //Debug.Log("Not grounded");
            return false;
        }
    }
}