using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public LayerMask InteractionLayer;
    public Transform interactionTransform;
    public float radius = 2.0f;

    private void Update()
    {
        var colliders = Physics.OverlapSphere(interactionTransform.position, radius, InteractionLayer);

        foreach (var collider in colliders)
        {
            Debug.Log(collider.gameObject.name);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                var interactable = colliders[i].GetComponent<IInteractable>();
                
                if (interactable != null) StartInteraction(interactable);
            }
        }
    }

    void StartInteraction(IInteractable interactable)
    {
        interactable.Interact();
    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
