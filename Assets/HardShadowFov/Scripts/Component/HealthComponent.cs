using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace m8t
{
    public class HealthComponent : MonoBehaviour
    {
        [Header("General settings")]
        [SerializeField] private int maxHealth = 100;
        public UnityEvent<float> OnHealthChanged;

        [Header("State")]
        [SerializeField] private int currentHealth;

        void Awake()
        {
            currentHealth = maxHealth;
        }

        public float HealthPercent { get { return currentHealth == 0 ? 0 : (float)currentHealth / maxHealth; } }

        public bool IsDead { get { return currentHealth == 0; } }

        public void ApplyHitPoints(int amount)
        {
            int oldHealth = currentHealth;
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            if (currentHealth != oldHealth)
            {
                OnHealthChanged.Invoke(HealthPercent);
            }
        }
    }
}

