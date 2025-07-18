using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    private Health _health;
    private Slider _slider;
    private Camera _camera;

    private void Start()
    {
        _health = GetComponentInParent<Health>(); // Assuming the health bar is a child of the target object
        _slider = GetComponent<Slider>();
        _camera = Camera.main;
        _health.UpdateHealth += UpdateHealthBar; // Subscribe to health updates
    }

    private void OnDisable()
    {
        if (_health != null)
        {
            _health.UpdateHealth -= UpdateHealthBar; // Unsubscribe to avoid memory leaks
        }
    }

    public void UpdateHealthBar(float value)
    {
        float currentHealthAsPercantage = (float)value / _health.MaxHealth;
        _slider.value = currentHealthAsPercantage;
    }

    private void Update()
    {
        transform.rotation = _camera.transform.rotation; // Keep the health bar facing the camera
    }
}
