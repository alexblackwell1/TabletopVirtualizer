using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintWinner : MonoBehaviour
{
    public Text textField;

    void Start()
    {
        textField.text = "Player " + GameInfo.GAMEINFO.Winner + " Wins!";
    }
}
