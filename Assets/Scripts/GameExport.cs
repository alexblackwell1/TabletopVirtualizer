using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExport : MonoBehaviour
{
    GameInfo build;
    string fileName;
    // Start is called before the first frame update
    void Start()
    {
        build = GameInfo.GAMEINFO;
        fileName = build.GameName;
    }

    void createFile() 
    {
        
    } 

    string getCards()
    {
        string cardInfo = "";
        if (build.HasDeckOfCards)
        {
            cardInfo += "Cards: ";
            foreach(var i in build.CardLocations)
            {
                cardInfo += i.Key;
                CardLocation cardLoc = i.Value;
            }
        }

        return cardInfo;
    }
}
