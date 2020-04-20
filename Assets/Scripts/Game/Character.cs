using System.Collections;
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
    private GameObject _exitPoints;
    private Spirit.Type _type;
    private Vector3 _velocity = Vector3.zero;
    private PlayerControls _controls;
    private Vector2 _moveAxis;
    private bool _isJumping = false;
    private bool _isAim = false;
    private BulletTime bulletTime;

    private void Awake()
    {
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
        _controls.Gameplay.Aim.canceled += context => { _isAim = false; if (bulletTime != null) bulletTime.Active = false; };

        _controls.Gameplay.Direction.performed += HandleDirection;
    }

    IEnumerator Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _babyCatcher = gameObject.transform.Find("BabyCatcher").GetComponent<BabyCatcher>();
        _exitPoints = gameObject.transform.Find("ExitPoints").gameObject;

        if (_type == Spirit.Type.Large)
        {
            _exitPoints.transform.Find("down-arrow").gameObject.SetActive(false);
            _exitPoints.transform.Find("left-arrow").gameObject.SetActive(false);
            _exitPoints.transform.Find("right-arrow").gameObject.SetActive(false);
        }
        else if (_type == Spirit.Type.Large)
        {
            _exitPoints.transform.Find("down-arrow").gameObject.SetActive(false);
            _exitPoints.transform.Find("left-arrow").gameObject.SetActive(false);
            _exitPoints.transform.Find("up-arrow").gameObject.SetActive(false);
        }
        yield return null; // just to make sure everybody is instancied
        bulletTime = FindObjectOfType<BulletTime>();
    }


    private void FixedUpdate()
    {
        MoveCharacter();
        Jump();
        AnimationsMovements(_rb.velocity.x);
        _animator.SetBool("InAir", Mathf.Abs(_rb.velocity.y) >= 1f);
    }

    private void HandleMove(InputAction.CallbackContext context)
    {
        if (_isAim) return;
        _moveAxis = context.ReadValue<Vector2>();
    }

    private void HandleJump(InputAction.CallbackContext context)
    {        
        _isJumping = true;
    }

    private void HandleChange(InputAction.CallbackContext context)
    {
        Debug.Log("handle change");
        GameManager.instance.ChangeSpirit();
    }

    private void HandleAim(InputAction.CallbackContext context)
    {
        _isAim = context.control.IsPressed();
        if (bulletTime != null) bulletTime.Active = true;

        if (!_isAim && _babyCatcher.isCarrying)
        {
            _exitPoints.SetActive(false);          
            _babyCatcher.Release(_direction);
        }
        else
        {
            _direction = Vector2.up * _throwForce;
            _exitPoints.SetActive(true);
        }
        
    }

    private void HandleDirection(InputAction.CallbackContext context)
    {
        if(_isAim && _type == Spirit.Type.medium)
        {
            switch(context.control.name)
            {
                case "upArrow":
                    _direction = Vector2.up * _throwForce;
                    break;
                case "leftArrow":
                    _direction = Vector2.left * _throwForce;
                    break;
                case "rightArrow":
                    _direction = Vector2.right * _throwForce;
                    break;
                case "downArrow":
                    _direction = Vector2.down * _throwForce;
                    break;
                default:
                    _direction = Vector2.up * _throwForce;
                    break;
            }
        }
    }

    private void MoveCharacter()
    {
        // block horizontal movement when carrying
        if (_babyCatcher.isCarrying && Mathf.Abs(_rb.velocity.y) > 0.1f)
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y) ;
            return;
        } 
        Vector3 targetVelocity = new Vector2(_moveAxis.x * Time.fixedDeltaTime * _speed, _rb.velocity.y);
        _rb.velocity = Vector3.SmoothDamp(_rb.velocity, targetVelocity, ref _velocity, .05f);             
    }


    private void Jump()
    {
        if (!_isJumping || _rb.velocity.y != 0 || _babyCatcher.isCarrying) return;

        _animator.SetTrigger("Jump");

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
