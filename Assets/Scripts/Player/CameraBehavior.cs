using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] Transform playerOne;
    [SerializeField] Transform playerTwo;
    [SerializeField] Camera c;

    [SerializeField] public Vector3 minValues, maxValues;
    [SerializeField] float minZoom, maxZoom;

    // Update is called once per frame
    void Update()
    {
        float[] positions = findPosition();
        float targetOneX = positions[0];
        float targetOneY = positions[1];
        float targetTwoX = positions[2];
        float targetTwoY = positions[3];

        float targetX = (targetOneX + targetTwoX) / 2;
        float targetY = (targetOneY + targetTwoY) / 2;
        transform.position = new Vector3(targetX, targetY, transform.position.z);

        float xDistance = Mathf.Abs(targetOneX - targetTwoX);
        float yDistance = Mathf.Abs(targetOneY - targetTwoY);
        float z = zoom(xDistance, yDistance);

        c.orthographicSize = z;
    }

    private float[] findPosition()
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

        float[] positions = { targetOneX, targetOneY, targetTwoX, targetTwoY };
        return positions;
    }

    private float zoom(float x, float y)
    {
        float xZoom = x / 200;
        float yZoom = y / 200;

        float z = xZoom;
        if (9 * xZoom > 16 * yZoom)
        {
            // float z = yZoom; TEMPORARILY COMMENTED OUT TO ALLOW COMPILATION
        }

        return -1f; // THIS NEEDS TO BE FIXED! TEMP FIX TO ALLOW COMPILATION
    }
}
