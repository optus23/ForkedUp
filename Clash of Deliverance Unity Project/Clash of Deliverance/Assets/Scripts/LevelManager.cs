using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    enum Panel_Stats
    {
        SHOP,
        CREDITS,
        RETURN_SHOP,
        RETURN_CREDITS,
        NONE
    }

    Scene scene;
    Touch touch;

    public GameObject panel;
    public float velocity_panel;
    public float offset_panel_shop;
    public float offset_panel_play;
    public float offset_panel_credits;

    private bool  moving_panel;

    private Panel_Stats panel_stats;
    

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

        if (scene.name == "Main Menu" && moving_panel)
        {
            MainMenuMovement();
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
    
    public void ShopButton()
    {
        moving_panel = true;
        panel_stats = Panel_Stats.SHOP;
    }
    public void ReturnShopButton()
    {
        moving_panel = true;
        panel_stats = Panel_Stats.RETURN_SHOP;

    }
    public void CreditsButton()
    {
        moving_panel = true;
        panel_stats = Panel_Stats.CREDITS;
    }
    public void ReturnCreditsButton()
    {
        moving_panel = true;
        panel_stats = Panel_Stats.RETURN_CREDITS;

    }



    void MainMenuMovement()
    {
        //  Go Shop
        if (Camera.main.transform.position.x <= offset_panel_shop && panel_stats == Panel_Stats.SHOP)
        {

            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + velocity_panel * Time.deltaTime, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
        //  Go Credits
        else if (Camera.main.transform.position.x >= offset_panel_credits && panel_stats == Panel_Stats.CREDITS)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - velocity_panel * Time.deltaTime, Camera.main.transform.position.y, Camera.main.transform.position.z);

        }
        //  Return  Shop
        else if (Camera.main.transform.position.x >= offset_panel_play && panel_stats == Panel_Stats.RETURN_SHOP)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - velocity_panel * Time.deltaTime, Camera.main.transform.position.y, Camera.main.transform.position.z);

        }
        //  Return  Credits
        else if (Camera.main.transform.position.x <= offset_panel_play && panel_stats == Panel_Stats.RETURN_CREDITS)
        {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + velocity_panel * Time.deltaTime, Camera.main.transform.position.y, Camera.main.transform.position.z);

        }
        else moving_panel = false;
    }


    
}
