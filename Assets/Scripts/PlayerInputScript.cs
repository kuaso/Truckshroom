using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    private InputActionMap _inputActionMap;
    private Rigidbody2D _rb;
    private GameObject _player;
    private float _horizontalMovement;
    private float _verticalMovement;

    [SerializeField] private float horizontalMultiplier = 15f;
    [SerializeField] private float verticalMultiplier = 20f;
    [SerializeField] private float gravity = 9.81f;

    private void Awake()
    {
        var inputActionAsset = GetComponent<UnityEngine.InputSystem.PlayerInput>().actions;
        _inputActionMap = inputActionAsset.FindActionMap("Player");
        _rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<GameObject>();
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
    }

    private void OnDisable()
    {
        _inputActionMap.FindAction("Horizontal Movement").performed -= Move;
        _inputActionMap.FindAction("Horizontal Movement").canceled -= StoppedMoving;
        _inputActionMap.FindAction("Crouch").performed -= Crouch;
        _inputActionMap.FindAction("Crouch").canceled -= StoppedCrouching;
        _inputActionMap.FindAction("Fly").performed -= Fly;
        _inputActionMap.FindAction("Fly").canceled -= StoppedFlying;
        _inputActionMap.Disable();
    }

    private void Update()
    {
        _rb.velocity = new Vector2(_horizontalMovement, _verticalMovement - gravity);
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
}