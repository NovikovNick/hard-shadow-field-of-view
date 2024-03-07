using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace m8t
{
    public class HealthBarUI : MonoBehaviour
    {
        [Header("General settings")]
        [SerializeField] private Image fill;
        [SerializeField] private Gradient gradient;

        private void Start()
        {
            Assert.IsNotNull(fill);
            Assert.IsNotNull(gradient);

            fill.color = gradient.Evaluate(fill.fillAmount);
        }

        public void OnHealthChanged(float amount)
        {
            fill.fillAmount = amount;
            fill.color = gradient.Evaluate(amount);
        }
    }
}
