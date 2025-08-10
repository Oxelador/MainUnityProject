using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public enum ControlType { Joystick, Keyboard }
    [SerializeField] private ControlType controlType = ControlType.Joystick;
    public bool IsMovementLocked { get; set; } = false;

    [Header("References")]
    FixedJoystick joystick;
    CharacterController characterController;
    AnimatorHandler animatorHandler;

    [Header("Movement Settings")]
    [SerializeField] float speed = 5;
    [SerializeField] float turnSpeed = 360;
    public bool IsMoving { get; private set; } = false;
    private Vector3 input;

    [Header("Gravity Settings")]
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float groundCheckDistance = 0.1f;
    [SerializeField] LayerMask groundMask;
    float verticalVelocity = 0f;
    bool isGrounded = false;

    [Header("Dash Settings")]
    [SerializeField] CooldownUI cooldownUI;
    [SerializeField] float dashSpeed = 2.5f;
    [SerializeField] float dashTime = 0.25f;
    public float DashCooldown { get; private set; } = 2f;
    bool canDash = true;
    float lastDashTime = -Mathf.Infinity;

    public bool isInteracting;

    private void Awake()
    {
        joystick = FindObjectOfType<FixedJoystick>();
        characterController = GetComponent<CharacterController>();
        animatorHandler = GetComponent<AnimatorHandler>();
        animatorHandler.Initialize();
    }

    void Update()
    {
        isInteracting = animatorHandler.anim.GetBool("isInteracting");

        GatherInput();
        Look();
        Move();
    }

    void GatherInput()
    {
        if (IsMovementLocked)
        {
            input = Vector3.zero;
            return;
        }

        if (controlType == ControlType.Joystick && joystick != null)
        {
            input = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        }
        else
        {
            input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }
    }

    void Look()
    {
        if (input != Vector3.zero)
        {
            var rot = Quaternion.LookRotation(input.ToIso(), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);

        }
    }

    void Move()
    {
        if (IsMovementLocked)
        {
            animatorHandler.anim.SetBool("run", false);
            return;
        }

        // Ground Check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);

        if(isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 move = Vector3.zero;
        if (input != Vector3.zero)
        {
            IsMoving = true;
            animatorHandler.anim.SetBool("run", true);
            move = transform.forward * input.normalized.magnitude * speed;
        }
        else
        {
            IsMoving = false;
            animatorHandler.anim.SetBool("run", false);
        }
        move.y = verticalVelocity;

        characterController.Move(move * Time.deltaTime);
    }

    public void Dash()
    {
        if(animatorHandler.anim.GetBool("isInteracting"))
            return;

        if (input != Vector3.zero)
        {
            if (canDash && Time.time >= lastDashTime + DashCooldown)
            {
                StartCoroutine(DashCoroutine());
                animatorHandler.PlayTargetAnimation("Dash", true);
                lastDashTime = Time.time;
            }
        }
    }

    IEnumerator DashCoroutine()
    {
        canDash = false;
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            characterController.Move(input.ToIso() * dashSpeed * Time.deltaTime);
            cooldownUI.TriggerCooldown();
            yield return null;
        }

        yield return new WaitForSeconds(DashCooldown);
        canDash = true;
    }

    public void LockMovement()
    {
        IsMovementLocked = true;
        IsMoving = false;
    }

    public void UnlockMovement()
    {
        IsMovementLocked = false;
    }
}
