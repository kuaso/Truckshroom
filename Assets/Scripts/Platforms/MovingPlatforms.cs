using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforma : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

                collision.gameObject.transform.parent = transform;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
