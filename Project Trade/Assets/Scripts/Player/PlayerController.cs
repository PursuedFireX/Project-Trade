using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace PFX
{
    public class PlayerController : MonoBehaviour
    {
        [FoldoutGroup("Character Controller Settings")]
        [SerializeField, FoldoutGroup("Character Controller Settings")]
        private float baseSize = 2.86f;
        [SerializeField, FoldoutGroup("Character Controller Settings")]
        private float jumpSize = 2.23f;
        [SerializeField, FoldoutGroup("Character Controller Settings")]
        private float crouchSize = 2.86f;

        [FoldoutGroup("Movement Settings")]
        [Title("Speed Options")]
        [SerializeField, FoldoutGroup("Movement Settings")]
        private float walkSpeed;
        [SerializeField, FoldoutGroup("Movement Settings")]
        private float sprintSpeed;
        [SerializeField, FoldoutGroup("Movement Settings")]
        private float sneakSpeed;
        [SerializeField, FoldoutGroup("Movement Settings")]
        private float acceleration = 0.1f;
        [SerializeField, FoldoutGroup("Movement Settings")]
        private float deceleration = 0.5f;

        [Title("Jump Options")]
        [SerializeField, FoldoutGroup("Movement Settings")]
        private float jumpHeight;
        [SerializeField, FoldoutGroup("Movement Settings")]
        private int maxJumps;

        [Title("Gravity/Ground Check")]
        [SerializeField, FoldoutGroup("Movement Settings")]
        private float gravity = -19.62f;
        [SerializeField, FoldoutGroup("Movement Settings")]
        private float groundDistance = 0.2f;
        [SerializeField, FoldoutGroup("Movement Settings")]
        private Transform groundCheck;
        [SerializeField, FoldoutGroup("Movement Settings")]
        private LayerMask checkMask;

        [FoldoutGroup("Animation Settings")]
        [SerializeField, FoldoutGroup("Animation Settings")]
        private Animator anim;
        [SerializeField, FoldoutGroup("Animation Settings")]
        private float animAcceleration = 0.1f;
        [SerializeField, FoldoutGroup("Animation Settings")]
        private float animDeceleration = 0.5f;
        [SerializeField, FoldoutGroup("Animation Settings")]
        private float animationPlayTime = 0.8f;

        [FoldoutGroup("Combat Settings")]
        [SerializeField, FoldoutGroup("Combat Settings")]
        private float comboCooldown = 2;

        [HideInInspector] public bool canPickup = false;

        [HideInInspector] public GameObject heldItem;
        public Transform holdPosition;
        [HideInInspector] public int actionTypeIndex;
        [HideInInspector] public bool hasItem;

        private PlayerMovement movement;
        private PlayerCombat combat;
        [HideInInspector] public WorldItem pickupItem;

        private void Awake()
        {
            movement = GetComponent<PlayerMovement>();
            combat = GetComponent<PlayerCombat>();
            movement.SetUp(anim, animAcceleration, animDeceleration, baseSize, jumpSize);
            combat.Initialize(anim, comboCooldown, animationPlayTime);
        }
        

        private void Update()
        {
            movement.ApplyGravity(gravity);
            movement.GroundCheck(groundCheck, groundDistance, checkMask);
            

            if (!UIManager.I.inUI)
            {
                movement.MovementHandler(walkSpeed, sprintSpeed, sneakSpeed, gravity, acceleration, deceleration);
                movement.JumpHandler(jumpHeight, gravity, maxJumps);
                combat.CombatHandler(hasItem, actionTypeIndex);

            }

            if(canPickup && pickupItem != null)
            {
                if(InputManager.I.PickUp())
                {
                    pickupItem.PickUp();
                    canPickup = false;
                }
            }
        }

        public void UpdateHeldItem()
        {
            if(InventoryManager.I.selectedSlot.itemInSlot != null)
            {
                InventoryItem itemInSlot = InventoryManager.I.selectedSlot.itemInSlot;
                if(itemInSlot.item.showInHand)
                {
                    if(heldItem != null)
                    {
                        Destroy(heldItem);
                    }
                    GameObject go = Instantiate(itemInSlot.item.itemPrefab, holdPosition);
                    go.transform.localPosition = Vector3.zero;
                    go.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -180));
                    go.transform.localScale = new Vector3(.01f, .01f, .01f);
                    heldItem = go;
                    hasItem = true;
                    anim.SetBool("hasItem", true);
                    anim.SetLayerWeight(1, 1);
                }
            }
            else if(InventoryManager.I.selectedSlot.itemInSlot == null)
            {
                Destroy(heldItem);
                anim.SetBool("hasItem", false);
                anim.SetLayerWeight(1, 0);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            canPickup = true;
            pickupItem = other.GetComponent<WorldItem>();

        }

        private void OnTriggerExit(Collider other)
        {
            canPickup = false;
            pickupItem = null;
        }
    }
}
