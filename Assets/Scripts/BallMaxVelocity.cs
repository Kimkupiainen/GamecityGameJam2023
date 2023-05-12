using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMaxVelocity : MonoBehaviour
{
    public Rigidbody2D rb;
    public float launchForce = 10f;    
    public float velocityDamping = 0.95f;
    public float gravityScale = 1f;
    public ParticleSystem prt;
    private AudioSource ads;
    public AudioClip[] audioClips;

    public Vector3 startposition;
    // Start is called before the first frame update
    void Start()
    {
        startposition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine( LaunchBall(1f));
        ads = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Apply custom gravity scale
        rb.gravityScale = gravityScale;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direction = collision.GetContact(0).normal;
            Vector2 launchVelocity = direction * launchForce;

            rb.velocity = launchVelocity;
            prt.Play();
            ads.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
            
        }
        rb.velocity *= velocityDamping;
    }
    public IEnumerator LaunchBall(float delay)
    {
        transform.position = startposition;
        rb.velocity = Vector2.zero;
        rb.simulated = false;
        yield return new WaitForSeconds(delay);
        rb.simulated = true;
        //Vector2 randomDirection = Random.insideUnitCircle.normalized;
        //float angle = Vector2.Angle(Vector2.up, randomDirection);

        float angle = Random.Range(-15, 15);

        Vector2 randomDirection = new Vector2(Mathf.Sin(angle), -Mathf.Cos(angle)).normalized;

        print(angle);

        print(randomDirection);
        //if (angle < 110f)
        //{
        //    randomDirection = Quaternion.Euler(0f, 0f, 110f) * Vector2.up;
        //}

        //Vector2 launchVelocity = randomDirection * launchForce;

        rb.velocity = randomDirection * launchForce;
    }
}
