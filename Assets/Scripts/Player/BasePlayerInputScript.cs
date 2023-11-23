using System;
using System.Collections.Generic;
using System.Linq;
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

    private bool _isPaused;

    // Each entry should be set by the child class
    protected static readonly Dictionary<int, Dictionary<Collider2D, bool>> ColliderStates = new();

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private float horizontalMultiplier = 15f;
    [SerializeField] private float verticalMultiplier = 20f;
    [SerializeField] private float gravity = 9.81f;
    
    
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

    private void Animate(Animator animator)
    {
        // TODO Animate by changing the animator's parameters
        // animator.SetBool("isCrouching", true);
    }

    protected void UpdateLoop(Rigidbody2D rb, Animator animator)
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
        
        Animate(animator);
    }

    protected void CollisionEntered2D(Collision2D other, int playerNumber)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Ground")) return;
        // Note that other.collider gets what we collided into, not the player collider.
        var playerCollider = other.otherCollider;
        if (!ColliderStates[playerNumber].ContainsKey(playerCollider)) return;
        ColliderStates[playerNumber][playerCollider] = true;

        var canRegenerate = ColliderStates.All(
            playerColliders => playerColliders.Value.ContainsValue(true)
        );
        if (canRegenerate)
        {
            _stamina.Regenerate();
        }
    }

    protected void CollisionExited2D(Collision2D other, int playerNumber)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Ground")) return;
        // Note that other.collider gets what we collided into, not the player collider.
        var playerCollider = other.otherCollider;
        if (!ColliderStates[playerNumber].ContainsKey(playerCollider)) return;
        ColliderStates[playerNumber][playerCollider] = false; // Note that this is false, unlike #CollisionEntered2D.

        var shouldStopRegenerate = ColliderStates.Any(
            playerColliders => !playerColliders.Value.ContainsValue(true)
        );
        if (shouldStopRegenerate)
        {
            _stamina.StopRegenerate();
        }
    }

    protected void Move(InputAction.CallbackContext ctx)
    {
        _horizontalMovement = ctx.ReadValue<Vector2>().x * horizontalMultiplier;
        // Sprite is flipped based on direction moved in UpdateLoop()
    }

    protected void StoppedMoving(InputAction.CallbackContext ctx) => _horizontalMovement = 0f;

    protected void Crouch(InputAction.CallbackContext ctx) => _verticalMovement = -1f * verticalMultiplier;

    protected void StoppedCrouching(InputAction.CallbackContext ctx) => _verticalMovement = 0f;

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