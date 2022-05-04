using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class PhysicalCard : MonoBehaviour
{
    private string suit;
    private string rank;
    private string cName;
    public Button faceField;


    public List<Tuple<CardCondition, List<CardAction>>> condActPair;
    private CardCondition condition;
    public CardCondition Condition
    {
        get { return condition; }
        set { condition = value; }
    }
    public List<CardAction> actions;

    public string getRank() { return this.rank; }
    public string getSuit() { return this.suit; }
    
    // Start is called before the first frame update
    void Start()
    {
        faceField.onClick.AddListener(wrapper);
    }

    public void setCard(string n)
    {
        if (n == "")
        {
            faceField.GetComponent<Image>().sprite = Resources.Load<Sprite>("GameElements/PlayingCards/EmptyDeck");
        }
        else if (n == "b")
        {
            faceField.GetComponent<Image>().sprite = Resources.Load<Sprite>("GameElements/PlayingCards/cardBack");
        }
        else
        {
            this.suit = n.Substring(0, 1);
            this.rank = n.Substring(1);
            cName = suit + rank;
            faceField.GetComponent<Image>().sprite = Resources.Load<Sprite>("GameElements/PlayingCards/" + cName);
        }     
    }

    public void addActions(List<CardAction> a)
    {
        actions = a;
    }

    private void wrapper()
    {
        foreach (var i in condActPair)
        {
            if (i.Item1.check())
            {
                foreach (var act in i.Item2)
                {
                    act.interact();
                }
                break;
            }
        }
        GameInfo.GAMEINFO.WinConditions.check();
    }
}
