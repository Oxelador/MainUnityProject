using TMPro;
using UnityEngine;

public class FloatingCombatText : MonoBehaviour
{
    private GameObject _floatingTextPrefab;
    private Health _health;
    private Camera _camera;
    private Vector3 offset = new Vector3(0, 0.5f, 0);

    private void Start()
    {
        _health = GetComponentInParent<Health>(); // Assuming the health bar is a child of the target object
        _camera = Camera.main;
        _floatingTextPrefab = Resources.Load<GameObject>("UI/FloatingText");
        _health.OnDamageTaken += ShowFloatingText; // Subscribe to health updates
    }

    private void OnDisable()
    {
        if (_health != null)
        {
            _health.OnDamageTaken -= ShowFloatingText; // Unsubscribe to avoid memory leaks
        }
    }

    public void ShowFloatingText(float value)
    {
        var text = Instantiate(_floatingTextPrefab,
            transform.position + offset,
            _camera.transform.rotation,
            transform);
        text.GetComponent<TextMeshProUGUI>().text = value.ToString("F0"); // Format the value as needed
    }
}
