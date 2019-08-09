using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    Scene scene;
    Touch touch;

    // Start is called before the first frame update
    void Start()
    {
       scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (scene.name == "WaitingPlayer" && ((Input.GetKey("space") || Input.touchCount > 0)))
        {
            if (Input.touchCount > 0)
                touch = Input.GetTouch(0);
            TouchToJump();
        }
    }

    public void StartLevel(string level_name)
    {
        SceneManager.LoadScene(level_name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void TouchToJump()
    {
        SceneManager.LoadScene("Clash of Deliverance");
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ReturnWaitinfPlayer()
    {
        SceneManager.LoadScene("WaitingPlayer");
        Player_Jump.dead = false;
    }

    
}
