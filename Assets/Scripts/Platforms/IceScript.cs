using System.Collections.Generic;
using UnityEngine;

public class IceScript : MonoBehaviour
{
    private List<GameObject> _gameObjectsToAlter = new();
    private float _pastXPosition;

    private void OnEnable()
    {
        _pastXPosition = transform.position.x;
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player");
            Debug.Log("Player on platform");
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
            gameObjectToAlter.transform.position += new Vector3(movedXPosition, 0f, 0f);
        }
    }
}