using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuNavigationScript : MonoBehaviour
{
    private PlayerInput _playerInput;

    private Button[] _buttons;
    private Button _currentButton;
    private int _currentButtonIndex;
    private ColorBlock _originalButtonColor;

    private void OnEnable()
    {
        _buttons = GetComponentsInChildren<Button>();
        _currentButton = _buttons[0];
        _originalButtonColor = _currentButton.colors;
        // Make it so that the first button is highlighted by default
        ChangeColorToClicked();
        
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.Menuing.NavigateDown.performed += NavigateDown;
        _playerInput.Menuing.NavigateUp.performed += NavigateUp;
        _playerInput.Menuing.Select.performed += SelectedOption;
        _playerInput.Menuing.Back.performed += GoBack;
        // Movement classes are responsible for suspending their own input when the pause menu is active
    }

    private void OnDisable()
    {
        _playerInput.Menuing.NavigateDown.performed -= NavigateDown;
        _playerInput.Menuing.NavigateUp.performed -= NavigateUp;
        _playerInput.Menuing.Select.performed -= SelectedOption;
        _playerInput.Menuing.Back.performed -= GoBack;
        _playerInput.Disable();
    }

    private void RestoreColor() => _currentButton.colors = _originalButtonColor;
    private void ChangeColorToClicked()
    {
        _originalButtonColor = _currentButton.colors;
        var colorCopy = _originalButtonColor;
        colorCopy.normalColor = _currentButton.colors.pressedColor;
        _currentButton.colors = colorCopy;
    }

    private void NavigateDown(InputAction.CallbackContext ctx)
    {
        RestoreColor();
        // _buttons.Length - 1 is so that the last value is the index, not the size
        _currentButtonIndex = _buttons.Length - 1 == _currentButtonIndex ? 0 : _currentButtonIndex + 1;
        _currentButton = _buttons[_currentButtonIndex];
        ChangeColorToClicked();
    }

    private void NavigateUp(InputAction.CallbackContext ctx)
    {
        RestoreColor();
        // _buttons.Length - 1 is so that we get the index of the last button, not the size
        _currentButtonIndex = 0 == _currentButtonIndex ? _buttons.Length - 1 : _currentButtonIndex - 1;
        _currentButton = _buttons[_currentButtonIndex];
        Debug.Log(_currentButton.name);
        ChangeColorToClicked();
    }

    private void SelectedOption(InputAction.CallbackContext ctx)
    {
        _currentButton.onClick.Invoke();
    }

    private void GoBack(InputAction.CallbackContext ctx)
    {
        // TODO
    }
}