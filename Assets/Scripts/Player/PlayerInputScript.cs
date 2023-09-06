using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    
    // TODO ATTEMPT TO FIX ONLY ONE PLAYER MOVING BY USING PLAYERINPUT() AND JUST USING SEPERATE MAPS FOR EACH PLAYER
    // TODO WHAT SHOULD PROBABLY BE DONE IS THAT EACH SCRIPT IS A SUBSCRIPT OF AN (ABSTRACT?) PLAYER INPUT SCRIPT
    
    /*
     * We have to use InputActionMap instead of directly calling new PlayerInput();
     * This is because directly using PlayerInput controls both players instead of just the current sprite.
     */
    private InputActionMap _inputActionMap;
    private Rigidbody2D _rb;
    private Stamina _stamina;
    private float _horizontalMovement;
    private float _verticalMovement;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private float horizontalMultiplier = 15f;
    [SerializeField] private float verticalMultiplier = 20f;
    [SerializeField] private float gravity = 9.81f;

    private void Awake()
    {
        var inputActionAsset = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions;
        _inputActionMap = inputActionAsset.FindActionMap("Player");
        _rb = GetComponent<Rigidbody2D>();
        var staminaManager = GameObject.Find("StaminaManager");
        _stamina = staminaManager.GetComponent<Stamina>();
    }

    private void OnEnable()
    {
        _inputActionMap.Enable();
        _inputActionMap.FindAction("Horizontal Movement").performed += Move;
        _inputActionMap.FindAction("Horizontal Movement").canceled += StoppedMoving;
        _inputActionMap.FindAction("Crouch").performed += Crouch;
        _inputActionMap.FindAction("Crouch").canceled += StoppedCrouching;
        _inputActionMap.FindAction("Fly").performed += Fly;
        _inputActionMap.FindAction("Fly").canceled += StoppedFlying;
        _inputActionMap.FindAction("Pause").performed += Pause;
    }

    private void OnDisable()
    {
        _inputActionMap.FindAction("Horizontal Movement").performed -= Move;
        _inputActionMap.FindAction("Horizontal Movement").canceled -= StoppedMoving;
        _inputActionMap.FindAction("Crouch").performed -= Crouch;
        _inputActionMap.FindAction("Crouch").canceled -= StoppedCrouching;
        _inputActionMap.FindAction("Fly").performed -= Fly;
        _inputActionMap.FindAction("Fly").canceled -= StoppedFlying;
        _inputActionMap.FindAction("Pause").performed -= Pause;
        _inputActionMap.Disable();
    }

    private void Update()
    {
        if (!_stamina.HasStamina)
        {
            _verticalMovement = 0f;
        }

        _rb.velocity = new Vector2(_horizontalMovement, _verticalMovement - gravity);
        if (_verticalMovement > 0f)
        {
            _stamina.TickSharedStamina();
        }

        var rbTransform = _rb.transform;
        rbTransform.localScale = _rb.velocity.x switch
        {
            > 0f => new Vector3(1, 1f, 1f),
            < 0f => new Vector3(-1, 1f, 1f),
            _ => rbTransform.localScale
        };
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        _horizontalMovement = ctx.ReadValue<Vector2>().x * horizontalMultiplier;
        // TODO Flip sprite based on direction moved
    }

    private void StoppedMoving(InputAction.CallbackContext ctx) => _horizontalMovement = 0f;

    private void Crouch(InputAction.CallbackContext ctx)
    {
        // TODO manipulate sprite and hit-box 
    }

    private void StoppedCrouching(InputAction.CallbackContext ctx)
    {
        // TODO manipulate sprite and hit-box
    }

    // This is a button press, not a vector value. When this is called, we know that the button is being pressed/held.
    private void Fly(InputAction.CallbackContext ctx) => _verticalMovement = 1f * verticalMultiplier;

    private void StoppedFlying(InputAction.CallbackContext ctx) => _verticalMovement = 0f;

    private void Pause(InputAction.CallbackContext ctx)
    {
        Instantiate(pauseMenu);
    }
}