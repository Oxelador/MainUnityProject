using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public enum ControlType { Joystick, Keyboard }
    [SerializeField] private ControlType _controlType = ControlType.Joystick;

    [SerializeField] private Animator _anim;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;

    private Vector3 _input;
    private CharacterController _characterController;

    private void Awake()
    {
        _anim.applyRootMotion = false;
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
        if(_controlType == ControlType.Joystick && _joystick != null)
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
        if (_input != Vector3.zero)
        {
            _anim.SetBool("run", true);
            Vector3 move = transform.forward * _input.normalized.magnitude * _speed * Time.deltaTime;
            _characterController.Move(move);
        }
        else
        {
            _anim.SetBool("run", false);
        }
    }
}
