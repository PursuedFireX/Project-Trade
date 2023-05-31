using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace PFX
{
    public class InventoryItem : MonoBehaviour, IPointerClickHandler
    {

        public Image image;
        public TMP_Text countText;

        [HideInInspector] public bool pickedUp = false;
        [HideInInspector] public BaseItemData item;
        [HideInInspector] public int count = 1;
        [HideInInspector] public Transform parentAfterDrag;


        private void Update()
        {
            

            if (pickedUp)
            {
                transform.position = Input.mousePosition;
            }
        }

        public void InitializeItem(BaseItemData newItem)
        {
            item = newItem;
            image.sprite = newItem.icon;
            UpdateCount();

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!pickedUp)
            {
                PickupHandler(eventData);
            }
        }

        public void UpdateCount()
        {
            countText.text = count.ToString();
            bool textActive = count > 1;
            countText.gameObject.SetActive(textActive);
        }

        private void PickupHandler(PointerEventData eventData)
        {
            image.raycastTarget = false;
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            pickedUp = true;
            InventoryManager.I.pickedUpItem = this;
        }

        private void PlaceHandler(PointerEventData eventData)
        {
            
        }

        public void PlaceItem()
        {
            image.raycastTarget = true;
            transform.parent = parentAfterDrag;
            pickedUp = false;
        }
    }
}
