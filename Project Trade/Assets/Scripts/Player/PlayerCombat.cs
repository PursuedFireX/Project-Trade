using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFX
{
    public class PlayerCombat : MonoBehaviour
    {
        private Animator anim;
        private float cooldown;
        private float maxComboDelay = 1;
        private bool playNext;
        private float animationPlayTime;

        private int clicks;
        private int combo;
        private float lastTimeClicked;

        public AnimatorOverrideController[] attacks;
        

        public void Initialize(Animator anim, float cooldown, float playTime)
        {
            this.anim = anim;
            this.cooldown = cooldown;
            this.animationPlayTime = playTime;
            playNext = false;
        }

        public void CombatHandler(bool hasSword, bool hasTool)
        {
            ResetAnimationBools();
            AnimationStateUpdate(hasSword, hasTool);


            if (InputManager.I.Fire1())
            {
                Fire(hasSword, hasTool);
            }
            

            if (playNext == true && anim.GetCurrentAnimatorStateInfo(2).normalizedTime > animationPlayTime)
            {
                anim.runtimeAnimatorController = attacks[combo];
                anim.Play("Attack", 2, 0);
                playNext = false;
                
            }
        }


        private void Fire(bool hasSword, bool hasTool)
        {
            anim.SetBool("hasSword", hasSword);
            anim.SetBool("isTool", hasTool);
            lastTimeClicked = Time.time;
            Debug.Log(combo);
            Debug.Log(clicks);

            if(clicks == 0)
            {
                anim.runtimeAnimatorController = attacks[0];
                anim.Play("Attack", 2, 0);
                clicks++;
            }
            else if (clicks > 0 && anim.GetCurrentAnimatorStateInfo(2).normalizedTime > .2f && anim.GetCurrentAnimatorStateInfo(2).IsName("Attack"))
            {
                playNext = true;
                if (combo < attacks.Length)
                    combo++;
                else
                    combo = 0;

                if (clicks < attacks.Length)
                    clicks++;
                else
                    clicks = 0;
            }
            

            anim.SetLayerWeight(2, 1);
        }

        private void ResetAnimationBools()
        {

            if (Time.time - lastTimeClicked > maxComboDelay)
            {
                combo = 0;
                clicks = 0;
            }
        }

        private void AnimationStateUpdate(bool hasSword, bool hasTool)
        {
            if (hasSword)
            {
                anim.SetBool("hasSword", true);
                anim.SetBool("isTool", false);
                anim.SetLayerWeight(1, 1);
            }
            else if (hasTool)
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
        }

    }
}
