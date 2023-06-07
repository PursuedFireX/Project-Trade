using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFX
{
    public class CameraHandler : MonoBehaviour
    {
        private Transform player;
        RaycastHit hit;
        public LayerMask hitMask;
        private List<Transform> fadedObjects;

        private void Start()
        {
            player = GameManager.I.player.transform;
            fadedObjects = new List<Transform>();
        }

        private void Update()
        {
            if(Physics.Raycast(transform.position, transform.forward, out hit, Vector3.Distance(transform.position, player.position), hitMask))
            {
                Debug.DrawLine(transform.position, player.position);
                if (hit.transform.GetComponent<FadeObject>())
                {
                    if(!fadedObjects.Contains(hit.transform))
                        fadedObjects.Add(hit.transform);

                    hit.transform.GetComponent<FadeObject>().Fade();
                }
            }
            else
            {
                if(fadedObjects.Count > 0)
                {
                    for (int i = 0; i < fadedObjects.Count; i++)
                    {
                        fadedObjects[i].GetComponent<FadeObject>().Restore();
                        fadedObjects.RemoveAt(i);
                    }
                }
            }    

        }

    }
}
