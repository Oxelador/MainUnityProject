using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public enum ControlType { Joystick, Keyboard }
    [SerializeField] private ControlType _controlType = ControlType.Joystick;
    public bool IsMovementLocked { get; set; } = false;

    [Header("References")]
    private FixedJoystick _joystick;
    private Animator _animator;
    private CharacterController _characterController;

    [Header("Movement Settings")]
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;

    [Header("Dash Settings")]
    [SerializeField] private CooldownUI _cooldownUI;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashTime;

    public float DashCooldown { get; private set; } = 2f;
    private bool _canDash = true;
    private float _lastDashTime = -Mathf.Infinity;

    public bool IsMoving { get; private set; } = false;

    private Vector3 _input;


    private void Awake()
    {
        _joystick = FindObjectOfType<FixedJoystick>();
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        GatherInput();
        Look();
        Move();
    }

    void GatherInput()
    {
        if (IsMovementLocked)
        {
            _input = Vector3.zero;
            return;
        }

        if (_controlType == ControlType.Joystick && _joystick != null)
        {
            _input = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
        }
        else
        {
            _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }
    }

    void Look()
    {
        if (_input != Vector3.zero)
        {
            var rot = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);

        }
    }

    void Move()
    {
        if (IsMovementLocked)
        {
            _animator.SetBool("run", false);
            return;
        }

        if (_input != Vector3.zero)
        {
            IsMoving = true;
            _animator.SetBool("run", true);
            Vector3 move = transform.forward * _input.normalized.magnitude * _speed * Time.deltaTime;
            _characterController.Move(move);
        }
        else
        {
            IsMoving = false;
            _animator.SetBool("run", false);
        }
    }

    public void Dash()
    {
        if (_canDash && Time.time >= _lastDashTime + DashCooldown)
        {
            StartCoroutine(DashCoroutine());
            _lastDashTime = Time.time;
        }
    }

    IEnumerator DashCoroutine()
    {
        _canDash = false;
        float startTime = Time.time;

        while (Time.time < startTime + _dashTime)
        {
            _characterController.Move(_input.ToIso() * _dashSpeed * Time.deltaTime);
            _cooldownUI.TriggerCooldown();
            yield return null;
        }

        yield return new WaitForSeconds(DashCooldown);
        _canDash = true;
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
