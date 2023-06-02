using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFX
{
    public class WeaponBase : MonoBehaviour
    {
        public WeaponData data;
        [SerializeField] private Collider col;
        public Rigidbody rb;
        public float damage;

        private void OnTriggerEnter(Collider other)
        {
            if (GameManager.I.playerController.isAttacking)
            {
                if (other.GetComponent<IDamage>() != null)
                {
                    other.GetComponent<IDamage>().TakeDamage(damage);
                }
            }
        }


    }
}
