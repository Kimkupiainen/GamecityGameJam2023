using UnityEngine;
using System.Collections;

public class AudienceWiggler : MonoBehaviour
{
    public float shakeAmount = 0.1f;
    public float shakeDuration = 0.5f;
    public AudioClip shakeSound;

    public GameObject leftSide;
    public GameObject rightSide;

    private Vector3 leftOriginalPosition;
    private Vector3 rightOriginalPosition;

    private void Start()
    {
        leftOriginalPosition = leftSide.transform.position;
        rightOriginalPosition = rightSide.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Shake(leftSide);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Shake(rightSide);
        }
    }

    public void Shake(GameObject obj)
    {
        StartCoroutine(ShakeCoroutine(obj));
        if (shakeSound != null)
        {
            AudioSource.PlayClipAtPoint(shakeSound, obj.transform.position);
        }
    }

    private IEnumerator ShakeCoroutine(GameObject obj)
    {
        Vector3 originalPosition = obj.transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            float x = originalPosition.x + Random.Range(-shakeAmount, shakeAmount);
            float y = originalPosition.y + Random.Range(-shakeAmount, shakeAmount);
            float z = originalPosition.z + Random.Range(-shakeAmount, shakeAmount);
            obj.transform.position = new Vector3(x, y, z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.transform.position = originalPosition;
    }
}