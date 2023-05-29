using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace PFX
{
    public class BaseItemData : MonoBehaviour
    {
        public string itemName;
        public Sprite icon;
        public ItemType type;
        public bool stackable;
    }

    public enum ItemType
    {
        Weapon,
        Tool,
        Consumable,
        Resource,
    }
}
