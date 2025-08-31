using oxi;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Place it on a button or UI element that should trigger the cooldown
public class CooldownUI : MonoBehaviour
{
    private Image _cooldownImage;
    private TextMeshProUGUI _cooldownText;
    private float _cooldown;
    private float _cooldownTimer = 0f;
    private bool _isOnCooldown = false;

    private void Start()
    {
        _cooldownImage = GameObject.Find("Cooldown_Fill_Image").GetComponent<Image>();
        _cooldownText = GameObject.Find("Cooldown_Text").GetComponent<TextMeshProUGUI>();
        //_cooldown = FindObjectOfType<PlayerLocomotion>().DashCooldown;
    }

    public void TriggerCooldown()
    {
        _cooldownTimer = _cooldown;
        _isOnCooldown = true;
    }

    private void Update()
    {
        if (_isOnCooldown)
        {
            _cooldownTimer -= Time.deltaTime;
            float percent = Mathf.Clamp01(_cooldownTimer / _cooldown);

            _cooldownImage.fillAmount = percent;
            _cooldownText.text = Mathf.Ceil(_cooldownTimer).ToString();

            if(_cooldownTimer <= 0f)
            {
                _isOnCooldown = false;
                _cooldownImage.fillAmount = 0f;
                _cooldownText.text = "";
            }
        }
    }
}
