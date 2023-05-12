using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class goal : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI score1text;

    [SerializeField]
    TextMeshProUGUI score2text;

    int score1;
    int score2;
    // Start is called before the first frame update
    void Start()
    {
        score1 = 0;
        score2 = 0;

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ball")
        {
            if (this.name == "goal1")
            {
                score2++;
                score2text.text = score2.ToString();
            }
            else
            {
                score1++;
                score1text.text = score1.ToString();
            }
        }
    }
}
