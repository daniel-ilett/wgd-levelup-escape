using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Image missileFill;

    [SerializeField]
    private Text missileText;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text timeText;

    private int score = 0;
    private float time = 120.0f;

    private string message = "Missile ready!";

    [SerializeField]
    private PlayerMech player;

    public static PlayerUI instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        missileFill.fillAmount = player.GetMissileTime() / PlayerMech.missileCooldown;
        missileText.text = (missileFill.fillAmount >= 1.0f) ? message : string.Empty;

        time -= Time.deltaTime;
        timeText.text = (int)time + "s remaining";

        if(time <= 0.0f)
        {
            GameController.instance.EndGame();
        }
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Caused $" + score + " damage to the capitalist dogs";
    }
}
