using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Image missileFill;

    private void Update()
    {
        missileFill.fillAmount = Time.time % 1.0f;
    }
}
