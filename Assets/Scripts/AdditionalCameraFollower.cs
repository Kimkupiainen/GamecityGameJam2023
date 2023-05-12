using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalCameraFollower : MonoBehaviour
{
    [SerializeField]Transform bol;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(bol);
    }
}
