using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeGenerator : MonoBehaviour
{
    public GameObject spike;

    public float minSpeed;
    public float maxSpeed;

    public float currentSpeed;

    public float speedMultiplier;

    public float minDelay;
    public float maxDelay;

    public int spikeThreshold = 200;
    private static int totalSpikeCount;
    
    private void Start()
    {
        totalSpikeCount = 0;
        currentSpeed = minSpeed;
        GenerateSpike();
    }

    public void GenerateSpikeWithDelay()
    {
        float wait = Random.Range(minDelay, maxDelay);
        Invoke("GenerateSpike", wait);
    }
    private void GenerateSpike()
    {
        if (totalSpikeCount < spikeThreshold)
        {
            var _spikeInstance = Instantiate(spike, transform.position, transform.rotation);
            _spikeInstance.GetComponent<SpikeScript>().spikeGenerator = this;

            totalSpikeCount++;
        }
    }

    private void Update()
    {
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += speedMultiplier;
        }
    }
}
