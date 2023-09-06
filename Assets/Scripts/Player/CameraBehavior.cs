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
        float targetOneX = playerOne.position.x;
        float targetTwoX = playerTwo.position.x;

        if (targetOneX > targetTwoX)
        {
            targetOneX = playerOne.position.x + 1.5f;
            targetTwoX = playerTwo.position.x - 1.5f;
        }
        else
        {
            targetOneX = playerOne.position.x - 1.5f;
            targetTwoX = playerTwo.position.x + 1.5f;
        }
        
        targetOneX = Mathf.Clamp(targetOneX, minValues.x, maxValues.x);
        targetTwoX = Mathf.Clamp(targetTwoX, minValues.x, maxValues.x);

        float targetOneY = playerOne.position.x;
        float targetTwoY = playerTwo.position.x;

        if (targetOneY > targetTwoY)
        {
            targetOneY = playerOne.position.y + 1.5f;
            targetTwoY = playerTwo.position.y - 1.5f;
        }
        else
        {
            targetOneY = playerOne.position.y - 1.5f;
            targetTwoY = playerTwo.position.y + 1.5f;
        }

        targetOneY = Mathf.Clamp(targetOneY, minValues.y, maxValues.y);
        targetTwoY = Mathf.Clamp(targetTwoY, minValues.y, maxValues.y);


        float[] positions = { targetOneX, targetOneY, targetTwoX, targetTwoY };
        return positions;
    }

    private float zoom(float x, float y)
    {
        float xZoom = x/2;
        float yZoom = y/2;

        float z = Mathf.Max(xZoom, yZoom);

        // Debug.Log("x: " + xZoom);
        // Debug.Log("y: " + yZoom);
        // Debug.Log("z: " + z);
        z = Mathf.Clamp(z, minZoom, maxZoom);
        return z;
    }
}
