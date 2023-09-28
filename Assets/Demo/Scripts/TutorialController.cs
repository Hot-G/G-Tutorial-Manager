using UnityEngine;
using UnityEngine.UI;

namespace GTutorialManager.Demo
{
    public class TutorialController : MonoBehaviour
    {
        private const float MaxHealth = 100;
        private const float HealthIncreaseValue = 10;
        private float _currentHealth;

        [SerializeField] private Button decreaseHealthButton;
        [SerializeField] private Button increaseHealthButton;
        [SerializeField] private Slider healthSlider;
    
        private void Awake()
        {
            _currentHealth = MaxHealth;

            decreaseHealthButton.onClick.AddListener(OnDecreaseButtonClicked);
            increaseHealthButton.onClick.AddListener(OnIncreaseButtonClicked);
        }

        private void OnIncreaseButtonClicked()
        {
            _currentHealth = Mathf.Clamp(_currentHealth + HealthIncreaseValue, 0, MaxHealth);
            healthSlider.value = _currentHealth / MaxHealth;
        }
        
        private void OnDecreaseButtonClicked()
        {
            _currentHealth = Mathf.Clamp(_currentHealth - HealthIncreaseValue, 0, MaxHealth);
            healthSlider.value = _currentHealth / MaxHealth;
        }
    }
}

