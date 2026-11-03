using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController Instance;
    protected bool gameOver = false;
    public GameObject DieInterface;
    public AudioSource BGM;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        DieInterface.SetActive(false);
        BGM.loop = true;//设置声音为循环播放
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == true)
        {
            BGM.Stop();
            DieInterface.SetActive(true);
        }
    }

    public void game()
    {
        gameOver = true;
    }

    public void gameState()
    {
        gameOver = false;
    }

    public bool over()
    {
        return gameOver;
    }
}


