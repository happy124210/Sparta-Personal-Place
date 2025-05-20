using System;
using ScriptableObjects;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ItemSlot : MonoBehaviour
    {
        public InventoryUI inventoryUI;
        
        [SerializeField] private ItemData item;
        public ItemData Item
        {
            get => item;
            set => item = value;
        }
        [SerializeField] private int index;
        public int Index
        {
            get => index;
            set => index = value;
        }
        [SerializeField] private bool isEquipped;
        public bool IsEquipped
        {
            get => isEquipped;
            set => isEquipped = value;
        }
        [SerializeField] private int count;
        public int Count
        {
            get => count;
            set => count = value;
        }
        
        [SerializeField] private Button button;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private Outline outline;
        
        private void Start()
        {
            outline = GetComponent<Outline>();
            if (outline == null) Debug.LogError("outline not found");
        }


        private void OnEnable()
        {
            outline.enabled = IsEquipped;
        }
        

        public void Set()
        {
            icon.gameObject.SetActive(true);
            icon.sprite = item.Icon;
            countText.text = count > 1 ? count.ToString() : string.Empty;
            outline.enabled = IsEquipped;
        }


        public void Clear()
        {
            item = null;
            icon.gameObject.SetActive(false);
            countText.text = string.Empty;
        }


        public void OnClickButton()
        {
            inventoryUI.SelectItem(index);
        }
    }
}
