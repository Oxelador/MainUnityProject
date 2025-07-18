using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIEventHandler : MonoBehaviour
{
    private TextMeshProUGUI _goldAmount;
    public static UnityEvent<int> OnGoldDisplayUpdate = new UnityEvent<int>();

    private void Awake()
    {
        _goldAmount = GameObject.Find("Gold_Amount").GetComponent<TextMeshProUGUI>();
        OnGoldDisplayUpdate.AddListener(UpdateGoldDisplay);
    }

    private void UpdateGoldDisplay(int goldAmount)
    {
        _goldAmount.text = "Gold: " + goldAmount.ToString();
    }

    private void OnDisable()
    {
        OnGoldDisplayUpdate.RemoveListener(UpdateGoldDisplay);
    }
}
