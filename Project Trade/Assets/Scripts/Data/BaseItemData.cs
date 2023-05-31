using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace PFX
{
    [CreateAssetMenu(menuName = "Data/Base Item")]
    public class BaseItemData : ScriptableObject
    {
        public string itemName;
        public GameObject itemPrefab;
        public Sprite icon;
        public ItemType type;
        public bool stackable;
        public bool showInHand;

        public int value;
    }

    public enum ItemType
    {
        Weapon,
        Tool,
        Consumable,
        Resource,
    }
}
