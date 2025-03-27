using UnityEngine;

public interface IInteractable
{
    bool IsInteracted { get; }
    void Interact();
}
