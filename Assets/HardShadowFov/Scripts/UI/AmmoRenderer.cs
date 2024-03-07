using UnityEngine;
using UnityEngine.UI;

namespace m8t
{
    public class AmmoRenderer : MonoBehaviour
    {
        [SerializeField] private Text bulletClipsLabel;

        public void OnAmmoDataChange(AmmoData ammo)
        {
            bulletClipsLabel.text = ammo.bullet + " \\ " + ammo.clips;
        }
    }
}
