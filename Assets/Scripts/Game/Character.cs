using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    private float _speed = 200f;
    public float Speed {
        get { return _speed; }
        set { _speed = value; }
    }

    private float _jumpForce = 300f;
    public float JumpForce
    {
        get { return _jumpForce; }
        set { _jumpForce = value; }
    }

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _velocity = Vector3.zero;
    private PlayerControls _controls;
    private Vector2 _moveAxis;
    private bool _isJumping = false;

    private void Awake()
    {
        _controls = new PlayerControls();

        _speed = gameObject.GetComponent<SpiritData>().spirit._speed;
        _jumpForce = gameObject.GetComponent<SpiritData>().spirit._jumpForce;

        _controls.Gameplay.Move.performed += HandleMove;
        _controls.Gameplay.Move.canceled += context => _moveAxis = Vector2.zero;

        _controls.Gameplay.Jump.performed += HandleJump;
        _controls.Gameplay.Jump.canceled += context => _isJumping = false;

        _controls.Gameplay.Change.performed += HandleChange;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        Jump();
        AnimationsMovements(_rb.velocity.x);
    }

    private void HandleMove(InputAction.CallbackContext context)
    {
        _moveAxis = context.ReadValue<Vector2>();
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        _isJumping = true;
    }

    private void HandleChange(InputAction.CallbackContext context)
    {
        GameManager.instance.ChangeSpirit();
        //_moveAxis = Vector2.zero;
    }

    private void MoveCharacter()
    {
        Vector3 targetVelocity = new Vector2(_moveAxis.x * Time.fixedDeltaTime * _speed, _rb.velocity.y);
        _rb.velocity = Vector3.SmoothDamp(_rb.velocity, targetVelocity, ref _velocity, .05f);             
    }


    private void Jump()
    {
        if (!_isJumping || _rb.velocity.y != 0) return;

        _rb.AddForce(new Vector2(0f, _jumpForce));
        _isJumping = false;
    }

    private void AnimationsMovements(float speedX)
    {
        if (speedX < 0f)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }

        _animator.SetFloat("Speed", Mathf.Abs(speedX));
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
