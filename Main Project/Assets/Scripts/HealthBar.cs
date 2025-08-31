using UnityEngine;
using UnityEngine.UI;

namespace oxi
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthBarFilling;
        [SerializeField] private Health _health;


        void Awake()
        {
            _health.UpdateHealth += OnHealthUpdate;
        }

        void OnDestroy()
        {
            _health.UpdateHealth -= OnHealthUpdate;
        }

        private void OnHealthUpdate(float value)
        {
            _healthBarFilling.fillAmount = value;
        }
    }
}
