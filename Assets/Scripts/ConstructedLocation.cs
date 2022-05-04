using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConstructedLocation : MonoBehaviour
{ 
    private static ConstructedLocation _LOC_SETUP;
    public static ConstructedLocation LOC_SETUP
    {
        get
        {
            if (_LOC_SETUP == null)
            {
                _LOC_SETUP = GameObject.FindObjectOfType<ConstructedLocation>();
            }
            return _LOC_SETUP;
        }
    }

    private static string locName = null;
    public string LocName
    {
        get { return locName; }
        set { locName = value; }
    }
    public static string elemType = null;
    public string ElemType
    {
        get { return elemType; }
        set { elemType = value; }
    }

    public static CardDeck deck = null;
    private static int startingAmount = -1;
    public int StartingAmount
    {
        get { return startingAmount; }
        set { startingAmount = value; }
    }

    private static bool placingCard = false;
    public bool PlacingCard
    {
        get { return placingCard; }
        set { placingCard = value; }
    }


    private static Dictionary<string, string> tempLocNames = new Dictionary<string, string>();
    public Dictionary<string, string> TempLocNames
    {
        get { return tempLocNames; }
    }



    private static List<GameElement> builtElements;
    public List<GameElement> BuiltElements
    {
        get { return builtElements; }
    }

    void Awake()
    {
        builtElements = GameInfo.GAMEINFO.Elements;
    }

    //private static CardDeck fDeck = null;
    public void builElemLocation()
    {
        if (locName != null && elemType != null && startingAmount >= 0)
        {
            for (int i = 0; i < GameInfo.GAMEINFO.NumPlayers; i++)
            {
                GameInfo.GAMEINFO.StartingAmount.Add(startingAmount);
                if (elemType == "Cards")
                {
                    CardLocation curLoc = new CardLocation(locName, i+1);
                    GameInfo.GAMEINFO.CardLocations.Add(locName+"_"+(i+1), curLoc);
                }
                else if (elemType == "Card Hand")
                {
                    Debug.Log("Card Hand");
                    HandLocation curHand = new HandLocation(locName, i+1);
                    GameInfo.GAMEINFO.HandLocations.Add(locName + "_" + (i + 1), curHand);
                }
                else
                {
                    GameInfo.GAMEINFO.ElementLocations.Add(locName+ "_"+(i+1), new ElementLocation(locName, elemType, i+1));
                }
                tempLocNames.Add(locName+"_"+(i+1), elemType);
            }
            Debug.Log("Loc created!");
            locName = null;
            startingAmount = -1;
            setInputFields();
        } 
        else
        {
            Debug.Log("Need to have input selected");
        }
    }

    private void setInputFields()
    {
        GameObject.Find("name_txtInput").GetComponent<InputField>().text = locName;
        GameObject.Find("amount_txtInput").GetComponent<InputField>().text = null;
    }

    public string location1;
    public string parseInput;

    public void build()
    {
        Parser parser = new Parser();
        Location primaryLocation = null;// = GameInfo.GAMEINFO.CardLocations[location1]; ;
        if (tempLocNames[location1] == "Cards")
        {
            if (parseInput == "FaceDown")
            {
                Debug.Log("Face down location");
                GameInfo.GAMEINFO.CardLocations[location1].FaceUp = false;
                return;
            }
            primaryLocation = GameInfo.GAMEINFO.CardLocations[location1];
            parser.condition.locationType = 1;
        } 
        else if (tempLocNames[location1] == "Card Hand")
        {
            primaryLocation = GameInfo.GAMEINFO.HandLocations[location1];
            parser.condition.locationType = 2;
        }
        Debug.Log("L type: "+parser.condition.locationType);
        parser.parse(parseInput);
        primaryLocation.addCondActPair(parser.condition, parser.actions);

        GameObject.Find("input_inputText").GetComponent<InputField>().text = "";
        /*Debug.Log(primaryLocation.Actions.ToString());
        Debug.Log(primaryLocation.Condition.ToString());*/
    }
}
