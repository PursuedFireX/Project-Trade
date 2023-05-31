using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFX
{
    public class WorldItem : MonoBehaviour
    {
        [HideInInspector] public GameObject prefab;
        [HideInInspector] public BaseItemData item;
        private Collider triggerCol;
        private bool held;

        private void Awake()
        {
            triggerCol = GetComponent<Collider>();
        }

        public void Initialize(GameObject prefab, BaseItemData itemData)
        {
            item = itemData;
            this.prefab = prefab;

            Debug.Log("Initialized " + item.itemName);
            GameObject newGo = Instantiate(prefab, transform);
            newGo.transform.localPosition = Vector3.zero;
            
        }

        public void PickUp()
        {
            if(InventoryManager.I.AddItem(item))
                Destroy(this.gameObject);

            /*transform.SetParent(holder);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -180));
            triggerCol.enabled = false;*/
        }
    }
}
