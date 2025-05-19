using Entity.Player;
using ScriptableObjects;
using UnityEngine;

namespace Items
{
    public interface IInteractable
    {
        public string GetInteractPrompt();
        public void OnInteract();
    }
    
    
    public class ItemObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemData data;


        public string GetInteractPrompt()
        {
            string str = $"{data.DisplayName}\n{data.Description}";
            return str;
        }

        public void OnInteract()
        {
            CharacterManager.Instance.Player.itemData = data;
            CharacterManager.Instance.Player.addItem?.Invoke();
            Destroy(gameObject);
        }
    }
}
