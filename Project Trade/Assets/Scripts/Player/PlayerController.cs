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

        public Transform holdPosition;

        public bool hasSword;
        public bool hasTool;

        private PlayerMovement movement;
        private PlayerCombat combat;
        private WorldItem pickupItem;
        private bool canPickup = false;

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
            movement.MovementHandler(walkSpeed, sprintSpeed, sneakSpeed, gravity, acceleration, deceleration);
            movement.JumpHandler(jumpHeight, gravity, maxJumps);
            combat.CombatHandler(hasSword, hasTool);

            if(canPickup && pickupItem != null)
            {
                if(InputManager.I.ToggleInvntory())
                {
                    pickupItem.PickUp(holdPosition);
                }
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
