using UnityEngine;

namespace PFX
{
    public class AssetManager : MonoBehaviour
    {
        #region Instance Manager
        public static AssetManager _instance;
        public static AssetManager I
        {
            get
            {
                return _instance;
            }
        }
        #endregion


        #region Prefabs

        public GameObject inventoryItemPrefab;
        public GameObject worldItem;

        #endregion


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
    }
}
