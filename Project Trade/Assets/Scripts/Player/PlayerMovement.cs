using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFX
{
    public class PlayerMovement : MonoBehaviour
    {
        private CharacterController controller;
        private float baseSize;
        private float jumpSize;
        private Transform cam;

        private float maxSpd;
        private float spd;

        private float turnSmoothTime = 0.1f;
        private float turnSmoothVelocity;

        private float animVelocity;
        private float maxAnimVelocity;
        private Animator anim;
        private float animAccel;
        private float animDecel;

        private bool isSprinting;
        private bool isSneaking;

        private bool isGrounded;
        private Vector3 velocity;
        private int jumps;
        private int maxJumps;


        private void Start()
        {
            controller = GetComponent<CharacterController>();
            cam = Camera.main.transform;
        }

        public void SetUp(Animator anim, float animAccel, float animDecel, float baseSize, float jumpSize)
        {
            this.anim = anim;
            this.animAccel = animAccel;
            this.animDecel = animDecel;
            this.baseSize = baseSize;
            this.jumpSize = jumpSize;
        }

        public void MovementHandler(float walkSpd, float sprintSpd, float sneakSpd, float gravity, float acceleration, float deceleration)
        {
            SpeedManager();
            float horizontal = InputManager.I.Move().x;
            float vertical = InputManager.I.Move().y;
            Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

            if (isSneaking)
            {
                maxSpd = sneakSpd;
            }
            else if (isSprinting)
            {
                maxSpd = sprintSpd;
                maxAnimVelocity = 1;
                anim.SetBool("isSprinting", true);
            }
            else
            {
                maxAnimVelocity = .5f;
                maxSpd = walkSpd;
                anim.SetBool("isSprinting", false);
            }



            if (direction.magnitude >= 0.1f)
            {

                if (spd < maxSpd)
                    spd += Time.deltaTime * acceleration;
                else if (spd > maxSpd)
                    spd -= Time.deltaTime * deceleration;
                    

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0, angle, 0);

                Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                controller.Move(moveDir.normalized * spd * Time.deltaTime);

                if (animVelocity < maxAnimVelocity)
                    animVelocity += Time.deltaTime * animAccel;
                else if (animVelocity > maxAnimVelocity)
                    animVelocity -= Time.deltaTime * animDecel;
            }
            else
            {

                if (spd > 0)
                    spd -= Time.deltaTime * deceleration;
                else if (spd < 0)
                    spd = 0;

                if (animVelocity > 0)
                    animVelocity -= Time.deltaTime * animDecel;
                else if (animVelocity < 0)
                    animVelocity = 0;
            }

            anim.SetFloat("Velocity", animVelocity);
        }

        private void SpeedManager()
        {
            if(InputManager.I.Crouch())
            {
                isSneaking = true;
            }
            else if(InputManager.I.Sprint())
            {
                isSprinting = true;
            }
            else
            {
                isSprinting = false;
                isSneaking = false;
            }
        }

        public void GroundCheck(Transform groundCheck, float groundDis, LayerMask groundMask)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDis, groundMask);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
                jumps = maxJumps;
                anim.SetBool("inAir", false);
                anim.SetBool("isGrounded", true);
                controller.height = baseSize;
            }
            else
            {
                anim.SetBool("inAir", true);
                anim.SetBool("isJumping", false);
                controller.height = jumpSize;
            }
        }

        public void ApplyGravity(float gravity)
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        public void JumpHandler(float jumpHeight, float gravity, int maxJumps = 1)
        {
            this.maxJumps = maxJumps;
            if (InputManager.I.Jump())
            {
                if (jumps >= 1)
                {
                    jumps -= 1;
                    anim.SetBool("isGrounded", false);
                    anim.SetBool("isJumping", true);
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }

            }
        }
    }
}
