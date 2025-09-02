using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace oxi
{
    public class InteractableUI : MonoBehaviour
    {
        [HideInInspector] public TextMeshProUGUI interactableText;

        private void Awake()
        {
            interactableText = transform.Find("InteractableText").GetComponent<TextMeshProUGUI>();
        }
    }
}