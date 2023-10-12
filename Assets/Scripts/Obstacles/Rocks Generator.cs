using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocksGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float waitSeconds;
    [SerializeField] GameObject rockPrefab;


    // Update is called once per frame
    void Start()
    {
        //start coroutine TODO
    }

    IEnumerator SpawnCoroutine(float waitSeconds)
    {
        GameObject rock = Instantiate(rockPrefab) as GameObject;
        yield return new WaitForSeconds(waitSeconds);
    }
}
