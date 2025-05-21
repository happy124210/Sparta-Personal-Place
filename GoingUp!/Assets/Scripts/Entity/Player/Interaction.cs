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
        [SerializeField] private float checkRate = 0.05f;
        [SerializeField] private float maxCheckDistance;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private GameObject curInteractGameObject;
        
        [SerializeField] private TextMeshProUGUI promptText;
        [SerializeField] private Camera cam;
        
        private IInteractable curInteractable;
        private float lastCheckTime;
        
        
        private void Start()
        {
            cam = Camera.main;
        }


        private void Update()
        {
            if (Time.time - lastCheckTime <= checkRate) return;
            
            lastCheckTime = Time.time;
            
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject == curInteractGameObject) return;
                curInteractGameObject = hit.collider.gameObject;
                curInteractable = curInteractGameObject.GetComponent<IInteractable>();
                
                SetPromptText();
            }
            else 
            {
                curInteractGameObject= null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }


        private void SetPromptText()
        {
            promptText.gameObject.SetActive(true);
            promptText.text = curInteractable.GetInteractPrompt();
        }
        
        
        public void OnInteractInput(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Started || curInteractable == null) return;
            
            curInteractable.OnInteract();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
