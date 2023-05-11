using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMaxVelocity : MonoBehaviour
{
    public Rigidbody2D rb;
    public float launchForce = 10f;    
    public float velocityDamping = 0.95f;
    public float gravityScale = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LaunchBall();
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
        }
        rb.velocity *= velocityDamping;
    }
    private void LaunchBall()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float angle = Vector2.Angle(Vector2.up, randomDirection);

        if (angle < 110f)
        {
            randomDirection = Quaternion.Euler(0f, 0f, 110f) * Vector2.up;
        }

        Vector2 launchVelocity = randomDirection * launchForce;

        rb.velocity = -launchVelocity;
    }
}
