using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LayoutBoard : MonoBehaviour
{
    public GameObject board;
    public GameObject elemPF;
    public GameObject cardPF;
    public GameObject handPF;
    public List<GameObject> locations = new List<GameObject>();
    public List<GameObject> cardLocations = new List<GameObject>();
    private CardDeck fDeck;
    private int aCount = 0;

    // Start is called before the first frame update
    void Awake()
    {
        //If a board was selected, set board
        if (GameInfo.GAMEINFO.getBoard() != null)
        {
            board.GetComponent<Image>().sprite = GameInfo.GAMEINFO.getBoard();
        }


        //For each game elemen, go through its locations and create a gameobject at each location
        //List<GameElement> elems = GameInfo.GAMEINFO.Elements;
        Dictionary<string, ElementLocation> elems = GameInfo.GAMEINFO.ElementLocations;
        for (int i = 0; i < elems.Count; i++)
        {
            //Dictionary<string, Vector2> elemLocs = elems[i].getLocations();
            string locName = elems.ElementAt(i).Key;
            GameObject loc = new GameObject(locName);
            loc.transform.SetParent(GameObject.Find("Canvas").transform, false);
            loc.transform.position = elems.ElementAt(i).Value.boardLocation;

            GameObject gObj = Instantiate(elemPF, new Vector3(0, 0, 0), transform.rotation);
            gObj.transform.SetParent(GameObject.Find(locName).transform, false);
            ElementLocation _loc = (ElementLocation)gObj.GetComponent<Location>();
            _loc.setElemObjImg(elems.ElementAt(i).Value.img.GetComponent<Sprite>());
        }

        if (fDeck == null)
        {
            Debug.Log("Created Deck");
            fDeck = new CardDeck();
            fDeck.fullDeck();
            fDeck.shuffle();
        }

        List<CardDeck> splitDecks = fDeck.divyOutXCards(GameInfo.GAMEINFO.StartingAmount[aCount]); 
        //Dictionary<string, Vector2> cardLocs = GameInfo.GAMEINFO.CardLocations;
        Dictionary<string, CardLocation > cardLocs = GameInfo.GAMEINFO.CardLocations;
        Debug.Log("card locs# = " + cardLocs.Count);
        for (int i = 0; i < cardLocs.Count; i++)
        {
            Debug.Log(aCount);
            if (i % GameInfo.GAMEINFO.NumPlayers == 0 && i != 0)
            {
                Debug.Log("New split");
                splitDecks = fDeck.divyOutXCards(GameInfo.GAMEINFO.StartingAmount[aCount]);
            }
            aCount++;

            string locName = cardLocs.ElementAt(i).Key;
            CardLocation cardLoc = cardLocs.ElementAt(i).Value;
            GameObject loc = new GameObject(locName);
            loc.transform.SetParent(GameObject.Find("Canvas").transform, false);
            loc.transform.position = cardLocs[locName].boardLocation;

            GameObject curCard = Instantiate(cardPF, new Vector3(0, 0, 0), transform.rotation);
            curCard.transform.SetParent(loc.transform, false);
            PhysicalCard curCardInfo = curCard.GetComponent<PhysicalCard>();

            //set card deck and 
            cardLoc.CardDeck = splitDecks[i % GameInfo.GAMEINFO.NumPlayers];
            cardLoc.CardDeck.revealTop();

            if (!cardLoc.CardDeck.isEmpty())
            {
                //cardLoc.CardDeck.setTopRandom();
                if (cardLoc.FaceUp)
                {
                    curCardInfo.setCard(cardLoc.CardDeck.top());
                }
                else
                {
                    curCardInfo.setCard("b");
                }
            }
            else
            {
                curCardInfo.setCard("");
            } 
            cardLoc.cardSceneObj = curCardInfo;

            //Debug.Log(cardLocs[locName].Action.ToString());
            curCardInfo.condActPair = cardLoc.condActPair;
            Debug.Log(locName + ": "+cardLoc.CardDeck.size());
        }

        
        Dictionary<string, HandLocation> handLocs = GameInfo.GAMEINFO.HandLocations;
        if (aCount >= GameInfo.GAMEINFO.StartingAmount.Count) return;
        splitDecks = fDeck.divyOutXCards(GameInfo.GAMEINFO.StartingAmount[aCount]);
        for (int i = 0; i < handLocs.Count; i++)
        {
            if (i % GameInfo.GAMEINFO.NumPlayers == 0 && i != 0)
            {
                fDeck.divyOutXCards(GameInfo.GAMEINFO.StartingAmount[aCount]);
            }
            aCount++;

            string handName = handLocs.ElementAt(i).Key;
            HandLocation handLoc = handLocs.ElementAt(i).Value;
            GameObject loc = new GameObject(handName);
            loc.transform.SetParent(GameObject.Find("Canvas").transform, false);
            loc.transform.position = handLocs[handName].boardLocation;

            handLoc.CardDeck = splitDecks[i % GameInfo.GAMEINFO.NumPlayers];

            GameObject curHand = Instantiate(handPF, new Vector3(0, 0, 0), transform.rotation);
            curHand.transform.SetParent(loc.transform, false);
            PhysicalHand curHandInfo = curHand.GetComponent<PhysicalHand>();
            curHandInfo.cardDeck = handLoc.CardDeck;
            curHandInfo.updateHand();

            handLoc.handSceneObj = curHandInfo;

            curHandInfo.condActPair = handLoc.condActPair;
        }
    }

}
