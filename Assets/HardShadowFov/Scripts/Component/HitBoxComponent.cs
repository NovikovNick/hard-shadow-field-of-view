using UnityEngine;
using UnityEngine.Assertions;

namespace m8t
{
    public class HitBoxComponent : MonoBehaviour
    {
        [Header("General settings")]
        [SerializeField] private HealthComponent healthComponent;

        public void ApplyDamage(int amount)
        {
            healthComponent.ApplyHitPoints(-amount);
        }

        private void Start()
        {
            Assert.IsNotNull(healthComponent);
        }
    }
}

