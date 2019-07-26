using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel(string level_name)
    {
        SceneManager.LoadScene(level_name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
