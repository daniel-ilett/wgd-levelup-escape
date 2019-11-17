using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text rankText;

    private void Awake()
    {
        scoreText.text = "You caused $" + PlayerUI.score + " damage to the capitalists.";

        if(PlayerUI.score > 100)
        {
            rankText.text = "Well done. Capitalism can't recover from this. The motherland is proud of your skills.";
        }
        else if(PlayerUI.score > 50)
        {
            rankText.text = "Good effort. $" + (100 - PlayerUI.score) + " more damage would have overthrown the corporations.";
        }
        else
        {
            rankText.text = "You didn't destroy much. The billionaires will party tonight.";
        }
    }
}
