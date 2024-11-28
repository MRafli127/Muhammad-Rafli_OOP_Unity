using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundaries : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    private Vector2 boundaryLimits;
    private float objWidth;
    private float objHeight;

    private void Start()
    {
        CalculateScreenBoundaries();
    }

    private void LateUpdate()
    {
        ConstrainObjectPosition();
    }

    private void CalculateScreenBoundaries()
    {
        if (playerCamera != null)
        {
            boundaryLimits = playerCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, playerCamera.transform.position.z));
        }
    }

    private void ConstrainObjectPosition()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, -boundaryLimits.x, boundaryLimits.x);
        currentPosition.y = Mathf.Clamp(currentPosition.y, -boundaryLimits.y, boundaryLimits.y);
        transform.position = currentPosition;
    }
}
