using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class goal : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI score1text;
    [SerializeField]
    CameraController cmr;

    [SerializeField]
    TextMeshProUGUI score2text;

    [SerializeField]
    TextMeshProUGUI whowontext;

    [SerializeField]
    AudienceWiggler audience;
    [SerializeField]
    ParticleSystem[] prt;

    int score1;
    int score2;
    int maxscore = 3;
    // Start is called before the first frame update
    void Start()
    {
        score1 = 0;
        score2 = 0;
        whowontext.text = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ball")
        {
            if (this.name == "goal1")
            {
                score2++;
                score2text.text = score2.ToString();
                audience.ShakeRight();
                prt[0].Play();
            }
            else
            {
                score1++;
                score1text.text = score1.ToString();
                audience.ShakeLeft();
                prt[1].Play();
            }
            BallMaxVelocity ball = collision.GetComponent<BallMaxVelocity>();
            if (score1 == maxscore)
            {
                cmr.DropExtras(1);
                whowontext.text = "Player 1 wins!";
                Destroy(ball);
            }
            if (score2 == maxscore)
            {
                cmr.DropExtras(2);
                whowontext.text = "player 2 wins!";
                Destroy(ball);
            }
            if (ball != null) StartCoroutine( ball.LaunchBall(1.5f));
        }
    }


}
