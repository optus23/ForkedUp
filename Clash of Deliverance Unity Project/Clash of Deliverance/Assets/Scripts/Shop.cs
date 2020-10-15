using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private int first_checkpoint_value;
    [SerializeField]
    private int second_checkpoint_value;
    [SerializeField]
    private int third_checkpoint_value;
    [SerializeField]
    private int fourth_checkpoint_value;

    bool first_checkpoint_bought;
    bool second_checkpoint_bought;
    bool third_checkpoint_bought;

    public Text ShopCheckpoint;

    void Start()
    {
    }

    void Update()
    {
    }

    public void UpdateCheckpoint()
    {
        CheckPoints.can_update_checkpoint = true;

        if (PlayerPrefs.GetInt("Checkpoint") >= 4 && Money.player_money_value >= fourth_checkpoint_value && third_checkpoint_bought)
        {
            PlayerPrefs.SetInt("CheckpointBought", 4);
            Money.player_money_value -= fourth_checkpoint_value;
            ShopCheckpoint.text = "MAX LEVEL";
        }
        else if (PlayerPrefs.GetInt("Checkpoint") >= 3 && Money.player_money_value >= third_checkpoint_value && second_checkpoint_bought)
        {
            PlayerPrefs.SetInt("CheckpointBought", 3);
            Money.player_money_value -= third_checkpoint_value;
            third_checkpoint_bought = true;
            ShopCheckpoint.text = fourth_checkpoint_value.ToString();
        }
        else if (PlayerPrefs.GetInt("Checkpoint") >= 2 && Money.player_money_value >= second_checkpoint_value && first_checkpoint_bought)
        {
            PlayerPrefs.SetInt("CheckpointBought", 2);
            Money.player_money_value -= second_checkpoint_value;
            second_checkpoint_bought = true;
            ShopCheckpoint.text = third_checkpoint_value.ToString();
        }
        else if (PlayerPrefs.GetInt("Checkpoint") >= 1 && Money.player_money_value >= first_checkpoint_value)
        {
            PlayerPrefs.SetInt("CheckpointBought", 1);
            Money.player_money_value -= first_checkpoint_value;
            first_checkpoint_bought = true;
            ShopCheckpoint.text = second_checkpoint_value.ToString();
        }
        else
            Debug.Log("NOT ENOUGH MONEY");

    }
}
