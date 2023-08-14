using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    private CustomInput _input;
    private Rigidbody2D _rb;
    private Vector2 _movement;

    public bool isMovementEnabled = true;
    [SerializeField] private float velocityMultiplierX = 5f;

    private void Awake()
    {
        _input = new CustomInput();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Movement.performed += OnMovement;
        _input.Player.Movement.canceled += OnCancelled;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Movement.performed -= OnMovement;
        _input.Player.Movement.canceled -= OnCancelled;
    }

    private void FixedUpdate()
    {
        if (isMovementEnabled)
        {
            _rb.velocity = _movement * velocityMultiplierX;
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }

    private void OnMovement(InputAction.CallbackContext callbackContext)
    {
        _movement = callbackContext.ReadValue<Vector2>();
    }

    private void OnCancelled(InputAction.CallbackContext callbackContext)
    {
        _movement = Vector2.zero;
    }
}