using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HandLocation : Location
{ 
    private string name;
    private int numElements;
    public Vector3 boardLocation;
    public int playerNum;
    public PhysicalHand handSceneObj;
    public CardDeck cDeck;

    public List<Tuple<CardCondition, List<CardAction>>> condActPair;
    public List<CardAction> actions = new List<CardAction>();
    public CardCondition condition;

    public HandLocation(string n, int pnum)
    {
        name = n;
        numElements = 0;
        playerNum = pnum;
        condActPair = new List<Tuple<CardCondition, List<CardAction>>>();
    }

    public HandLocation(string n, int pnum, int numE)
    {
        name = n;
        numElements = numE;
        playerNum = pnum;
        condActPair = new List<Tuple<CardCondition, List<CardAction>>>();
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
        get { return null; }
    }

    PhysicalHand Location.HandSceneObj
    {
        get { return handSceneObj; }
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

    public void addActions(List<CardAction> l)
    {
        foreach (var c in l)
        {
            actions.Add(c);
        }
    }

    public CardCondition Condition
    {
        set { condition = value; }
        get { return condition; }
    }

    // repeated in CardLocations
    public override string ToString()
    {
        string ret = name + "\n";
        ret += playerNum + "\n";
        ret += numElements + "\n";
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
