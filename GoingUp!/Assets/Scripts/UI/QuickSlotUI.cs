using System;
using System.Linq;
using Entity.Player;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class QuickSlotUI : MonoBehaviour
    {
        [SerializeField] private QuickSlot[] slots;
        [SerializeField] private Transform slotPanel;
            
        private PlayerController controller;
        private PlayerStat stat;

        private void Start()
        {
            controller = CharacterManager.Instance.Player.controller;
            stat = CharacterManager.Instance.Player.stat;
            
            CharacterManager.Instance.Player.addItem += AddItem;
            slots = new QuickSlot[slotPanel.childCount];
            
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i] = slotPanel.GetChild(i).GetComponent<QuickSlot>();
                if (slots[i] == null) Debug.LogError($"Slot {i} not found");
                
                slots[i].Index = i+1;
                slots[i].IndexText.text = (i+1).ToString();
                slots[i].Clear();
            }
        }


        private void Update()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (!Input.GetKeyDown(KeyCode.Alpha1 + i)) continue;
                if (i >= slots.Length) continue;
                
                UseSlot(i);
            }
        }

        private void AddItem()
        {
            ItemData data = CharacterManager.Instance.Player.itemData;
            if (data == null) Debug.LogError("ItemData is null");
            
            QuickSlot emptySlot = GetEmptySlot();
            if (emptySlot == null)
            {
                Debug.Log("Empty slot is null");
                CharacterManager.Instance.Player.itemData = null;
                return;
            }
            
            //Debug.Log($"[New slot] {data.name} saved");
            emptySlot.Item = data;
            UpdateUI();
            //Debug.Log($"[New slot] {data.name} UI Updated");
            
            CharacterManager.Instance.Player.itemData = null;
        }
        
        private QuickSlot GetEmptySlot()
        {
            return slots.FirstOrDefault(slot => slot.Item == null);
        }
        
        
        private void UpdateUI()
        {
            foreach (QuickSlot slot in slots)
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


        private void UseSlot(int index)
        {
            slots[index].Use();
        }
    }
}
