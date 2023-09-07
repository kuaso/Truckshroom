using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforma : MonoBehaviour
{
    private List<GameObject> _gameObjectsToAlter = new();
    private Animation _animation;
    private float _pastXPosition;
    private float _pastMovedXAmount;

    private void OnEnable()
    {
        _animation = GetComponent<Animation>();
        _pastXPosition = transform.position.x;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _gameObjectsToAlter.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _gameObjectsToAlter.Remove(collision.gameObject);
        }
    }

    private void FixedUpdate()
    {
        // We want moving left to be negative and moving right to be positive
        // e.g. -1f * (5f - 2f) = -3f
        // e.g. -1f * (2f - 5f) = 3f
        // e.g. -1f * (1f - -1f) = -2f
        var movedXPosition = -1f * (transform.position.x - _pastXPosition);
        foreach (var gameObjectToAlter in _gameObjectsToAlter)
        {
            // Is it being removed too fast? The player straight up isn't moving
            gameObjectToAlter.transform.position -= new Vector3(_pastMovedXAmount, 0f, 0f);
            gameObjectToAlter.transform.position += new Vector3(movedXPosition, 0f, 0f);
        }
        _pastMovedXAmount = movedXPosition;
    }
}