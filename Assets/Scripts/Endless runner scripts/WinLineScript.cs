using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLineScript : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
    }
}
