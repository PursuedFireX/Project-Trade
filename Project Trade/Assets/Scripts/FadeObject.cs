using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFX
{
    public class FadeObject : MonoBehaviour
    {
        private Material mat;
        private Renderer renderer;

        private void Awake()
        {
            renderer = GetComponent<Renderer>();
            mat = renderer.material;
        }


        public void Fade()
        {
            renderer.material = AssetManager.I.fadeMat;
        }

        public void Restore()
        {
            renderer.material = mat;
        }    
    }
}
