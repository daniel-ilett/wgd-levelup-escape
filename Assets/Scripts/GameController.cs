using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private CameraController cam;

    [SerializeField]
    private PlayerMech player;

    public static GameController instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player.enabled = false;

        Invoke("StartGame", 3.0f);
    }

    private void StartGame()
    {
        player.enabled = true;
        cam.StartGame();
    }

    public void EndGame()
    {
        SceneManager.LoadScene("GameOver");
    }
}
