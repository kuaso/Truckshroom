using UnityEngine;

public class Player1InputScript : BasePlayerInputScript
{
    private const int PlayerNumber = 0;
    private PlayerInput _playerInput;
    private Rigidbody2D _rb;

    public Player1InputScript() : base(PlayerNumber)
    {
    }

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.Player1.HorizontalMovement.performed += Move;
        _playerInput.Player1.HorizontalMovement.canceled += StoppedMoving;
        _playerInput.Player1.Crouch.performed += Crouch;
        _playerInput.Player1.Crouch.canceled += StoppedCrouching;
        _playerInput.Player1.Fly.performed += Fly;
        _playerInput.Player1.Fly.canceled += StoppedFlying;
        _playerInput.Player1.Pause.performed += Pause;
    }

    private void OnDisable()
    {
        _playerInput.Player1.HorizontalMovement.performed -= Move;
        _playerInput.Player1.HorizontalMovement.canceled -= StoppedMoving;
        _playerInput.Player1.Crouch.performed -= Crouch;
        _playerInput.Player1.Crouch.canceled -= StoppedCrouching;
        _playerInput.Player1.Fly.performed -= Fly;
        _playerInput.Player1.Fly.canceled -= StoppedFlying;
        _playerInput.Player1.Pause.performed -= Pause;
        _playerInput.Disable();
    }

    private void FixedUpdate() => UpdateLoop(_rb);
}