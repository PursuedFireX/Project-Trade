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
        [SerializeField] private Sprite baseSprite;
        [SerializeField] private Sprite selectedSprite;
        [HideInInspector] public InventoryItem itemInSlot;

        private Animator anim;

        private void Awake()
        {
            anim = GetComponent<Animator>();
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
            anim.SetBool("selected", true);
            image.sprite = selectedSprite;
        }

        public void Deselect()
        {
            anim.SetBool("selected", false);
            image.sprite = baseSprite;
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
