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
    // public float moveInputX;
    public float moveInputZ;
    public float speed;

    public int desiredLane; //0 = left, 1 = middle, 2 = right
    public float laneDistance = 3f;

    CharacterController cr;

    private void Awake()
    {
        cr = GetComponent<CharacterController>();
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
        time += Time.deltaTime;
        if (time > timeThreshold)
        {
            winLine.gameObject.SetActive(true);
        }


        //        moveInputX = Input.GetAxisRaw("Horizontal1");
        moveInputZ = Input.GetAxisRaw("Vertical");
        //rb.velocity = new Vector3(0, rb.velocity.y, moveInputZ * speed);

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            MoveLane(false);
        } 
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            MoveLane(true);
        }

        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * (speed/2);
        moveVector.z = moveInputZ;

        cr.Move(moveVector * Time.deltaTime * speed);
        //if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    if (IsGrounded == true)
        //    {
        //        rb.AddForce(Vector2.up * jumpforce);
        //        IsGrounded = false;
        //    }
        //}

        if (IsAlive)
        {
            score += Time.deltaTime * scoreMultiplier;
            int scr = (int)score;
            ScoreText.text = "Score : " + scr.ToString();
        }
    }

    private void MoveLane(bool goingRight)
    {
        if (!goingRight)
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }
        else
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
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
        if (collision.gameObject.CompareTag("m�rk�"))
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