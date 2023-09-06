using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforma : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //need to figure out how much update animation according to this
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
        }
    }
}
