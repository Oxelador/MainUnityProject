using UnityEngine;

namespace oxi
{
    public class PlayerManager : MonoBehaviour
    {
        [HideInInspector] public PlayerInventory playerInventory;

        InputHandler inputHandler;
        Animator animator;
        PlayerLocomotion playerLocomotion;

        InteractableUI interactableUI;
        public GameObject interactableUIGameObject;

        [Header("Player Flags")]
        public bool isInteracting;
        public bool isInAir;
        public bool isGrounded;
        public bool canDoCombo;

        void Start()
        {
            playerInventory = GetComponent<PlayerInventory>();
            inputHandler = GetComponent<InputHandler>();
            animator = GetComponentInChildren<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            interactableUI = FindObjectOfType<InteractableUI>();
        }

        void Update()
        {
            float delta = Time.deltaTime;

            isInteracting = animator.GetBool("isInteracting");
            canDoCombo = animator.GetBool("canDoCombo");

            inputHandler.TickInput(delta);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRolling(delta);
            playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
            CheckForInteractableObject();
        }

        void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.lightAttackInput = false;
            inputHandler.heavyAttackInput = false;
            inputHandler.interactingInput = false;

            if (isInAir)
            {
                playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
            }
        }

        public void CheckForInteractableObject()
        {
            RaycastHit hit;

            if (Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 1f))
            {
                if(hit.collider.tag == "Interactable")
                {
                    Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                    if (interactableObject != null)
                    {
                        string interactableText = interactableObject.interactbleText;
                        interactableUI.interactableText.text = interactableText;
                        interactableUIGameObject.SetActive(true);

                        if (inputHandler.interactingInput)
                        {
                            hit.collider.GetComponent<Interactable>().Interact(this);
                        }
                    }
                }

                if (hit.collider.tag == "Gold")
                {
                    Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                    if (interactableObject != null)
                    {
                        hit.collider.GetComponent<Interactable>().Interact(this);
                    }
                }
            }
            else
            {
                if(interactableUIGameObject != null)
                {
                    interactableUIGameObject.SetActive(false);
                }
            }
        }
    }
}
