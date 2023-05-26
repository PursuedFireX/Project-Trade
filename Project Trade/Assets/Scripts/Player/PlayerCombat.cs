using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFX
{
    public class PlayerCombat : MonoBehaviour
    {
        private Animator anim;
        private float cooldown;
        private float nextTimeToFire = 0;
        public static int numberOfClicks;
        private float lastTimeClicked = 0;
        private float maxComboDelay = 1;

        public void Initialize(Animator anim, float cooldown)
        {
            this.anim = anim;
            this.cooldown = cooldown;
            numberOfClicks = 0;
        }

        public void CombatHandler(bool hasSword)
        {
            ResetAnimationBools();

            if(Time.time > nextTimeToFire)
            {
                if(InputManager.I.Fire1())
                {
                    Fire();
                }
            }

            if (hasSword)
            {
                anim.SetBool("hasSword", true);
                anim.SetLayerWeight(1, 1);
            }
            else
            {
                anim.SetBool("hasSword", false);
                anim.SetLayerWeight(1, 0);
            }
        }


        private void Fire()
        {
            
            lastTimeClicked = Time.time;
            numberOfClicks++;

            if (numberOfClicks == 1)
            {
                anim.SetBool("Attack1", true);
            }
            numberOfClicks = Mathf.Clamp(numberOfClicks, 0, 2);

            if (numberOfClicks >= 1 && anim.GetCurrentAnimatorStateInfo(2).normalizedTime > .5f && anim.GetCurrentAnimatorStateInfo(2).IsName("Attack1"))
            {
                anim.SetBool("Attack2", true);
                anim.SetBool("Attack1", false);
                numberOfClicks = 0;
            }

            Debug.Log(numberOfClicks);
            anim.SetLayerWeight(2, 1);
        }

        private void ResetAnimationBools()
        {
            if (anim.GetCurrentAnimatorStateInfo(2).normalizedTime > .9f && anim.GetCurrentAnimatorStateInfo(2).IsName("Attack1"))
            {
                anim.SetBool("Attack1", false);
            }
            if (anim.GetCurrentAnimatorStateInfo(2).normalizedTime > .9f && anim.GetCurrentAnimatorStateInfo(2).IsName("Attack2"))
            {
                anim.SetBool("Attack2", false);
                anim.SetLayerWeight(2, 0);
                numberOfClicks = 0;
            }

            if(Time.time - lastTimeClicked > maxComboDelay)
            {
                numberOfClicks = 0;
            }
        }
    }
}
