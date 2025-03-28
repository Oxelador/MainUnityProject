using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public LayerMask InteractionLayer;
    public Transform interactionTransform;
    public float radius = 2.0f;

    private HashSet<IInteractable> _nearbyInteractableObjects = new HashSet<IInteractable>();

    private void Update()
    {
        var colliders = Physics.OverlapSphere(interactionTransform.position, radius, InteractionLayer);
        var currentInteractableObjects = new HashSet<IInteractable>();

        // check all objects that fall within the radius
        foreach (var collider in colliders)
        {
            var interactable = collider.GetComponent<IInteractable>();

            // check if obj is already interacted before add
            if (interactable != null && !interactable.IsInteracted)
            {
                currentInteractableObjects.Add(collider.gameObject.GetComponent<IInteractable>());

                // if the object is new, add it
                if (!_nearbyInteractableObjects.Contains(collider.gameObject.GetComponent<IInteractable>())) // ?
                {
                    //Debug.Log("Object add: " + collider.gameObject.name);
                    _nearbyInteractableObjects.Add(collider.gameObject.GetComponent<IInteractable>());
                }
            }
        }

        // checking which objects have left the radius
        var objectsToRemove = new HashSet<IInteractable>(_nearbyInteractableObjects);
        objectsToRemove.ExceptWith(currentInteractableObjects);

        foreach (var obj in objectsToRemove)
        {
            if(obj == null)
            {
                _nearbyInteractableObjects.Remove(obj);
            } else
            {
                //Debug.Log("Object remove: " + obj.name);
                _nearbyInteractableObjects.Remove(obj);
            }
        }

        InteractionButtonsUI.Instance.UpdateUI(_nearbyInteractableObjects);

    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
