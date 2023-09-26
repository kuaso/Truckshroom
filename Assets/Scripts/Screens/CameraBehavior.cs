using System;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private Camera c;
    [SerializeField] public Vector3 minValues, maxValues;
    [SerializeField] private float minZoom, maxZoom;

    private Transform playerOne;
    private Transform playerTwo;
    
    private void Awake()
    {
        playerOne = GameObject.Find("Player1").transform;
        playerTwo = GameObject.Find("Player2").transform;
    }

    // Update is called once per frame
    void Update()
    {
        var positions = FindPosition();
        var targetOneX = positions[0];
        var targetOneY = positions[1];
        var targetTwoX = positions[2];
        var targetTwoY = positions[3];

        var targetX = (targetOneX + targetTwoX) / 2;
        var targetY = (targetOneY + targetTwoY) / 2;
        var cameraTransform = transform;
        

        var xDistance = Mathf.Abs(targetOneX - targetTwoX);
        var yDistance = Mathf.Abs(targetOneY - targetTwoY);
        var z = Zoom(xDistance, yDistance);
        cameraTransform.position = new Vector3(targetX, targetY, cameraTransform.position.z);

        c.orthographicSize = z;
    }

    private float[] FindPosition()
    {
        var targetOneX = playerOne.position.x;
        var targetTwoX = playerTwo.position.x;

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

        var targetOneY = playerOne.position.y;
        var targetTwoY = playerTwo.position.y;

        if (targetOneY > targetTwoY)
        {
            targetOneY = playerOne.position.y + 10f;
            targetTwoY = playerTwo.position.y - 5f;
        }
        else
        {
            targetOneY = playerOne.position.y - 5f;
            targetTwoY = playerTwo.position.y + 10f;
        }

        targetOneY = Mathf.Clamp(targetOneY, minValues.y, maxValues.y);
        targetTwoY = Mathf.Clamp(targetTwoY, minValues.y, maxValues.y);


        float[] positions = { targetOneX, targetOneY, targetTwoX, targetTwoY };
        return positions;
    }

    private float Zoom(float x, float y)
    {
        var xZoom = x / 2;
        var yZoom = y / 2;

        var z = Mathf.Max(xZoom, yZoom);

        // Debug.Log("x: " + xZoom);
        // Debug.Log("y: " + yZoom);
        // Debug.Log("z: " + z);
        z = Mathf.Clamp(z, minZoom, maxZoom);
        return z;
    }
}