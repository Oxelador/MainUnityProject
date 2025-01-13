using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseItemData : MonoBehaviour
{
    public Image ItemSprite;
    public TextMeshProUGUI ItemCount;

    public void Awake()
    {
        ItemSprite.color = Color.clear;
        ItemCount.text = "";
    }
}
