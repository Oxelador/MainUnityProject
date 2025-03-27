using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChestInventory : InventoryHolder, IInteractable
{
    public UnityAction<IInteractable> OnInteractionComplete {  get; set; }

    private bool isInteracted = false;

    public bool IsInteracted
    {
        get => isInteracted;
        set => isInteracted = value;
    }

    public void Interact()
    {
        OnDynamicInventoryDisplayRequested?.Invoke(primaryInventorySystem);
    }
}


