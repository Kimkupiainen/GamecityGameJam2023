using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creditsroll : MonoBehaviour
{
    public float targetY;      // The target Y position where you want the object to stop
    public float speed;        // The speed at which the object moves

    private bool isMoving;     // Flag to track if the object is currently moving
    public float startY;
    private void Start()
    {
        startY = gameObject.transform.position.y;
    }
    private void Update()
    {
        if (isMoving)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);

            if (transform.position.y >= targetY)
            {
                isMoving = false;
                transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
            }
        }
    }

    public void StartMoving()
    {
        // Begin moving the object
        isMoving = true;
    }
    public void resetPosition()
    {
        isMoving = false;
        transform.position = new Vector3(transform.position.x, startY,transform.position.z);
    }
}
