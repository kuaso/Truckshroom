using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] Transform playerOne;
    [SerializeField] Transform playerTwo;
    [SerializeField] public Vector3 minValues, maxValues;

    // Update is called once per frame
    void Update()
    {
        float targetOneX = playerOne.position.x + 1.5f;
        float targetTwoX = playerTwo.position.x + 1.5f;
        if (targetOneX < minValues.x)
        {
            targetOneX = minValues.x;
        }
        else if (targetOneX > maxValues.x)
        {
            targetOneX = maxValues.x;
        }

        if (targetTwoX < minValues.x)
        {
            targetTwoX = minValues.x;
        }
        else if (targetTwoX > maxValues.x)
        {
            targetTwoX = maxValues.x;
        }


        float targetOneY = playerOne.position.y + 1.5f;
        float targetTwoY = playerTwo.position.y + 1.5f;
        if (targetOneY < minValues.y)
        {
            targetOneY = minValues.y;
        }
        else if (targetOneY > maxValues.y)
        {
            targetOneY = maxValues.y;
        }

        if (targetTwoY < minValues.y)
        {
            targetTwoY = minValues.y;
        }
        else if (targetTwoY > maxValues.y)
        {
            targetTwoY = maxValues.y;
        }
        transform.position = new Vector3(targetX, targetY, transform.position.z);

    }
}
