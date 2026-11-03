using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DieUIController : MonoBehaviour
{
    public Button RestartButton;
    public Button EndButton;
    public AudioSource Player;
    // Start is called before the first frame update
    void Start()
    {
        Player.Play();
        Player.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitGame()
    {
        Debug.Log("exit");
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void Restart()
    {
        Debug.Log("end");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
