using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace PFX
{
    public class InventorySlot : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image image;
        [SerializeField] private Color baseColor;
        [SerializeField] private Color selectedColor;
        [HideInInspector] public InventoryItem itemInSlot;

        private void Awake()
        {
            Deselect();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (InventoryManager.I.pickedUpItem != null)
            {
                DropHandler(eventData);
            }
        }

        public void Select()
        {
            image.color = selectedColor;
        }

        public void Deselect()
        {
            image.color = baseColor;
        }

        private void DropHandler(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                InventoryItem item = InventoryManager.I.pickedUpItem;
                if (transform.childCount == 0)
                {
                    item.parentAfterDrag = transform;
                    item.PlaceItem();
                    InventoryManager.I.pickedUpItem = null;
                }
            }
            else if (eventData.button == PointerEventData.InputButton.Middle)
            {
                //Middle click
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                //Right click option
            }
        }

    }
}
