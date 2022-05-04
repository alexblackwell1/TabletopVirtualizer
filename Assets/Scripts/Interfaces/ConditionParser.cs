using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionParser
{
	public GameCondition condition;

	public ConditionParser()
	{
		condition = new GameCondition();
	}

	public void parse(string input)
	{
		// input string
		// deck_1 count = 0 : player1 loses;
		// deck_2 rank > deck_1 rank : player2 wins;
		string[] tkns = input.ToLower().Split(' ');
		List<string> tokens = new List<string>(tkns);

		int i = 0;
		List<string> ifInput = new List<string>();
		while (tokens[i] != ":")
			ifInput.Add(tokens[i++]);

		List<string> result = new List<string>();
		while (tokens[++i] != ";")
        {
			result.Add(tokens[i]);
        }

		condition.ifCondition = ifInput;
		condition.result = result;
	}
}
