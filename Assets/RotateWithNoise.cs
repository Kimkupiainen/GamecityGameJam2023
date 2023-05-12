using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithNoise : MonoBehaviour
{
    public float strengthMultiplier = 31.0f;
    public float noiseScale = 1.0f;
    public float noiseSpeed = 0.2f;
    public float offsetMultiplier = 1.0f;

    private Vector3 initialRotation;

    void Start()
    {
        initialRotation = transform.rotation.eulerAngles;
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(Time.time * noiseSpeed, 0.0f);
        float remappedNoise = Mathf.Lerp(-1.0f, 1.0f, noise * 2.0f - 1.0f);
        Vector3 offset = transform.position * offsetMultiplier;
        Vector3 rotation = initialRotation + new Vector3(0.0f,0.0f+offset.x, offset.z) + new Vector3(0.0f, noise * strengthMultiplier * noiseScale, 0.0f);
        transform.rotation = Quaternion.Euler(rotation);
    }
}