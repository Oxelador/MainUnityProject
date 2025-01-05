using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public Collider interactionArea;
    private List<Interactable> interactableObjects = new List<Interactable>();

    private void OnTriggerEnter(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if(interactable != null && !interactableObjects.Contains(interactable))
        {
            interactableObjects.Add(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
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
                // Удаляем объект из списка после взаимодействия, если необходимо 
                // interactableObjects.RemoveAt(0);
            }
        }
    }
}
