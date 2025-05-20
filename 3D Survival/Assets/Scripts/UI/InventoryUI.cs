using System.Linq;
using Entity.Player;
using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private ItemSlot[] slots;

        [SerializeField] private GameObject inventoryWindow;
        [SerializeField] private Transform slotPanel;
        [SerializeField] private Transform dropPosition;
        
        [Header("Selected Item")]
        [SerializeField] private TextMeshProUGUI selectedName;
        [SerializeField] private TextMeshProUGUI selectedDescription;
        [SerializeField] private TextMeshProUGUI selectedStatName;
        [SerializeField] private TextMeshProUGUI selectedStatValue;
        [SerializeField] private GameObject useButton;
        [SerializeField] private GameObject equipButton;
        [SerializeField] private GameObject unequipButton;
        [SerializeField] private GameObject dropButton;
        
        private ItemData selectedItem;
        private int selectedIndex;
        
        private PlayerController controller;
        private PlayerStat stat;

        private void Start()
        {
            controller = CharacterManager.Instance.Player.controller;
            stat = CharacterManager.Instance.Player.stat;
            dropPosition = CharacterManager.Instance.Player.dropPosition;

            controller.inventory += ToggleInventory;
            CharacterManager.Instance.Player.addItem += AddItem;
            
            inventoryWindow.SetActive(false);
            slots = new ItemSlot[slotPanel.childCount];

            for (int i = 0; i < slots.Length; i++)
            {
                slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
                if (slots[i] == null) Debug.LogError($"Slot {i} not found");
                
                slots[i].Index = i;
                slots[i].inventoryUI = this;
                slots[i].Clear();
            }

            ClearSelectedItemWindow();
        }
        
        
        private void ClearSelectedItemWindow()
        {
            selectedItem = null;

            selectedName.text = string.Empty;
            selectedDescription.text = string.Empty;
            selectedStatName.text = string.Empty;
            selectedStatValue.text = string.Empty;

            useButton.SetActive(false);
            equipButton.SetActive(false);
            unequipButton.SetActive(false);
            dropButton.SetActive(false);
        }


        private void ToggleInventory()
        {
            inventoryWindow.SetActive(!IsOpen());
        }
        
        
        private bool IsOpen()
        {
            return inventoryWindow.activeInHierarchy;
        }


        private void AddItem()
        {
            ItemData data = CharacterManager.Instance.Player.itemData;
            
            if (data == null) Debug.LogError("data is null");
            Debug.Log($"[Pickup] itemData = {data.name}");
            
            // 아이템이 중복 가능한지 canStack
            if (data.CanStack)
            {
                ItemSlot slot = GetItemStack(data);
                if (slot != null)
                {
                    Debug.Log($"[Stacked] {data.name} stacked");
                    slot.Count++;
                    UpdateUI();
                    CharacterManager.Instance.Player.itemData = null;
                    return;
                }
            }
            
            // 아니라면 비어있는 슬롯 가져옴
            ItemSlot emptySlot = GetEmptySlot();
            
            // 있다면
            if (emptySlot != null)
            {
                Debug.Log($"[New slot] {data.name} saved");
                emptySlot.Item = data;
                emptySlot.Count = 1;
                UpdateUI();
                Debug.Log($"[New slot] {data.name} UI Updated");
                CharacterManager.Instance.Player.itemData = null;
                return;
            }
            
            // 없다면
            ThrowItem(data);
            Debug.Log($"[Throw] {data.name} disposed");
            CharacterManager.Instance.Player.itemData = null;
        }


        private void UpdateUI()
        {
            foreach (ItemSlot slot in slots)
            {
                if (slot.Item != null)
                {
                    slot.Set(); 
                    Debug.Log("[UI Set] Item Updated");
                }
                else
                {
                    slot.Clear();
                    Debug.Log("[UI Set] Item Cleared");
                }
            }
        }
        
        
        private ItemSlot GetItemStack(ItemData data)
        {
            return slots.FirstOrDefault(slot => slot.Item == data && slot.Count < data.MaxStackAmount);
        }


        private ItemSlot GetEmptySlot()
        {
            return slots.FirstOrDefault(slot => slot.Item == null);
        }


        private void ThrowItem(ItemData data)
        {
            Instantiate(data.DropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
        }


        public void SelectItem(int index)
        {
            if (slots[index].Item == null) return;
            
            selectedItem = slots[index].Item;
            selectedIndex = index;
            
            selectedName.text = selectedItem.DisplayName;
            selectedDescription.text = selectedItem.Description;

            selectedStatName.text = string.Empty;
            selectedStatValue.text = string.Empty;

            foreach (var consumable in selectedItem.Consumables)
            {
                selectedStatName.text += consumable.Type.ToString() + "\n";
                selectedStatValue.text += consumable.Value.ToString() + "\n";
            }
            
            useButton.SetActive(selectedItem.Type == ItemType.Consumable);
            equipButton.SetActive(selectedItem.Type == ItemType.Equipable && !slots[index].IsEquipped);
            unequipButton.SetActive(selectedItem.Type == ItemType.Equipable && slots[index].IsEquipped);
            dropButton.SetActive(true);
        }
    }
}
