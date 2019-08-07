using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public static int player_money_value;
    public Text money_value_text;

    // Start is called before the first frame update
    void Start()
    {
        player_money_value = PlayerPrefs.GetInt("Money", 10);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("Money", player_money_value);
        money_value_text.text = "x" + player_money_value;
    }

}
