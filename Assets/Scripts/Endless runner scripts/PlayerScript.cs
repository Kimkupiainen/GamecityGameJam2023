using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public float time;
    public float timeThreshold;

    public WinLineScript winLine;
    
    public bool IsAlive = true;

    public Rigidbody rb;

    [Header("Score")]
    public float score;
    public float scoreMultiplier;
    public float endscore;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI WinText;
    public TextMeshProUGUI LoseText;
    [SerializeField] AudioClip[] clp;
    AudioSource src;

    [Header("Movement")]
    // public float moveInputX;
    public float moveInputZ;
    public float speed;

    public int desiredLane; //0 = left, 1 = middle, 2 = right
    public float laneDistance = 3f;
    bool wongame;
    CharacterController cr;

    private void Awake()
    {
        src=GetComponent<AudioSource>();
        cr=GetComponent<CharacterController>();
    }
    private void Start()
    {
        wongame = false;
        WinText.gameObject.SetActive(false);
        LoseText.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
        score = 0;
    }
    void PlayerMovement()
    {
        moveInputZ = Input.GetAxisRaw("Vertical");

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
        moveVector.x = (targetPosition - transform.position).normalized.x * (speed / 2);
        moveVector.z = moveInputZ;

        cr.Move(moveVector * Time.deltaTime * speed);

    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time > timeThreshold)
        {
            winLine.gameObject.SetActive(true);
        }

        if (IsAlive&&!wongame)
        {
            score += Time.deltaTime * scoreMultiplier;
            int scr = (int)score;
            ScoreText.text = "Score : " + scr.ToString();
            PlayerMovement();
        }
    }

    private void MoveLane(bool goingRight)
    {
        if (!goingRight)
        {
            desiredLane--;
            transform.eulerAngles = new Vector3(0, 180, 0);
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }
        else
        {
            desiredLane++;
            transform.eulerAngles = new Vector3(0, 0, 0);
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsAlive && !wongame)
        {
            if (other.gameObject.CompareTag("lineOne"))
            {
                scoreMultiplier = 2.5f;
            }
            else if (other.gameObject.CompareTag("lineTwo"))
            {
                scoreMultiplier = 3f;
            }
            else if (other.gameObject.CompareTag("lineThree"))
            {
                scoreMultiplier = 4.5f;
            }
            else if (other.gameObject.CompareTag("lineFour"))
            {
                scoreMultiplier = 7f;
            }
            else
            {
                scoreMultiplier = 2;
            }
        }

        if (other.gameObject.CompareTag("mörkö")&&!wongame)
        {
            endscore = score;
            src.PlayOneShot(clp[Random.Range(0, clp.Length)]);
            cr.enabled = false;
            IsAlive = false;
            LoseText.gameObject.SetActive(true);
            rb.constraints = RigidbodyConstraints.None;
            rb.isKinematic = false;

            Vector3 explosionPosition = other.transform.position;
            float explosionRadius = 10f; // Adjust the explosion radius as needed
            float explosionForce = 500f; // Adjust the explosion force as needed

            rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);

        }
        if (other.gameObject.CompareTag("Winnn") && IsAlive)
        {
            wongame = true;
            WinText.gameObject.SetActive(true);
            cr.enabled = false;

        }
    }
}
