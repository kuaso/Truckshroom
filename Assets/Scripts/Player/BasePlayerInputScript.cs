using UnityEngine;
using UnityEngine.InputSystem;

/**
 * This class is meant to be extended on by the PlayerInputScript class.
 * onEnable and onDisable methods should be written by the child class.
 */
public abstract class BasePlayerInputScript : MonoBehaviour
{
    private readonly int _playerNumber;
    private Stamina _stamina;
    private float _horizontalMovement;
    private float _verticalMovement;
    private bool _isPaused;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private float horizontalMultiplier = 15f;
    [SerializeField] private float verticalMultiplier = 20f;
    [SerializeField] private float gravity = 9.81f;

    protected BasePlayerInputScript(int playerNumber)
    {
        _playerNumber = playerNumber;
    }

    private void Awake()
    {
        var staminaManager = GameObject.Find("StaminaManager");
        _stamina = staminaManager.GetComponent<Stamina>();
        PauseMenuScript.MenuDestroyed += UnPause;
    }

    private void OnDestroy()
    {
        PauseMenuScript.MenuDestroyed -= UnPause;
    }
    private void UnPause() => _isPaused = false;
    
    protected void UpdateLoop(Rigidbody2D rb)
    {
        if (!_stamina.HasStamina)
        {
            _verticalMovement = 0f;
        }

        rb.velocity = new Vector2(_horizontalMovement, _verticalMovement - gravity);
        if (_verticalMovement > 0f)
        {
            _stamina.TickSharedStamina();
        }

        var rbTransform = rb.transform;
        var localScale = rbTransform.localScale;
        rbTransform.localScale = rb.velocity.x switch
        {
            > 0f => new Vector3(Mathf.Abs(localScale.x), localScale.y, localScale.z),
            < 0f => new Vector3(Mathf.Abs(localScale.x) * -1, localScale.y, localScale.z),
            _ => localScale
        };
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("AllowStaminaRecharge"))
        {
            _stamina.RequestStaminaRegeneration(_playerNumber);
        }
    }

    protected void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("AllowStaminaRecharge"))
        {
            _stamina.StopStaminaRegeneration(_playerNumber);
        }
    }

    protected void Move(InputAction.CallbackContext ctx)
    {
        _horizontalMovement = ctx.ReadValue<Vector2>().x * horizontalMultiplier;
        // Sprite is flipped based on direction moved in UpdateLoop()
    }

    protected void StoppedMoving(InputAction.CallbackContext ctx) => _horizontalMovement = 0f;

    protected void Crouch(InputAction.CallbackContext ctx)
    {
        // TODO manipulate sprite and hit-box 
    }

    protected void StoppedCrouching(InputAction.CallbackContext ctx)
    {
        // TODO manipulate sprite and hit-box
    }

    // This is a button press, not a vector value. When this is called, we know that the button is being pressed/held.
    protected void Fly(InputAction.CallbackContext ctx) => _verticalMovement = 1f * verticalMultiplier;

    protected void StoppedFlying(InputAction.CallbackContext ctx) => _verticalMovement = 0f;

    protected void Pause(InputAction.CallbackContext ctx)
    {
        if (_isPaused) return;
        Instantiate(pauseMenu);
        _isPaused = true;
    }
}