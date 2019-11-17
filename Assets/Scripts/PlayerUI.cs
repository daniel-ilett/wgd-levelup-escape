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

    private string message = "Missile ready!";

    [SerializeField]
    private PlayerMech player;

    private void Update()
    {
        missileFill.fillAmount = player.GetMissileTime() / PlayerMech.missileCooldown;
        missileText.text = (missileFill.fillAmount >= 1.0f) ? message : string.Empty;
    }
}
