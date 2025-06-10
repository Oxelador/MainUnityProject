using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    bool IsInteracted { get; set; }
    public void Interact();
    public string GetDescription();
}
