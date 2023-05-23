using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFX
{
    public class PlayerMovement : MonoBehaviour
    {
        private CharacterController controller;
        private Transform cam;

        private float spd;

        private float turnSmoothTime = 0.1f;
        private float turnSmoothVelocity;

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

        public void MovementHandler(float walkSpd, float sprintSpd, float sneakSpd, float gravity)
        {
            SpeedManager();
            float horizontal = InputManager.I.Move().x;
            float vertical = InputManager.I.Move().y;
            Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

            if (isSneaking)
                spd = sneakSpd;
            else if (isSprinting)
                spd = sprintSpd;
            else
                spd = walkSpd;



            if (direction.magnitude >= 0.1f)
            {

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0, angle, 0);

                Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                controller.Move(moveDir.normalized * spd * Time.deltaTime);
            }
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
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }

            }
        }
    }
}
