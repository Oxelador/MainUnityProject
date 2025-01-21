using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IInteractable
{
    public abstract void Interact(Interactor interactor, out bool interactSuccessful);

    public abstract void EndInteraction();
}
