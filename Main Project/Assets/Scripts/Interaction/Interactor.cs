using TMPro;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractionDistance;

    public GameObject interactionUI;
    public TextMeshProUGUI interactionText;
    private float interactionRange;

    private void Start()
    {
        interactionUI.SetActive(false);
    }

    private void Update()
    {
        InteractionRay();
    }

    void InteractionRay()
    {
        Collider[] colliders = Physics.OverlapSphere(InteractorSource.position, InteractionDistance);

        IInteractable interactable = null;
        foreach (var collider in colliders)
        {
            interactable = collider.GetComponent<IInteractable>();
            if (interactable != null && !interactable.IsInteracted)
            {
                break;
            }
            interactable = null;
        }

        if (interactable != null)
        {

            interactionText.text = interactable.GetDescription();
            interactionUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                interactable.IsInteracted = true;
                interactable.Interact();
                interactionUI.SetActive(false);
            }

        }
        else
        {
            interactionUI.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(InteractorSource.position, interactionRange);
    }
}
