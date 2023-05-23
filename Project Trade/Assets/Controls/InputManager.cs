using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFX
{
    public class InputManager : MonoBehaviour
    {
        private InputController inputs;
        public static InputManager _instance;
        public static InputManager I
        {
            get
            {
                return _instance;
            }
        }

        private void Awake()
        {
            inputs = new InputController();
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        private void OnEnable()
        {
            inputs.Player.Enable();
        }

        private void OnDisable()
        {
            inputs.Player.Disable();
        }


        public Vector2 Move()
        {
            return inputs.Player.Move.ReadValue<Vector2>();
        }

        public Vector2 Look()
        {
            return inputs.Player.Look.ReadValue<Vector2>();
        }

        public float Scroll()
        {
            return inputs.Player.Scroll.ReadValue<Vector2>().y;
        }

        public bool Jump()
        {
            return inputs.Player.Jump.triggered;
        }

        public bool Sprint()
        {
            return inputs.Player.Sprint.IsPressed();
        }

        public bool Crouch(bool hold = false, bool checkReleased = false)
        {
            if (hold)
                return inputs.Player.Crouch.IsPressed();
            else if (checkReleased)
                return inputs.Player.Crouch.WasReleasedThisFrame();
            else
                return inputs.Player.Crouch.triggered;
        }

        public bool Fire1(bool hold = false)
        {
            if (hold)
            {
                return inputs.Player.Fire1.IsPressed();
            }
            else
                return inputs.Player.Fire1.triggered;
        }

        public bool Fire2(bool hold = false)
        {
            if (hold)
            {
                return inputs.Player.Fire2.IsPressed();
            }
            else
                return inputs.Player.Fire2.triggered;
        }

        public bool Fire3()
        {
            return inputs.Player.Fire3.triggered;
        }

        public bool Drop()
        {
            return inputs.Player.Drop.triggered;
        }    

        public bool ToggleInvntory()
        {
            return inputs.Player.ToggleInventory.triggered;
        }

    }
}
