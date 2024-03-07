using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace m8t
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
    public class Weapon : ScriptableObject
    {
        public GameObject projectile;
        public int roundsInClip;
        public int fireRatePerSecond;
    }
}

