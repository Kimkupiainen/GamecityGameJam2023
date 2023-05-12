using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public SpikeGenerator spikeGenerator;

    private void Update()
    {
        transform.Translate(Vector3.back * spikeGenerator.currentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("nextLine"))
        {
            spikeGenerator.GenerateSpikeWithDelay();
        }

        if (other.gameObject.CompareTag("finnish"))
        {
            Destroy(gameObject);
        }
    }
}
