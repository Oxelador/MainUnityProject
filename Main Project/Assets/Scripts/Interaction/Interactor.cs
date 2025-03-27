using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public LayerMask InteractionLayer;
    public Transform interactionTransform;
    public float radius = 2.0f;

    private HashSet<GameObject> _nearbyObjects = new HashSet<GameObject>();

    private void Update()
    {
        var colliders = Physics.OverlapSphere(interactionTransform.position, radius, InteractionLayer);
        var currentObjects = new HashSet<GameObject>();

        // check all objects that fall within the radius
        foreach (var collider in colliders)
        {
            var interactable = collider.GetComponent<IInteractable>();

            // check if obj is already interacted before add
            if (interactable != null && !interactable.IsInteracted)
            {
                currentObjects.Add(collider.gameObject);

                // if the object is new, add it
                if (!_nearbyObjects.Contains(collider.gameObject))
                {
                    //Debug.Log("Object add: " + collider.gameObject.name);
                    _nearbyObjects.Add(collider.gameObject);
                }
            }
        }

        // checking which objects have left the radius
        var objectsToRemove = new HashSet<GameObject>(_nearbyObjects);
        objectsToRemove.ExceptWith(currentObjects);

        foreach (var obj in objectsToRemove)
        {
            if(obj == null)
            {
                _nearbyObjects.Remove(obj);
            } else
            {
                //Debug.Log("Object remove: " + obj.name);
                _nearbyObjects.Remove(obj);
            }
        }

        InteractionButtonsUI.Instance.UpdateUI(_nearbyObjects);

    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
