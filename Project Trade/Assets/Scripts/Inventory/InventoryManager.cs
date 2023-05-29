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
        private bool showInventory = false;

         public InventoryItem pickedUpItem;


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

        private void Update()
        {
            if(InputManager.I.ToggleInvntory())
            {
                showInventory = !showInventory;
                mainInventoryGroup.SetActive(showInventory);
            }
        }
    }
}
