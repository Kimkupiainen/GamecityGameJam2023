using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    [Header("WinLine")]
    public float time;
    public float timeThreshold;

    public WinLineScript winLine;
    
    [Header("Jump")]
    public float jumpforce;

    public bool IsGrounded = false;
    public bool IsAlive = true;

    public Rigidbody rb;

    [Header("Score")]
    public float score;
    public float scoreMultiplier;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI WinText;

    [Header("Movement")]
    public float moveInputX;
    public float moveInputZ;
    public float speed;

    private void Awake()
    {
        winLine = GameObject.Find("WinLine").GetComponent<WinLineScript>();
    }
    private void Start()
    {
        WinText.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
        score = 0;
    }

    private void Update()
    {
       
        moveInputX = Input.GetAxisRaw("Horizontal1");
        moveInputZ = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(moveInputX * speed, rb.velocity.y, moveInputZ * speed);

        time += Time.deltaTime;
        if (time > timeThreshold)
        {
            winLine.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (IsGrounded == true)
            {
                rb.AddForce(Vector2.up * jumpforce);
                IsGrounded = false;
            }
        }

        if (IsAlive)
        {
            score += Time.deltaTime * scoreMultiplier;
            int scr = (int)score;
            ScoreText.text = "Score : " + scr.ToString();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            if (IsGrounded == false)
            {
                IsGrounded = true;
            }
        }
        if (collision.gameObject.CompareTag("mörkö"))
        {
            Destroy(gameObject);
            Time.timeScale = 0;
        }
        if (collision.gameObject.CompareTag("Winnn"))
        {
            WinText.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("lineOne"))
        {
            scoreMultiplier = 1.0f;
        }
        if (other.gameObject.CompareTag("lineTwo"))
        {
            scoreMultiplier = 1.35f;
        }
        if (other.gameObject.CompareTag("lineThree"))
        {
            scoreMultiplier = 1.55f;
        }
        if (other.gameObject.CompareTag("lineFour"))
        {
            scoreMultiplier = 2.0f;
        }

    }
}
