using Entity.Player;
using UnityEngine;

namespace Items
{
    public class InteractableItem : MonoBehaviour, IInteractable
    {
        public string GetInteractPrompt()
        {
            Debug.Log("Interacting with " + gameObject.name);
            return gameObject.name;
        }

        public void OnInteract()
        {
            Debug.Log("획득 " + gameObject.name);
            // TODO: 인벤토리 추가
            Destroy(gameObject);
        }
    }
}
