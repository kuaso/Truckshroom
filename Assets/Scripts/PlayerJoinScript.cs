using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{
    private PlayerInput _playerInput;
    private readonly bool[] _spawned = new bool[2];
    [SerializeField] private GameObject player1Prefab;
    [SerializeField] private GameObject player2Prefab;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.JoinPlayer1.performed += JoinPlayer1;
        _playerInput.Player.JoinPlayer2.performed += JoinPlayer2;
    }

    private void OnDisable()
    {
        _playerInput.Player.JoinPlayer1.performed -= JoinPlayer1;
        _playerInput.Player.JoinPlayer2.performed -= JoinPlayer2;
        _playerInput.Disable();
    }

    private void JoinPlayer1(InputAction.CallbackContext ctx)
    {
        if (_spawned[0])
        {
            return;
        }

        _spawned[0] = true;
        UnityEngine.InputSystem.PlayerInput.Instantiate(player1Prefab, controlScheme: "Keyboard Left", playerIndex: 0, pairWithDevice: Keyboard.current);
        Debug.Log($"Player 1 joined"); // Array is 0 indexed, but this is really player 1
    }

    private void JoinPlayer2(InputAction.CallbackContext ctx)
    {
        if (_spawned[1])
        {
            return;
        }

        _spawned[1] = true;
        UnityEngine.InputSystem.PlayerInput.Instantiate(player2Prefab, controlScheme: "Keyboard Right", playerIndex: 1, pairWithDevice: Keyboard.current);
        Debug.Log($"Player 2 joined");
    }
}