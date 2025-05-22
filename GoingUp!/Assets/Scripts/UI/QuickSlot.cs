using Entity.Player;
using UnityEngine;
using UnityEngine.UI;
using ScriptableObjects;
using TMPro;

namespace UI
{
    public class QuickSlot : MonoBehaviour
    {
        [SerializeField] private int index;
        public int Index
        {
            get => index;
            set => index = value;
        }
        [SerializeField] private ItemData item;
        [SerializeField] private TextMeshProUGUI indexText;

        public TextMeshProUGUI IndexText
        {
            get => indexText;
            set => indexText = value;
        }
        
        public ItemData Item
        {
            get => item;
            set => item = value;
        }
        
        [SerializeField] private Image icon;
        
        public void Set()
        {
            icon.gameObject.SetActive(true);
            icon.sprite = item.Icon;
        }
        
        
        public void Clear()
        {
            item = null;
            icon.gameObject.SetActive(false);
        }


        public void Use()
        {
            if (item == null) return;

            switch (item.Type)
            {
                case ItemType.Passive:
                    Debug.Log($"Passive item: {item.DisplayName} 사용됨");
                    CharacterManager.Instance.Player.stat.ApplyEffect(item.PassiveEffects);
                    Clear();
                    break;
                
                case ItemType.Key:
                    Debug.Log($"Key item: {item.DisplayName} 사용 불가");
                    break;
            }
        }
    }
}