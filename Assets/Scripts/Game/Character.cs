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

    private float _throwForce = 10f;
    public float ThrowForce
    {
        get { return _throwForce; }
        set { _throwForce = value; }
    }

    private Vector2 _direction = new Vector2(0, 0);
    public Vector2 Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private BabyCatcher _babyCatcher;
    private Spirit.Type _type;
    private Vector3 _velocity = Vector3.zero;
    private PlayerControls _controls;
    private Vector2 _moveAxis;
    private bool _isJumping = false;
    private bool _isAim = false;

    private void Awake()
    {
        Debug.Log("AWAKE §§§");
        _controls = new PlayerControls();

        _speed = gameObject.GetComponent<SpiritData>().spirit._speed;
        _jumpForce = gameObject.GetComponent<SpiritData>().spirit._jumpForce;
        _throwForce = gameObject.GetComponent<SpiritData>().spirit._throwForce;
        _type = gameObject.GetComponent<SpiritData>().spirit.type;


        switch (_type)
        {
            case Spirit.Type.small:
                _direction = Vector2.right * _throwForce;
                break;

            case Spirit.Type.Large:
                _direction = Vector2.up * _throwForce;
                break;
            default:
                break;
        }

        _controls.Gameplay.Move.performed += HandleMove;
        _controls.Gameplay.Move.canceled += context => _moveAxis = Vector2.zero;

        _controls.Gameplay.Jump.performed += HandleJump;
        _controls.Gameplay.Jump.canceled += context => { _isJumping = false; _isAim = false; };

        _controls.Gameplay.Change.performed += HandleChange;

        _controls.Gameplay.Aim.performed += HandleAim;
        _controls.Gameplay.Aim.canceled += context => _isAim = false;

        _controls.Gameplay.Fire.performed += HandleFire;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _babyCatcher = GameObject.Find("BabyCatcher").GetComponent<BabyCatcher>();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        Jump();
        AnimationsMovements(_rb.velocity.x);
        _animator.SetBool("InAir", Mathf.Abs(_rb.velocity.y) > 0.1f);
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

    private void HandleAim(InputAction.CallbackContext context)
    {
        _isAim = context.control.IsPressed();
    }

    private void HandleFire(InputAction.CallbackContext context)
    {
        if(_isAim)
        {
            switch(context.control.name)
            {
                case "upArrow":
                    if (_type == Spirit.Type.Large || _type == Spirit.Type.medium)
                    {
                        _babyCatcher.Release(_direction);
                    }                   
                    break;
                case "leftArrow":
                    if (_type == Spirit.Type.small || _type == Spirit.Type.medium)
                    {
                        _babyCatcher.Release(-_direction);
                    }
                    break;
                case "rightArrow":
                    if (_type == Spirit.Type.small || _type == Spirit.Type.medium)
                    {
                        _babyCatcher.Release(_direction);
                    }
                    break;
                case "downArrow":
                    if (_type == Spirit.Type.medium)
                    {
                        _babyCatcher.Release(_direction);
                    }
                    break;
                default:
                    break;
            }
        }
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
