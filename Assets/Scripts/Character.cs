using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [SerializeField]
    private float _speed = 200f;

    [SerializeField]
    private float _jumpForce = 300;

    private Rigidbody2D _rb;
    private Vector3 _velocity = Vector3.zero;
    private PlayerControls _controls;
    private Vector2 _moveAxis;

    private void Awake()
    {
        _controls = new PlayerControls();
        _controls.Gameplay.Move.performed += HandleMove;
        _controls.Gameplay.Move.canceled += StopMove;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void HandleMove(InputAction.CallbackContext context)
    {
        _moveAxis = context.ReadValue<Vector2>();
    }

    private void StopMove(InputAction.CallbackContext context)
    {
        _moveAxis = Vector2.zero;
    }

    private void MoveCharacter()
    {
        Vector3 targetVelocity = new Vector2(_moveAxis.x * Time.fixedDeltaTime * _speed, _rb.velocity.y);
        _rb.velocity = Vector3.SmoothDamp(_rb.velocity, targetVelocity, ref _velocity, .01f);
    }


    private void Jump()
    {
        _rb.AddForce(new Vector2(0f, _jumpForce));
    }

    private void OnEnable()
    {
        _controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        _controls.Gameplay.Disable();
    }
}
