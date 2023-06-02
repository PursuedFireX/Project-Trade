using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFX
{
    public class EnemyBase : MonoBehaviour, IDamage
    {

        public float hp = 100;
        public void TakeDamage(float dmg)
        {
            hp -= dmg;
            if(hp <= 0)
            {
                GameManager.I.playerController.combat.DisableLockOn();
                Destroy(this.gameObject);
            }
        }
    }
}
