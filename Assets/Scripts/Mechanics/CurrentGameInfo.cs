/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentGameInfo : MonoBehaviour
{
    private static CurrentGameInfo _CUR_GAME;
    public static CurrentGameInfo CUR_GAME
    {
        get
        {
            if (_CUR_GAME == null)
            {
                _CUR_GAME = GameObject.FindObjectOfType<CurrentGameInfo>();
            }
            return _CUR_GAME;
        }
    }

    //public CardPlayInterface playMethod { get; set; }

    public static CardDeck deck; 
    private static List<CardDeck> playerHands;
    public List<CardDeck> PlayerHands
    {
        get { return playerHands; }
    }

    public List<GameObject> cardObjLocations;

    private static GameObject deckButton;
    public GameObject DeckButton
    {
        get { return deckButton; }
        set { deckButton = value; }
    }

    void Start()
    {
        *//*
        deck = new CardDeck();//GameInfo.GAMEINFO.DeckOfCards;
        deck.fullDeck();
        playerHands = deck.splitDeck();
        //playMethod = new EachPlaysRandCard(2, playerHands);
        //GameObject c0 = GameObject.Find("Card0/GameElement(Clone)/Canvas/ElemButton");
        //c0.GetComponent<Button>().onClick.AddListener(test);
        //DeckButton.GetComponent<Button>().onClick.AddListener(test);*//*
    }

    public void test()
    {*//*
        playMethod.onPlay();
        Debug.Log("Reach");*//*
    }
}
*/