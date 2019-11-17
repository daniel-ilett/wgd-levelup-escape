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

    private int score = 0;

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
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Siezed " + score + " means of production";
    }
}
