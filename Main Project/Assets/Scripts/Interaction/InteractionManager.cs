using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public Collider interactionArea;
    private List<IInteractable> interactableObjects = new List<IInteractable>();

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if(interactable != null && !interactableObjects.Contains(interactable))
        {
            interactableObjects.Add(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null && !interactableObjects.Contains(interactable))
        {
            interactableObjects.Remove(interactable);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(interactableObjects.Count > 0)
            {
                interactableObjects[0].Interact();
                // ������� ������ �� ������ ����� ��������������, ���� ���������� 
                // interactableObjects.RemoveAt(0);
            }
        }
    }
}
