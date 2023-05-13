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
    [SerializeField] ActionReplay actionReplay;

    int score1;
    int score2;
    int maxscore = 3;
    bool gameover;
    // Start is called before the first frame update
    void Start()
    {
        score1 = 0;
        score2 = 0;
        whowontext.text = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ball"&&!gameover)
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
            if (score1 == maxscore||score2==maxscore)
            {
                gameover = true;
                if (score1 > score2)
                {
                    cmr.DropExtras(1);
                    whowontext.text = "Player 1 wins!";
                    audience.ShakeLeft();
                    Destroy(ball);
                    score1text.text = null;
                    score2text.text = null;

                }
                else
                {
                    cmr.DropExtras(2);
                    audience.ShakeRight();
                    whowontext.text = "Player 2 wins!";
                    Destroy(ball);
                    score1text.text = null;
                    score2text.text = null;
                }

            }
            if (!gameover)
            {
                if (ball != null) StartCoroutine(ball.LaunchBall(1.5f));
            }
        }
    }


}
