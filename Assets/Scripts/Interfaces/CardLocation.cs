using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CardLocation : Location
{
    private string name;
    private int numElements;
    public Vector3 boardLocation;
    public int playerNum;
    public PhysicalCard cardSceneObj;
    public CardDeck cDeck;
    public GameObject img;

    public List<Tuple<CardCondition, List<CardAction>>> condActPair;
    public List<CardAction> actions = new List<CardAction>();
    public CardCondition condition;
    public bool FaceUp;

    public CardLocation(string n, int pnum)
    {
        name = n;
        numElements = 0;
        playerNum = pnum;  
        condActPair = new List<Tuple<CardCondition, List<CardAction>>>();
        FaceUp = true;
    }

    public CardLocation(string n, int pnum, int numE, bool face)
    {
        name = n;
        numElements = numE;
        playerNum = pnum;
        condActPair = new List<Tuple<CardCondition, List<CardAction>>>();
        FaceUp = face;
    }

    string Location.Name
    {
        get { return name; }
        set { name = value; }
    }
    int Location.NumElements
    {
        get { return numElements; }
        set { numElements = value; }
    }
    
    public CardDeck CardDeck
    {
        get { return cDeck; }
        set { cDeck = value; }
    }

    PhysicalCard Location.CardSceneObj
    {
        get { return cardSceneObj; }
    }

    PhysicalHand Location.HandSceneObj
    {
        get { return null; }
    }

    public List<CardAction> Actions
    {
        get { return actions; }
        set { actions = value; }
    }

    public void addCondActPair(CardCondition cc, List<CardAction> ca)
    {
        condActPair.Add(new Tuple<CardCondition, List<CardAction>>(cc, ca));
    }

    // repeated in HandLocations
    public override string ToString()
    {
        string ret = name + "\n";
        ret += playerNum + "\n";
        ret += numElements + "\n";
        ret += FaceUp + "\n";
        ret += boardLocation.x + "\n" + boardLocation.y + "\n" + boardLocation.z + "\n";
        for (int i = 0; i < condActPair.Count; i++)
        {
            ret += condActPair[i].Item1.ToString() + "\n" + condActPair[i].Item1.defaultReturn + "\n";
            for (int j = 0; j < condActPair[i].Item2.Count; j++)
            {
                ret += condActPair[i].Item2[j].ToString() + "\n";
            }
            ret += "~\n";
        }
        ret += "~\n";
        return ret;
    }
}
