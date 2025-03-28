using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChestInventory : InventoryHolder, IInteractable
{
    public UnityAction<IInteractable> OnInteractionComplete {  get; set; }

    private bool isInteracted = false;
    private bool isNear = false;

    public bool IsInteracted
    {
        get => isInteracted;
        set => isInteracted = value;
    }

    public bool IsNear
    {
        get => isNear;
        set => isNear = value;
    }

    public void Interact()
    {
        OnDynamicInventoryDisplayRequested?.Invoke(primaryInventorySystem);
    }
}


