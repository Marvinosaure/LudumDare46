using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    private PlayerControls _controls;
    private Vector2 _moveAxis;

    [SerializeField]
    private float _speed = 8f;

    private void Awake()
    {
        _controls = new PlayerControls();

        _controls.Gameplay.Move.performed += HandleMove;
    }

    private void Update()
    {
        
    }

    private void HandleMove(InputAction.CallbackContext context)
    {
        _moveAxis = context.ReadValue<Vector2>();
        Debug.Log(_moveAxis);

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
