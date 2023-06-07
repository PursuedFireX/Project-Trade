using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFX
{
    public class CameraTarget : MonoBehaviour
    {
        [HideInInspector] public Transform target;
        private Transform player;

        private void Start()
        {
            player = GameManager.I.player.transform;
            target = player;
        }

        private void Update()
        {
            transform.position = target.position;
        }

    }
}
