using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Damageable _damageable;
    [SerializeField] private Slider _slider;
    private void Awake()
    {
        if (_damageable == null)
            _damageable = GetComponentInParent<Damageable>();
        if (_slider == null)
            _slider = GetComponentInChildren<Slider>();

        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void OnEnable()
    {
        _slider.maxValue = _damageable.MaxHealth;
        _slider.value = _damageable.CurrentHealth;

        _damageable.OnHealthChanged += DisplayHealth;
    }

    private void OnDisable()
    {
        _damageable.OnHealthChanged -= DisplayHealth;
    }

    public void DisplayHealth(int newHealthValue)
    {
        _slider.value = newHealthValue;
    }
}
