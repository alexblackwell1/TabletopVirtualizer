using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameCondition
{
	public List<string> ifCondition;
	public List<string> result;
	// CardLocation (1)
	// HandLocation (2)
	public int locationType;

	public GameCondition()
	{
		ifCondition = new List<string>();
		result = new List<string>();
	}


	public GameCondition(string ifC, string rst)
	{
		ifCondition = ifC.Split(' ').ToList();
		result = rst.Split(' ').ToList();
	}

	public bool check()
    {
		if (_check())
        {
			int playerNumber = int.Parse(result[0])-1;
			GameInfo.GAMEINFO.WinConditions.changeStatus(playerNumber, result[1]);
			return true;
		}
		return false;
    }

	private bool _check()
	{
		int val1;
		int val2;
		int next = 0;
		// find val1 value
		/// 'comparsion' will be at [1]
		if (ifCondition[0].All(char.IsDigit))
		{
			val1 = int.Parse(ifCondition[0]);
			next = 2;
		}
		/// looking at a location, 'comparsion' at [2]
		else
		{
			CardDeck deck1;
			if (locationType == 1)
			{
				deck1 = getCLocation(ifCondition[0]).CardDeck;
			}
			else
			{
				deck1 = getHLocation(ifCondition[0]).CardDeck;
			}
			val1 = getField(deck1, ifCondition[1]);
			next = 3;
		}
		// find val2 value
		/// if it is a number
		if (ifCondition[next].All(char.IsDigit))
		{
			val2 = int.Parse(ifCondition[next]);
		}
		else
		{
			CardDeck deck2;
			if (locationType == 1)
			{
				deck2 = getCLocation(ifCondition[0]).CardDeck;
			}
			else
			{
				deck2 = getHLocation(ifCondition[0]).CardDeck;
			}
			val2 = getField(deck2, ifCondition[next + 1]);
		}

		// do comparsion
		switch (ifCondition[next - 1])
		{
			case "<":
				return val1 < val2;
			case "=":
				return val1 == val2;
			case ">":
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
		return -1;
	}

	CardLocation getCLocation(string loc)
	{
		return GameInfo.GAMEINFO.CardLocations[loc];
	}
	Location getHLocation(string loc)
	{
		if (GameInfo.GAMEINFO.HandLocations.ContainsKey(loc))
		{
			return GameInfo.GAMEINFO.HandLocations[loc];
		}
		return GameInfo.GAMEINFO.CardLocations[loc];
	}

	public override string ToString()
    {
		string ret = "";
		foreach (string s in ifCondition)
			ret += s + " ";
		ret += "\n";
		foreach (string s in result)
			ret += s + " ";
		return ret;
    }
}