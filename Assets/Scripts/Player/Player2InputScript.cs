using UnityEngine;

public class Player2InputScript : BasePlayerInputScript
{
    
    // TODO ATTEMPT TO FIX ONLY ONE PLAYER MOVING BY USING PLAYERINPUT() AND JUST USING SEPERATE MAPS FOR EACH PLAYER
    // TODO WHAT SHOULD PROBABLY BE DONE IS THAT EACH SCRIPT IS A SUBSCRIPT OF AN (ABSTRACT?) PLAYER INPUT SCRIPT
    private PlayerInput _playerInput;
    private Rigidbody2D _rb;
    private Collider2D _coll;
    public Player2InputScript() : base(1) { }
    
    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _coll = GetComponent<Collider2D>();
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.Player2.HorizontalMovement.performed += Move;
        _playerInput.Player2.HorizontalMovement.canceled += StoppedMoving;
        _playerInput.Player2.Crouch.performed += Crouch;
        _playerInput.Player2.Crouch.canceled += StoppedCrouching;
        _playerInput.Player2.Fly.performed += Fly;
        _playerInput.Player2.Fly.canceled += StoppedFlying;
        // Player 2 does not have a pause function. If both players pause, 2 pause menus will be instantiated.
    }

    private void OnDisable()
    {
        _playerInput.Player2.HorizontalMovement.performed -= Move;
        _playerInput.Player2.HorizontalMovement.canceled -= StoppedMoving;
        _playerInput.Player2.Crouch.performed -= Crouch;
        _playerInput.Player2.Crouch.canceled -= StoppedCrouching;
        _playerInput.Player2.Fly.performed -= Fly;
        _playerInput.Player2.Fly.canceled -= StoppedFlying;
        _playerInput.Disable();
    }

    private void FixedUpdate() => UpdateLoop(_rb, _coll, 1);
}
