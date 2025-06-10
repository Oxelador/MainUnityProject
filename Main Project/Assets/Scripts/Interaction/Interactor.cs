using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractionDistance;

    public GameObject interactionUI;
    public TextMeshProUGUI interactionText;

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
        Ray ray = new Ray(InteractorSource.position, InteractorSource.forward);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, InteractionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null && !interactable.IsInteracted)
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
        }
        else
        {
            interactionUI.SetActive(false);
        }
    }
}
