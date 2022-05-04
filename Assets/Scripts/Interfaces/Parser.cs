using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parser
{
    public CardCondition condition;
    public List<CardAction> actions;

    public Parser()
    {
        condition = new CardCondition();
        actions = new List<CardAction>();
    }

    public void parse(string input)
    {   
        // input string
        // if deck1 rank > deck2 rank : deck3 = deck1 ; deck3 = deck2 ;
        // if deck1 count <= deck2 count : deck3 = deck1 ; deck3 = deck2 ;
        // deck3 draw 1 deck1 ;
        string[] tkns = input.ToLower().Split(' ');
        List<string> tokens = new List<string>(tkns);

        // ifs should always be first
        // if the condition is met, then do all the cases

        int i = 0;
        if (tokens[i] == "if")
        {
            List<string> ifInput = new List<string>();
            while (tokens[++i] != ":")
                ifInput.Add(tokens[i]);

            condition = ifCase(ifInput);
        }

        // actions seperated by ;
        while (++i < tokens.Count)
        {
            List<string> actionTkns = new List<string>();
            while (tokens[i] != ";")
            {
                actionTkns.Add(tokens[i++]);
            }
            actions.Add(doAction(actionTkns));
        }
    }

    CardAction doAction(List<string> actionList)
    {
        CardAction nCA = new CardAction(actionList);
        nCA.locationType = condition.locationType;
        return nCA;
    }

    CardCondition ifCase(List<string> ifInput)
    {
        //  0       1       2           3       4
        // object, field, comparsion, object, field
        return new CardCondition(ifInput);
    }
}
