using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFX
{
    public class InventoryManager : MonoBehaviour
    {
        #region Instance Manager
        public static InventoryManager _instance;
        public static InventoryManager I
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        [SerializeField] private GameObject mainInventoryGroup;
        [SerializeField] private GameObject hotbar;
        public int maxStack = 99;
        private bool showInventory = false;

        public InventorySlot[] slots;
        [HideInInspector] public InventorySlot selectedSlot;

        [HideInInspector] public InventoryItem pickedUpItem;

        public int hotBarSlots;

        private int selectedSlotIndex = -1;
        private int newSlot;


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        private void Start()
        {
            ChangeSelectedSlot(0);
            ToggleInventory(false);
            
        }

        private void Update()
        {
            if (InputManager.I.ToggleInvntory())
            {
                ToggleInventory();
            }

            ScrollHandler();

        }


        public bool AddItem(BaseItemData item)
        {

            for (int i = 0; i < slots.Length; i++)
            {
                InventorySlot slot = slots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot != null && itemInSlot.item == item && item.stackable && itemInSlot.count < maxStack)
                {
                    itemInSlot.count++;
                    return true;
                }

            }

            for (int i = 0; i < slots.Length; i++)
            {
                InventorySlot slot = slots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if(itemInSlot == null)
                {
                    SpawnItemInInventory(item, slot);
                    return true;
                }

            }

            return false;
        }

        public bool AddItem(BaseItemData item, int slotIndex)
        {
            InventorySlot slot = slots[slotIndex];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.item == item && item.stackable && itemInSlot.count < maxStack)
            {
                itemInSlot.count++;
                return true;
            }

            if (itemInSlot == null)
            {
                SpawnItemInInventory(item, slot);
                return true;
            }

            return false;
        }

        private void SpawnItemInInventory(BaseItemData item, InventorySlot slot)
        {
            GameObject newItemGo = Instantiate(AssetManager.I.inventoryItemPrefab, slot.transform);
            InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
            inventoryItem.InitializeItem(item);
            slot.itemInSlot = inventoryItem;
            GameManager.I.playerController.UpdateHeldItem();
        }

        private void ScrollHandler()
        {
            //Numpad selection
            if (Input.inputString != null)
            {
                bool isNumber = int.TryParse(Input.inputString, out int number);
                if (isNumber && number > 0 && number < hotBarSlots + 1)
                {
                    ChangeSelectedSlot(number - 1);
                }
            }

            //Scroll selection
            float scroll = InputManager.I.Scroll();
            if (scroll != 0)
            {

                if (scroll > 0)
                    newSlot--;
                else if (scroll < 0)
                    newSlot++;

                if (newSlot > hotBarSlots - 1)
                    newSlot = 0;
                if (newSlot < 0)
                    newSlot = hotBarSlots - 1;

                ChangeSelectedSlot(newSlot);
            }

            //Controller Selected
        }

        private void ChangeSelectedSlot(int newSlot) //Updates the current selected slot
        {
            if (selectedSlotIndex >= 0)
                slots[selectedSlotIndex].Deselect();

            slots[newSlot].Select();
            selectedSlotIndex = newSlot;
            selectedSlot = slots[selectedSlotIndex];
            GameManager.I.playerController.UpdateHeldItem();
        }

        public void ToggleInventory()
        {
            showInventory = !showInventory;
            mainInventoryGroup.SetActive(showInventory);
            UIManager.I.inUI = showInventory;
        }

        public void ToggleInventory(bool showInventory)
        {
            mainInventoryGroup.SetActive(showInventory);
            UIManager.I.inUI = showInventory;
        }

    }
}
