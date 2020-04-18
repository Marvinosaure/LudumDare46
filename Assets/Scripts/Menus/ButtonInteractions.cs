using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonInteractions : MonoBehaviour
{
    [SerializeField] private bool _focused = false;

    private Button _button;

    [SerializeField] ButtonInteractions _buttonOnLeft = null;
    [SerializeField] ButtonInteractions _buttonOnRight = null;
    [SerializeField] ButtonInteractions _buttonOnTop = null;
    [SerializeField] ButtonInteractions _ButtomOnBottom = null;

    private PlayerControls _controls;

    public bool Focused { get => _focused; set => _focused = value; }

    void Awake()
    {
        _button = GetComponent<Button>();

        _controls = new PlayerControls();

        // Navigation
        _controls.UI.Up.started += HandleUp;
        _controls.UI.Down.started += HandleDown;
        _controls.UI.Right.started += HandleRight;
        _controls.UI.Left.started += HandleLeft;

        _controls.UI.Validate.started += HandleValidate;
    }

    private void HandleValidate(InputAction.CallbackContext obj)
    {
        if (_focused)
        {
            _button.onClick.Invoke();
        }
    }

    private void ChangeCurrentFocusedButtonTo(ref ButtonInteractions button)
    {
        if (_focused && button != null)
        {
            this._focused = false;
            button._focused = true;
        }
    }

    private void HandleUp(InputAction.CallbackContext context)
    {
        if (context.started)
        ChangeCurrentFocusedButtonTo(ref _buttonOnTop);
    }

    private void HandleDown(InputAction.CallbackContext context)
    {
        ChangeCurrentFocusedButtonTo(ref _ButtomOnBottom);
    }

    private void HandleLeft(InputAction.CallbackContext context)
    {
        ChangeCurrentFocusedButtonTo(ref _buttonOnLeft);
    }

    private void HandleRight(InputAction.CallbackContext context)
    {
        ChangeCurrentFocusedButtonTo(ref _buttonOnRight);
    }

    // Update is called once per frame
    void Update()
    {
        if (_focused && Gamepad.current != null)
        {
            _button.image.color = _button.colors.highlightedColor;
        }
        else
        {
            _button.image.color = _button.colors.normalColor;
        }
    }



    private void OnEnable()
    {
        _controls.UI.Enable();
    }

    private void OnDisable()
    {
        _controls.UI.Disable();
    }
}
