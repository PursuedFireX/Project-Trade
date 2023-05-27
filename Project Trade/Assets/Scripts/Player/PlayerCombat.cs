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
        private bool playNext;
        private float animationPlayTime;

        public void Initialize(Animator anim, float cooldown, float playTime)
        {
            this.anim = anim;
            this.cooldown = cooldown;
            this.animationPlayTime = playTime;
            numberOfClicks = 0;
            playNext = false;
        }

        public void CombatHandler(bool hasSword, bool hasTool)
        {
            ResetAnimationBools();

            if(InputManager.I.Fire1())
            {
                Fire(hasSword, hasTool);
            }

            if (hasSword)
            {
                anim.SetBool("hasSword", true);
                anim.SetBool("isTool", false);
                anim.SetLayerWeight(1, 1);
            }
            else if(hasTool)
            {
                anim.SetBool("hasSword", false);
                anim.SetBool("isTool", true);
                anim.SetLayerWeight(1, 1);
            }
            else
            {
                anim.SetBool("hasSword", false);
                anim.SetBool("isTool", false);
                anim.SetLayerWeight(1, 0);
            }

            if (playNext == true && anim.GetCurrentAnimatorStateInfo(2).normalizedTime > animationPlayTime)
            {
                anim.SetBool("Attack2", true);
                anim.SetBool("Attack1", false);
                playNext = false;
                numberOfClicks = 0;
            }
        }


        private void Fire(bool hasSword, bool hasTool)
        {
            if (hasSword)
            {
                anim.SetBool("hasSword", true);
                anim.SetBool("isTool", false);
                lastTimeClicked = Time.time;
                numberOfClicks++;

                if (numberOfClicks == 1)
                {
                    anim.SetBool("Attack1", true);
                }
                numberOfClicks = Mathf.Clamp(numberOfClicks, 0, 2);

                if (numberOfClicks >= 1 && anim.GetCurrentAnimatorStateInfo(2).normalizedTime > .2f && anim.GetCurrentAnimatorStateInfo(2).IsName("Attack1"))
                {
                    playNext = true;

                }

                if (numberOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(2).normalizedTime > .2f && anim.GetCurrentAnimatorStateInfo(2).IsName("Attack2"))
                {
                    playNext = true;

                }
            }
            else if(hasTool)
            {
                anim.SetBool("hasSword", false);
                anim.SetBool("isTool", true);
                anim.SetTrigger("mine");
            }

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
            if (anim.GetCurrentAnimatorStateInfo(2).normalizedTime > .9f && anim.GetCurrentAnimatorStateInfo(2).IsName("Attack3"))
            {
                anim.SetBool("Attack3", false);
                anim.SetLayerWeight(2, 0);
                numberOfClicks = 0;
            }

            if (Time.time - lastTimeClicked > maxComboDelay)
            {
                numberOfClicks = 0;
            }
        }
    }
}
