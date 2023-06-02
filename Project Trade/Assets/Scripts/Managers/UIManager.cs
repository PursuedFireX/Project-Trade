using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace PFX
{
    public class UIManager : MonoBehaviour
    {
        #region Instance Manager
        public static UIManager _instance;
        public static UIManager I
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        [HideInInspector] public bool inUI;
        public CinemachineFreeLook cam;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        private void Update()
        {
            if (!inUI)
            {
                cam.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                cam.enabled = false;
                Cursor.lockState = CursorLockMode.None;
            }

            

        }

        public void ToggleInUI()
        {
            inUI = !inUI;
        }

        public void ToggleInUI(bool inUI)
        {
            this.inUI = inUI;
        }
    }
}
