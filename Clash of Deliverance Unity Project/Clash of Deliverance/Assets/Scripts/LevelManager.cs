using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    Scene scene;
    // Start is called before the first frame update
    void Start()
    {
       scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if(scene.name == "WaitingPlayer" && Input.GetKey("space"))
        {
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
}
