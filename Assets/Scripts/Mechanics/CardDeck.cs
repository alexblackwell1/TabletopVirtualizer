using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class CardDeck
{
    public bool topVisible;
    public bool isHand; // allVisible
    public int MAX_SIZE;

    private List<string> deck = new List<string>();

    public CardDeck()
    {
        shuffle();
    }

    public void fullDeck()
    {
        topVisible = false;

        string[] suits = { "C", "S", "H", "D" };
        string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                deck.Add(suits[i] + ranks[j]);
            }
        }

        //setTopRandom();
    }

    public void shuffle()
    {
        Random rnd = new Random();
        List<string> deck2 = new List<string>();
        while (deck.Count > 0)
        {
            int n = rnd.Next(deck.Count);
            deck2.Add(deck[n]);
            deck.RemoveAt(n);
        }
        deck = deck2;
    }

    public string draw()
    {
        // return the top card
        string tc = deck[0];
        // remove old top
        deck.RemoveAt(0);
        return tc;
    }

    public void setTop(string card)
    {
        deck.Remove(card);
        deck.Insert(0, card);
    }

    public List<CardDeck> divyOutXCards(int num_cards)
    {
        int num_players = GameInfo.GAMEINFO.NumPlayers;
        num_cards *= num_players;
        if (num_cards >= deck.Count)
            return splitDeck();
        List<CardDeck> split_decks = new List<CardDeck>();
        for (int i = 0; i < num_players; i++) split_decks.Add(new CardDeck());
        for (int i = 0; i < num_cards; i++)
        {
            split_decks[i % num_players].addCard(draw());
        }
        return split_decks;
    }

    public void discardToDeckBottom(CardDeck _deck2)
    {
        // send over the top card
        _deck2.addCard(draw());
    }

    public void discardToDeckTop(CardDeck _deck2)
    {
        _deck2.deck.Insert(0, draw());
    }

    public void discardToDeckRandom(CardDeck _deck2)
    {
        Random rnd = new Random();
        _deck2.deck.Insert(rnd.Next(_deck2.size()), draw());
    }


    public void discardAllToDeck(CardDeck deck2)
    {
        while (!isEmpty())
            discardToDeckBottom(deck2);
    }

    public List<CardDeck> splitDeck()
    {
        int num_splits = GameInfo.GAMEINFO.NumPlayers;
        List<CardDeck> split_decks = new List<CardDeck>();
        for (int i = 0; i < num_splits; i++) split_decks.Add(new CardDeck());
        int player = 0;
        while (size() > 0)
        {
            split_decks[player].addCard(this.draw());
            player = (++player) % num_splits;
        }
        return split_decks;
    }

    public void addCard(string cardName)
    {
        if (cardName == null)
            return;
        deck.Add(cardName);
    }

    public void addCards(List<string> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            deck.Add(cards[i]);
        }
    }

    public bool removeCard(string card)
    {
        return deck.Remove(card);
    }

    public bool hasCard(string card)
    {
        return deck.Contains(card);
    }

    public List<string> getCards()
    {
        List<string> allCards = new List<string>(deck);
        return allCards;
    }

    public string top()
    {
        return deck[0];
    }

    public bool isFull()
    {
        return size() == MAX_SIZE;
    }

    public void revealTop()
    {
        topVisible = true;
        // topCard = cardProp selectedCard
    }

    public void hideTop()
    {
        topVisible = false;
        // topCard = default back
    }

    public void setMaxSize(int i)
    {
        MAX_SIZE = i;
    }

    public int size()
    {
        return deck.Count;
    }

    public bool isEmpty() { return deck.Count == 0; }
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck //: MonoBehaviour
{
	public string selectedCard;
	public bool topVisible;
    public bool isHand; // allVisible
    public int MAX_SIZE;
    private string none = "None";

    private List<string> deck = new List<string>();

    public CardDeck()
    {
        setTopRandom();
        MAX_SIZE = GameInfo.GAMEINFO.HandSize;
    }

    public void fullDeck()
    {
		topVisible = false;
		
        string[] suits = { "C", "S", "H", "D" };
        string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                deck.Add(suits[i] + ranks[j]);
            }
        }
		
		setTopRandom();
    }

    public void setTopRandom()
    {
        // case: no deck
        if (deck.Count <= 0)
        {
            selectedCard = none;
            return;
        }
        // else:
        int num = Random.Range(0, deck.Count);
        // get a random card still in the deck
        string drawnCard = deck[num];
        // remove it from the deck
        deck.RemoveAt(num);
        // add it as the top card
        selectedCard = drawnCard;
    }

    public string draw()
    {
        // return the top card
        string tc = selectedCard;
        // set a new top card
        setTopRandom();
        return tc;
    }

    public List<CardDeck> divyOutXCards(int num_cards)
    {
        int num_players = GameInfo.GAMEINFO.NumPlayers;
        num_cards *= num_players;
        if (num_cards >= deck.Count)
            return splitDeck();

        List<CardDeck> split_decks = new List<CardDeck>();
        for (int i = 0; i < num_players; i++) split_decks.Add(new CardDeck());
        for (int i = 0; i < num_cards; i++)
        {
            split_decks[i % num_players].addCard(draw());
        }
        return split_decks;
    }

    public void discardToDeck(CardDeck _deck2)
    {
        // send over the top card
        _deck2.addCard(selectedCard);
        // set a new top card
        setTopRandom();
        if (_deck2.selectedCard == none)
        {
            _deck2.setTopRandom();
        }
    }

    public void discardToDeckTop(CardDeck _deck2)
    {
        _deck2.shuffleTopCard();
        _deck2.selectedCard = draw();
    }

    public void shuffleTopCard()
    {
        deck.Add(selectedCard);
        selectedCard = "";
    }

    public void discardAllToDeck(CardDeck deck2)
    {
        while (!isEmpty())
            discardToDeck(deck2);
    }

    public void returnTop()
    {
        deck.Add(selectedCard);
    }

    public List<CardDeck> splitDeck()
    {
        int num_splits = GameInfo.GAMEINFO.NumPlayers;
        List<CardDeck> split_decks = new List<CardDeck>();
        for (int i = 0; i < num_splits; i++) split_decks.Add(new CardDeck());
        int player = 0;
        while (size() > 0)
        {
            split_decks[player].addCard(this.draw());
            player = (++player) % num_splits;
        }
        return split_decks;
    }
    
    public void addCard(string cardName)
    {
        if (cardName == null)
            return;
        deck.Add(cardName);
    }

    public void addCards(List<string> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            deck.Add(cards[i]);
        }
    }

    public bool removeCard(string card)
    {
        // if the card wasn't in the deck
        if (!deck.Remove(card))
        {
            if (selectedCard == card)
            {
                draw();
                return true;
            }
            return false;
        }
        return true;
    }
    public bool hasCard(string card)
    {
        if (isHand)
        {
            return deck.Contains(card);
        }
        return false;
    }

    public List<string> getCards()
    {
        List<string> allCards = new List<string>(deck);
        if (selectedCard != none)
        {
            allCards.Add(selectedCard);
        }
        return allCards;
    }

    public bool isFull()
    {
        return size() == MAX_SIZE;
    }

    public void revealTop() {
		topVisible = true;
		// topCard = cardProp selectedCard
	}
	
	public void hideTop() {
		topVisible = false;
		// topCard = default back
	}

    public void setMaxSize(int i)
    {
        MAX_SIZE = i;
    }

    public int size() {
        int i = 1;
        if (selectedCard == none)
            i--; ;
        return i+deck.Count;
    }
	
	public bool isEmpty() { return deck.Count == 0 && selectedCard == none; }
}
*/