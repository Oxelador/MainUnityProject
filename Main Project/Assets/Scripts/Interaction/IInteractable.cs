using UnityEngine;

public interface IInteractable
{
    bool IsInteracted { get; }

    bool IsNear { get; set; }

    void Interact();
}
