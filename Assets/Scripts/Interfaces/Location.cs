using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Location
{
    PhysicalCard CardSceneObj
    {
        get;
    }

    PhysicalHand HandSceneObj
    {
        get;
    }

    public CardDeck CardDeck
    {
        get;
        set;
    }

    string Name
    {
        get;
        set;
    }

    int NumElements
    {
        get;
        set;
    }

    public void addCondActPair(CardCondition cc, List<CardAction> ca);
}
