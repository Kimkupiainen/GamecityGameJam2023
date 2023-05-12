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

    private void Awake()
    {
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
        var _spikeInstance = Instantiate(spike, transform.position, transform.rotation);

        _spikeInstance.GetComponent<SpikeScript>().spikeGenerator = this;
    }

    private void Update()
    {
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += speedMultiplier;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(2);
        }
    }
}
