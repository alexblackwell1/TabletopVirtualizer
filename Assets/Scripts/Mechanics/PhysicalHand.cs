using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class PhysicalHand : MonoBehaviour
{
    private string hName;
    public Dropdown cardsDD;
    public Button play;
    public string selectedCard;
    public CardDeck cardDeck;

    public List<Tuple<CardCondition, List<CardAction>>> condActPair;
    private CardCondition condition;
    public CardCondition Condition
    {
        get { return condition; }
        set { condition = value; }
    }
    public List<CardAction> actions;


    // Start is called before the first frame update
    void Start()
    {
        updateHand();
        play.onClick.AddListener(wrapper);
    }

    public void updateHand()
    {
        cardsDD.ClearOptions();
        Dropdown.OptionData elemOption = new Dropdown.OptionData();
        elemOption.text = null;
        cardsDD.options.Add(elemOption);
        foreach (string c in cardDeck.getCards())
        {
            elemOption = new Dropdown.OptionData();
            elemOption.text = formatName(c);
            cardsDD.options.Add(elemOption);
        }
        selectedCard = null;
    }

    private string formatName(string cardName)
    {
        string suit = cardName.Substring(0, 1);
        string rank = cardName.Substring(1);
        switch (rank)
        {
            case "J":
                rank = "Jack";
                break;
            case "Q":
                rank = "Queen";
                break;
            case "K":
                rank = "King";
                break;
            case "A":
                rank = "Ace";
                break;
        }

        string newName = rank + " of ";

        switch (suit)
        {
            case "C":
                newName += "Clubs";
                break;
            case "S":
                newName += "Spades";
                break;
            case "H":
                newName += "Hearts";
                break;
            case "D":
                newName += "Diamonds";
                break;
        }
        return newName;
    }

    public void updateSelected()
    {
        string[] temp = cardsDD.options[cardsDD.value].text.Split(' ');
        if (temp[0] == "10")
        {
            selectedCard = temp[2].Substring(0, 1) + "10";
        }
        else
        {
            selectedCard = temp[2].Substring(0, 1) + temp[0].Substring(0, 1);
        }
        cardDeck.setTop(selectedCard);
        //cardDeck.selectedCard = selectedCard;
        Debug.Log("SelectedCard = "+selectedCard);
    }

    public void addActions(List<CardAction> a)
    {
        actions = a;
    }

    private void wrapper()
    {
        if (selectedCard != null)
        {
            foreach (var i in condActPair)
            {
                if (i.Item1.check())
                {
                    foreach (var act in i.Item2)
                    {
                        Debug.Log("SelectedCard = " + selectedCard);
                        act.interact();
                    }
                    break;
                }
            }
        }
        GameInfo.GAMEINFO.WinConditions.check();
    }
}
