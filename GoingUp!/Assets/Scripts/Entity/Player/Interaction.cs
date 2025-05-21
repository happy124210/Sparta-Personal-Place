using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity.Player
{
    public interface IInteractable
    {
        public string GetInteractPrompt();
        public void OnInteract();
    }
    
    public class Interaction : MonoBehaviour
    {
        [SerializeField] private float checkRate;
        [SerializeField] private float maxCheckDistance;
        [SerializeField] private LayerMask layerMask;
        
        [SerializeField] private TextMeshProUGUI promptText;
        
        [SerializeField] private GameObject curInteractGameObject;
        private IInteractable curInteractable;
        private float lastCheckTime;
        

        private void Update()
        {
            if (Time.time - lastCheckTime <= checkRate) return;
            
            lastCheckTime = Time.time;
            
            Vector3 rayOrigin = transform.position + Vector3.up * 0.5f;
            Vector3 rayDir = transform.forward;

            if (Physics.Raycast(rayOrigin, rayDir, out RaycastHit hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject == curInteractGameObject) return;

                curInteractGameObject = hit.collider.gameObject;
                curInteractable = curInteractGameObject.GetComponent<IInteractable>();

                if (curInteractable != null)
                    SetPromptText();
                else
                    Clear();
            }
            else
            {
                Clear();
            }
        }


        private void SetPromptText()
        {
            promptText.gameObject.SetActive(true);
            promptText.text = curInteractable.GetInteractPrompt();
        }
        
        
        private void Clear()
        {
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
        
        
        public void OnInteractInput(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Started || curInteractable == null) return;
            
            curInteractable.OnInteract();
            Clear();
        }
    }
}
