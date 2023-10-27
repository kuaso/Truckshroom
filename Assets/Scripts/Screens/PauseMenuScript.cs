using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{

    private PlayerInput _playerInput;
    public delegate void OnMenuDestroyed();
    public static event OnMenuDestroyed MenuDestroyed;
    
    private Button _currentButton;
    private Button[] _buttons;
    
    private void OnEnable()
    {
        _buttons = GetComponentsInChildren<Button>();
        _currentButton = _buttons[0];
        Debug.Log(_currentButton.name);
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
    
    public void DestroyMenu()
    {
        Destroy(transform.root.gameObject);
        MenuDestroyed?.Invoke();
    }

    public void ReturnToMainMenu() {
        SaveGame();
        SceneManager.LoadScene("MainMenu");
        // MenuDestroyed is still invoked as the scene it is no longer active in the new scene
        MenuDestroyed?.Invoke();
    }
    
    private void SaveGame() {  
        // TODO save logic, either in this or in another script
    }

    private void NavigateDown(InputAction.CallbackContext ctx)
    {
        // TODO
    }
    
    private void NavigateUp(InputAction.CallbackContext ctx)
    {
        // TODO
    }
    
    private void SelectedOption(InputAction.CallbackContext ctx)
    {
        // TODO
    }
    
    private void GoBack(InputAction.CallbackContext ctx)
    {
        // TODO
    }
}
