using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace PFX
{
    public class PlayerController : MonoBehaviour
    {

        [FoldoutGroup("Movement Settings")]

        [Title("Speed Options")]
        [SerializeField, FoldoutGroup("Movement Settings")]
        private float walkSpeed;
        [SerializeField, FoldoutGroup("Movement Settings")]
        private float sprintSpeed;
        [SerializeField, FoldoutGroup("Movement Settings")]
        private float sneakSpeed;

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

        [SerializeField] private Animator anim;

        private PlayerMovement movement;

        private void Awake()
        {
            movement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            movement.ApplyGravity(gravity);
            movement.GroundCheck(groundCheck, groundDistance, checkMask);
            movement.MovementHandler(walkSpeed, sprintSpeed, sneakSpeed, gravity);
            movement.JumpHandler(jumpHeight, gravity, maxJumps);
            
        }
    }
}
