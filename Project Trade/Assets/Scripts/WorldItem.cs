using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFX
{
    public class WorldItem : MonoBehaviour
    {
        private Collider triggerCol;
        private Collider col;
        private bool held;

        private void Awake()
        {
            triggerCol = GetComponent<Collider>();
            col = GetComponentInChildren<Collider>();
        }

        public void PickUp(Transform holder)
        {
            transform.SetParent(holder);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -180));
            triggerCol.enabled = false;
            col.enabled = false;
        }
    }
}
