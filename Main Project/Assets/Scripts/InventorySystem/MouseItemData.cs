using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class MouseItemData : MonoBehaviour
{
    public Image itemSprite;
    public TextMeshProUGUI itemCount;
    public InventorySlot assignedInventorySlot;

    private Transform _playerTransform;
    [SerializeField] private float _dropOffset = 1f;

    public void Awake()
    {
        itemSprite.color = Color.clear;

        itemSprite.preserveAspect = true;
        itemCount.text = "";

        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (_playerTransform == null) Debug.Log("Player not found!");

    }

    public void UpdateMouseSlot(InventorySlot invSlot)
    {
        assignedInventorySlot.AssignItem(invSlot);
        UpdateMouseSlot();
    }

    public void UpdateMouseSlot()
    {
        itemSprite.sprite = assignedInventorySlot.ItemData.Icon;
        itemCount.text = assignedInventorySlot.StackSize.ToString();
        itemSprite.color = Color.white;
    }

    private void Update()
    {
        // TODO: Add controller support

        if(assignedInventorySlot.ItemData != null) // If has an item, follow the mouse position.
        {
            transform.position = Input.mousePosition;

            if(Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
            {
                if(assignedInventorySlot.ItemData.ItemPrefab != null)
                {
                    Instantiate(assignedInventorySlot.ItemData.ItemPrefab,
                        _playerTransform.position + _playerTransform.forward * _dropOffset,
                        Quaternion.identity);
                }

                if(assignedInventorySlot.StackSize > 1)
                {
                    assignedInventorySlot.AddToStack(-1);
                    UpdateMouseSlot();
                }
                else
                {
                    ClearSlot();
                }
            }
        }
    }

    public void ClearSlot()
    {
        assignedInventorySlot.CrearSlot();
        itemCount.text = "";
        itemSprite.color = Color.clear;
        itemSprite.sprite = null;
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
