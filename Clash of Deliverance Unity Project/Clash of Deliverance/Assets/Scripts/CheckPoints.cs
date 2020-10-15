using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{

    public GameObject[] haloSprite;
    public GameObject[] greySprite;

    [SerializeField]
    [Range(0, 4)]
    private int max_check_point;
    [SerializeField]
    [Range(0, 4)]
    private int checkpoint_reached;
    [SerializeField]
    [Range(0, 4)]
    private int checkpoint_bought;

    public static bool can_update_checkpoint;


    void Awake()
    {
        UpdateCheckpoints();

    }

    void Update()
    {
        if(can_update_checkpoint)
        {
            UpdateCheckpoints();
            can_update_checkpoint = false;
        }
    }


    void UpdateCheckpoints()
    {
        // Show checkpoints reached
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("Checkpoint", checkpoint_reached);
        checkpoint_reached = PlayerPrefs.GetInt("Checkpoint", 0);

        int my_point = checkpoint_reached;

        if (my_point > 0)
        {
            for (int j = max_check_point; j > 0; j--)
            {
                haloSprite[--my_point].SetActive(true);

                if (my_point <= 0)
                    j = 0;
            }
        }
        else
            foreach (var item in haloSprite)
                item.SetActive(false);


        // Show checkpoints bought
        //PlayerPrefs.SetInt("CheckpointBought", checkpoint_bought);
        checkpoint_bought = PlayerPrefs.GetInt("CheckpointBought", 0);

        int my_point_bought = checkpoint_bought;

        if (my_point_bought > 0)
        {
            for (int j = max_check_point; j > 0; j--)
            {
                greySprite[--my_point_bought].SetActive(false);

                if (my_point_bought <= 0)
                    j = 0;
            }
        }
        else
            foreach (var item in greySprite)
                item.SetActive(true);
    }
}
