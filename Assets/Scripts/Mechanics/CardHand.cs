/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardHand : CardDeck
{
    string name;
    int playerID;
    int MAX_SIZE;
    public List<string> hand;
    public string selected;
    public Vector3 loc;

    public CardHand(string _name, int id)
    {
        name = _name;
        playerID = id;
        MAX_SIZE = GameInfo.GAMEINFO.HandSize;
        hand = new List<string>(MAX_SIZE);
        selected = "";
    }

    public CardHand(string loadHand)
    {
        List<string> result = loadHand.Split(' ').ToList();
        name = result[0];
        playerID = int.Parse(result[1]);
        MAX_SIZE = GameInfo.GAMEINFO.HandSize;
        hand = new List<string>(MAX_SIZE);
        for (int i = 2; i < result.Count; i++)
        {
            hand.Add(result[i]);
        }
        selected = "";
    }

    public bool isFull()
    {
        return size() == MAX_SIZE;
    }

    public bool addCard(string card)
    {
        if (!isFull())
        {
            hand.Add(card);
            return true;
        }
        return false;
    }

    public bool hasCard(string card)
    {
        return hand.Contains(card);
    }

    public bool removeCard(string card)
    {
        return hand.Remove(card);
    }
    
    private string removeSelected()
    {
        string ret = selected;
        hand.Remove(ret);
        selected = "";
        return ret;
    }

    public void moveToDeck(CardDeck deck)
    {
        deck.addCard(removeSelected());
    }

    public void moveToHand(CardHand hand)
    {
        string _s = removeSelected();
        if (!hand.addCard(_s))
        {
            addCard(_s);
        }
    }

    public int size()
    {
        return hand.Count;
    }

    public string toString()
    {
        string ret = "";
        ret += name + " ";
        ret += playerID + " ";
        foreach (string c in hand)
        {
            ret += c + " ";
        }
        return ret;
    }
}
*/