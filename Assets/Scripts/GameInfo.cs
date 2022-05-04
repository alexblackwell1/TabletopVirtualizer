using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour
{
    private static GameInfo _GAMEINFO;

    public static GameInfo GAMEINFO
    {
        get
        {
            if (_GAMEINFO == null)
            {
                _GAMEINFO = GameObject.FindObjectOfType<GameInfo>();
            }
            return _GAMEINFO;
        }
    }


    private static string gameName = "Game";
    public string GameName
    {
        get { return gameName; }
        set { gameName = value; }
    }

    private static WinCondition wincondition;
    public WinCondition WinConditions
    {
        get { return wincondition; }
        set { wincondition = value; }
    }

    private static string boardName = " ";
    public string BoardName
    {
        get { return boardName; }
        set { boardName = value; }
    }

    private static Sprite boardImage;
    public void setBoard(Sprite b) { boardImage = b; }
    public Sprite getBoard() { return boardImage; }

    private static int numPlayers = 1;
    public int NumPlayers
    {
        get { return numPlayers; }
        set { numPlayers = value; }
    }

    private static bool hasDeckOfCards = false;
    public bool HasDeckOfCards
    {
        get { return hasDeckOfCards; }
        set { hasDeckOfCards = value; }
    }

    private static bool hasHandOfCards = false; 
    public bool HasHandOfCards
    {
        get { return hasHandOfCards; }
        set { hasHandOfCards = value; }
    }

    private static Dictionary<string, HandLocation> handLocations = new Dictionary<string, HandLocation>();
    public Dictionary<string, HandLocation> HandLocations
    {
        get { return handLocations; }
    }

    private static Dictionary<string, ElementLocation> elementLocations = new Dictionary<string, ElementLocation>();
    public Dictionary<string, ElementLocation> ElementLocations
    {
        get { return elementLocations; }
    }

    private static Dictionary<string, CardLocation> cardLocations = new Dictionary<string, CardLocation>();
    public Dictionary<string, CardLocation> CardLocations
    {
        get { return cardLocations; }
    }

    private static List<GameElement> elements = new List<GameElement>();
    public List<GameElement> Elements
    {
        get { return elements; }
        set { elements = value; }
    }

    private static int handSize = -1;
    public int HandSize
    {
        get { return handSize; }
        set { handSize = value; }
    }

    private static List<int> startingAmount = new List<int>();
    public List<int> StartingAmount
    {
        get { return startingAmount; }
        set { startingAmount = value; }
    }

    public string curElem;
    public Sprite curImg;
    public void buildCurElement()
    {
        if (curElem != null && curImg != null)
        {
            createGameElement(curElem, curImg);
            curElem = null;
            curImg = null;
            GameObject.Find("name_txtInput").GetComponent<InputField>().text = curElem;
            GameObject.Find("image_btn").GetComponent<Button>().GetComponent<Image>().sprite = curImg;
            Debug.Log("Element created!");
        } 
        else
        {
            Debug.Log("Fields cannot be empty");
        }
            
    }

    public void createGameElement(string en, Sprite es)
    {
        //Creates a gameElement with specified attributes
        GameElement newElem = new GameElement(en);
        newElem.setImage(es);
        elements.Add(newElem);
    }

    public void buildCondition()
    {
        ConditionParser parser = new ConditionParser();
        //reusing old public string to get input from UserInput
        Debug.Log(curElem);
        string l = curElem.Split(' ')[0];
        int t;
        if (CardLocations.ContainsKey(l)) t = 1;
        else t = 2;
        parser.condition.locationType = t;

        if (wincondition == null) wincondition = new WinCondition();
        parser.parse(curElem);
        WinConditions.addCondition(parser.condition);
        Debug.Log(parser.condition.ToString());
        GameObject.Find("input_inputText").GetComponent<InputField>().text = "";
    }

    private static int winner;
    public int Winner
    {
        get { return winner; }
        set { winner = value; }
    }

    public void reset()
    {
        gameName = "Game";
        hasDeckOfCards = false;
        handLocations = new Dictionary<string, HandLocation>();
        elementLocations = new Dictionary<string, ElementLocation>();
        cardLocations = new Dictionary<string, CardLocation>();
        elements = new List<GameElement>();
        handSize = -1;
        startingAmount = new List<int>();
    }
}
