using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocksGenerator : MonoBehaviour
{
    //Create a rock object every something seconds

    [SerializeField] float waitSeconds;
    [SerializeField] GameObject rockPrefab;

    void Start()
    {
        IEnumerator coroutine = SpawnCoroutine(waitSeconds);
        StartCoroutine(coroutine);
        
    }

    IEnumerator SpawnCoroutine(float waitSeconds)
    {
        while (true)
        {
            
            GameObject rock = Instantiate(rockPrefab, transform) as GameObject;
            rock.transform.SetParent(transform, true);
            Destroy(rock, 15); //might want to change this to a certain position instead of time ltr
            yield return new WaitForSeconds(waitSeconds);
            
        }
    }
}
