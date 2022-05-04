using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAction : ActionEvent
{
    List<string> actionList;
    public int locationType;

    public CardAction(List<string> _actionList)
    {
        actionList = _actionList;
    }

    public void interact()
    {
        Location loc1;
        if (locationType == 1) loc1 = getCLocation(actionList[0]);
        else loc1 = getHLocation(actionList[0]);
        Debug.Log("actionList[1]: "+actionList[1]);
        Location loc2 = new CardLocation("",0);
        // deck2 gets cards drawn by deck1
        if (actionList[1] == "drawtotop")
        {
            Debug.Log("DrawToTop");
            int num = int.Parse(actionList[2]);
            if (locationType == 1) loc2 = getCLocation(actionList[3]);
            else loc2 = getHLocation(actionList[3]);
            while (num-- > 0)
                loc1.CardDeck.discardToDeckTop(loc2.CardDeck);
            if (loc1 is CardLocation)
            {
                if (loc1.CardDeck.isEmpty())
                {
                    loc1.CardSceneObj.setCard("");
                }
                else if (((CardLocation)loc1).FaceUp)
                {
                    loc1.CardSceneObj.setCard(loc1.CardDeck.top());
                }
            }
            else 
            {
                Debug.Log("Updating hands");
                loc1.HandSceneObj.updateHand();
            }
            if (loc2 is CardLocation && ((CardLocation)loc2).FaceUp)
            {
                if (loc2.CardDeck.isEmpty())
                {
                    loc2.CardSceneObj.setCard("");
                }
                else if (((CardLocation)loc2).FaceUp)
                {
                    loc2.CardSceneObj.setCard(loc2.CardDeck.top());
                }
            }
            else
            {
                loc2.HandSceneObj.updateHand();
            }
        }
        if (actionList[1] == "drawtorandom")
        {
            Debug.Log("DrawToRandom");
            int num = int.Parse(actionList[2]);
            if (locationType == 1) loc2 = getCLocation(actionList[3]);
            else loc2 = getHLocation(actionList[3]);
            while (num-- > 0)
                loc1.CardDeck.discardToDeckRandom(loc2.CardDeck);
            if (loc1 is CardLocation)
            {
                if (loc1.CardDeck.isEmpty())
                {
                    loc1.CardSceneObj.setCard("");
                }
                else if(((CardLocation)loc1).FaceUp)
                {
                    loc1.CardSceneObj.setCard(loc1.CardDeck.top());
                }
                
            }
            else
            {
                Debug.Log("Updating hands");
                loc1.HandSceneObj.updateHand();
                
            }
            if (loc2 is CardLocation)
            {
                if (loc2.CardDeck.isEmpty())
                {
                    loc2.CardSceneObj.setCard("");
                }
                else if (((CardLocation)loc2).FaceUp)
                {
                    loc2.CardSceneObj.setCard(loc2.CardDeck.top());
                }
            } 
            else
            {
                loc2.HandSceneObj.updateHand();
            }
            //return;
        }
        if (actionList[1] == "=")
        {
Debug.Log("Set");
            loc2 = getHLocation(actionList[2]);
            loc2.CardDeck.discardAllToDeck(loc1.CardDeck);
            if (((CardLocation)loc1).FaceUp) loc1.CardSceneObj.setCard(loc1.CardDeck.top());
            loc2.CardSceneObj.setCard("");
        }
    }

    CardLocation getCLocation(string loc)
    {
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
        for (int i = 0; i < actionList.Count; i++)
        {
            ret += actionList[i];
            if (i < actionList.Count - 1)
                ret += ",";
        }
        return ret;
    }
}
