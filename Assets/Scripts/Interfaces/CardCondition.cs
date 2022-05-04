using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardCondition
{
    public List<string> ifInput;
    public bool defaultReturn;
    // CardLocation (1)
    // HandLocation (2)
    public int locationType;

    public CardCondition()
    {
        defaultReturn = true;
    }

    public CardCondition(List<string> _ifInput)
    {
        defaultReturn = false;
        ifInput = _ifInput;
    }

    public CardCondition(List<string> _ifInput, bool dRet)
    {
        defaultReturn = dRet;
        ifInput = _ifInput;
    }

    public bool check()
    {
        Debug.Log("LocType: " + locationType);
        if (defaultReturn)
            return true;

        // 1       2           3       4       5
        // object, field, comparsion, object, field
        // object, field, comparsion, number
        // number, comparsion, object, field
        // number, comparsion, number

        int val1;
        int val2;
        int next = 0;
        // find val1 value
        /// number first, 'comparsion' will be at [2]
        if (ifInput[0].All(char.IsDigit))
        {
            val1 = int.Parse(ifInput[0]);
            next = 2;
        }
        /// looking at a location, 'comparsion' at [3]
        else
        {
            CardDeck deck1;
            if (locationType == 1)
            {
                deck1 = getCLocation(ifInput[0]).CardDeck;
            }
            else
            {
                deck1 = getHLocation(ifInput[0]).CardDeck;
            }
            val1 = getField(deck1, ifInput[1]);
            next = 3;
        }
        // find val2 value
        /// if it is a number
        if (ifInput[next].All(char.IsDigit))
        {
            val2 = int.Parse(ifInput[next]);
        }
        else
        {
            CardDeck deck2;
            if (locationType == 1)
            {
                deck2 = getCLocation(ifInput[next]).CardDeck;
            }
            else
            {
                deck2 = getHLocation(ifInput[next]).CardDeck;
            }
            val2 = getField(deck2, ifInput[next + 1]);
        }

        // do comparsion
        switch (ifInput[next - 1])
        {
            case "<":
Debug.Log(val1+"<"+val2+" = "+(val1<val2));
                return val1 < val2;
            case "=":
Debug.Log(val1+"=="+val2+" = "+(val1==val2));
                return val1 == val2;
            case ">":
Debug.Log(val1+">"+val2+" = "+(val1>val2));
                return val1 > val2;
        }
        return false;
    }

    int getField(CardDeck deck, string field)
    {
        if (field == "count")
        {
            return deck.size();
        }
        if (field == "rank")
        {
            string card = deck.top();
            if (card.Length == 3) return 10;
            int rank = (int)card[1];
            Debug.Log("Rank: " + rank);
            if ((rank - 48) > 1 && (rank - 48) < 10)
                return rank - 48;
            switch (rank)
            {
                case (int)'J':
                    return 11;
                case (int)'Q':
                    return 12;
                case (int)'K':
                    return 13;
                case (int)'A':
                    return 14;
            }
        }
        if (field == "suit")
        {
            return (int)deck.top()[0];
        }
        return -1;
    }

    CardLocation getCLocation(string loc)
    {
        Debug.Log("Loc: " + loc);
        return GameInfo.GAMEINFO.CardLocations[loc];
    }

    Location getHLocation(string loc)
    {
        Debug.Log("Loc: " + loc);
        if (GameInfo.GAMEINFO.HandLocations.ContainsKey(loc))
        {
            return GameInfo.GAMEINFO.HandLocations[loc];
        }
        return GameInfo.GAMEINFO.CardLocations[loc];
    }

    public override string ToString()
    {
        string ret = "";
        for (int i = 0; i < ifInput.Count; i++)
        {
            ret += ifInput[i];
            if (i < ifInput.Count - 1)
                ret += ",";
        }
        return ret;
    }
}
