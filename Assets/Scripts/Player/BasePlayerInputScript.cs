using UnityEngine;
using UnityEngine.InputSystem;

/**
 * This class is meant to be extended on by the PlayerInputScript class.
 * onEnable and onDisable methods should be written by the child class.
 */
public abstract class BasePlayerInputScript : MonoBehaviour
{
    private Stamina _stamina;
    private float _horizontalMovement;
    private float _verticalMovement;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private float horizontalMultiplier = 15f;
    [SerializeField] private float verticalMultiplier = 20f;
    [SerializeField] private float gravity = 9.81f;

    private void Awake()
    {
        var staminaManager = GameObject.Find("StaminaManager");
        _stamina = staminaManager.GetComponent<Stamina>();
    }

    protected void UpdateLoop(Rigidbody2D rb, int playerNumber)
    {
        if (!_stamina.HasStamina)
        {
            _verticalMovement = 0f;
        }

        rb.velocity = new Vector2(_horizontalMovement, _verticalMovement - gravity);
        if (_verticalMovement > 0f)
        {
            _stamina.TickSharedStamina(playerNumber);
        }
        
        // Compare current y pos to previous y pos, with some tolerance for floating points doesn't work
        // rb.velocity does not work as it will be ${gravity} by default
        // TODO FIND A WAY to only regen when on the ground _stamina.RequestStaminaRegeneration(playerNumber);


        // var rbTransform = rb.transform;
        // rbTransform.localScale = rb.velocity.x switch
        // {
        //     > 0f => new Vector3(1, 1f, 1f),
        //     < 0f => new Vector3(-1, 1f, 1f),
        //     _ => rbTransform.localScale
        // };
    }

    protected void Move(InputAction.CallbackContext ctx)
    {
        _horizontalMovement = ctx.ReadValue<Vector2>().x * horizontalMultiplier;
        // TODO Flip sprite based on direction moved
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
        Instantiate(pauseMenu);
    }
}