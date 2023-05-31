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

        private AnimatorOverrideController[] attacks;
        private float[] animSpeeds;

        private void Update()
        {
            if(InventoryManager.I.selectedSlot.itemInSlot != null)
            {
                InventoryItem item = InventoryManager.I.selectedSlot.itemInSlot;
                if (GameManager.I.playerController.heldItem.GetComponent<WeaponBase>() != null)
                {
                    attacks = GameManager.I.playerController.heldItem.GetComponent<WeaponBase>().data.attacks;
                    animSpeeds = GameManager.I.playerController.heldItem.GetComponent<WeaponBase>().data.animSpeeds;
                }
            }
        }

        public void Initialize(Animator anim, float cooldown, float playTime)
        {
            this.anim = anim;
            this.cooldown = cooldown;
            this.animationPlayTime = playTime;
            playNext = false;
        }

        public void CombatHandler(bool hasItem, int actionType)
        {
            ResetAnimationBools();

            if (InputManager.I.Fire1())
            {
                if(InventoryManager.I.selectedSlot.itemInSlot != null)
                    Fire(actionType);
            }
            

            if (playNext == true && anim.GetCurrentAnimatorStateInfo(2).normalizedTime > animationPlayTime)
            {
                if (combo > attacks.Length - 2)
                    combo = attacks.Length - 1;

               
                anim.runtimeAnimatorController = attacks[combo];
                anim.speed = animSpeeds[combo];
                anim.Play("Attack", 2, 0);
                playNext = false;
                
            }
        }


        private void Fire(int actionType)
        {
            lastTimeClicked = Time.time;

            switch(InventoryManager.I.selectedSlot.itemInSlot.item.type)
            {
                case ItemType.Weapon:
                    if (clicks == 0 && !anim.GetCurrentAnimatorStateInfo(2).IsName("Attack"))
                    {
                        anim.runtimeAnimatorController = attacks[0];
                        anim.speed = animSpeeds[0];
                        anim.Play("Attack", 2, 0);
                        clicks++;
                    }
                    else if (clicks > 0 && anim.GetCurrentAnimatorStateInfo(2).normalizedTime > .2f && anim.GetCurrentAnimatorStateInfo(2).IsName("Attack"))
                    {
                        playNext = true;
                        if (combo < attacks.Length - 1)
                            combo++;
                        else
                            combo = 0;

                        if (clicks < attacks.Length)
                            clicks++;
                        else
                            clicks = 0;
                    }
                    break;

                case ItemType.Tool:
                    anim.Play("Mine", 2, 0);
                    break;
            }

            anim.SetLayerWeight(2, 1);
        }

        private void ResetAnimationBools()
        {
            if(anim.GetCurrentAnimatorStateInfo(2).normalizedTime > .9f)
            {
                clicks = 0;
                combo = 0;
            }

            if (Time.time - lastTimeClicked > maxComboDelay)
            {
                combo = 0;
                clicks = 0;
            }
        }

    }
}
