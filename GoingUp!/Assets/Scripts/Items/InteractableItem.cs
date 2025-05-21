using Entity.Player;
using ScriptableObjects;
using UnityEngine;

namespace Items
{
    public class InteractableItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemData data;
        
        public string GetInteractPrompt()
        {
            string str = $"{data.DisplayName}\n{data.Description}";
            return str;
        }

        public void OnInteract()
        {
            Debug.Log("획득 " + gameObject.name);
            // TODO: 인벤토리 추가
            Destroy(gameObject);
        }
    }
}
