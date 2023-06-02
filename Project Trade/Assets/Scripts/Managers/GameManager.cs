using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace PFX
{
    public class GameManager : MonoBehaviour
    {
        #region Instance Manager
        public static GameManager _instance;
        public static GameManager I
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        [HideInInspector] public GameObject player;
        [HideInInspector] public PlayerController playerController;
        public BaseItemData test;
        public GameObject sword;

        public GameObject target;

        public bool lockedOn = false;

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
            player = GameObject.FindGameObjectWithTag("Player");
            playerController = player.GetComponent<PlayerController>();

        }

        private void Start()
        {
            GameObject go = Instantiate(AssetManager.I.worldItem);
            go.GetComponent<WorldItem>().Initialize(sword, test);
        }

        private void Update()
        {
            
        }
    }
}
