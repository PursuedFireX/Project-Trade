using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PFX
{
    [CreateAssetMenu(menuName = "Data/Weapon Data")]
    public class WeaponData : BaseItemData
    {
        public bool ranged;
        public Vector2 damageMinMax;
        public float range;

        public AnimatorOverrideController[] attacks;
        public float[] animSpeeds;

    }
}
