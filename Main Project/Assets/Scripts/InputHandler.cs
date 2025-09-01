using UnityEngine;
using UnityEngine.UI;

namespace oxi
{
    public class InputHandler : MonoBehaviour
    {
        public enum ControlType { Joystick, Keyboard }

        [SerializeField] private ControlType controlType = ControlType.Joystick;

        public float horizontal;
        public float vertical;
        public float moveAmount;

        public bool rollInput;
        public bool interactingInput;
        public bool lightAttackInput;
        public bool heavyAttackInput;

        public bool rollFlag;
        public bool comboFlag;
        public float rollInputTimer;

        [Header("Action Buttons")]
        public Button rollButton;
        public Button basicAttackButton;

        PlayerControls inputActions;
        FixedJoystick joystick;
        PlayerCombatController combatController;
        PlayerManager playerManager;

        Vector2 movementInput;

        private void Awake()
        {
            joystick = FindObjectOfType<FixedJoystick>();
            combatController = GetComponent<PlayerCombatController>();
            playerManager = GetComponent<PlayerManager>();
        }

        public void OnEnable()
        {
            if (inputActions == null && controlType == ControlType.Keyboard)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.Enable();
            }
            // for joystick input
            /*
            if (rollButton != null)
                rollButton.onClick.AddListener(() => rollInput = true);

            if (basicAttackButton != null)
                basicAttackButton.onClick.AddListener(() => lightAttackInput = true);
            */
        }

        private void OnDisable()
        {
            if (inputActions != null)
            {
                inputActions.Disable();
            }
        }

        public void TickInput(float delta)
        {
            MoveInput(delta);
            HandleRollInput(delta);
            HandleAttackInput(delta);
            HandleInteractingButtonInput();

            //rollInput = false; // for "joystick" input
        }


        private void MoveInput(float delta)
        {
            if (controlType == ControlType.Joystick && joystick != null)
            {
                horizontal = joystick.Horizontal;
                vertical = joystick.Vertical;
            }
            else
            {
                horizontal = movementInput.x;
                vertical = movementInput.y;
            }
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        }

        private void HandleRollInput(float delta)
        {
            rollInput = inputActions.PlayerActions.Roll.triggered;

            if (rollInput)
            {
                rollInputTimer += delta;
            }
            else
            {
                if (rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    rollFlag = true;
                }
                rollInputTimer = 0;
            }

        }

        private void HandleAttackInput(float delta)
        {
            inputActions.PlayerActions.LightAttack.performed += i => lightAttackInput = true;
            inputActions.PlayerActions.HeavyAttack.performed += i => heavyAttackInput = true;

            if (lightAttackInput)
            {
                if (playerManager.canDoCombo)
                {
                    comboFlag = true;
                    combatController.HandleWeaponCombo();
                    comboFlag = false;
                }
                else
                {
                    if (playerManager.isInteracting)
                        return;

                    if (playerManager.canDoCombo)
                        return;

                    combatController.HandleLightAttack();
                }
            }

            if (heavyAttackInput)
            {
                if (playerManager.isInteracting)
                    return;

                combatController.HandleHeavyAttack();
            }
        }

        private void HandleInteractingButtonInput()
        {
            inputActions.PlayerActions.Interacting.performed += i => interactingInput = true;

            
        }
    }
}
