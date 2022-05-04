using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition
{
    List<int> playerList;
    List<GameCondition> conditions;

    public WinCondition()
    {
        playerList = new List<int>(GameInfo.GAMEINFO.NumPlayers);
        conditions = new List<GameCondition>();
        // 0 for neither win/lose
        // 1 for victory
        // -1 if lost
        for (int i = 0; i < GameInfo.GAMEINFO.NumPlayers; i++)
        {
            playerList.Add(0);
        }
    }

    public void changeStatus(int playerNum, string condition)
    {
        if (condition == "wins")
        {
            playerWin(playerNum);
        }
        else
        {
            playerLose(playerNum);
        }
    }

    public void check()
    {
        int winner = 0;
        foreach(GameCondition gc in conditions)
        {
            gc.check();
            winner = checkPlayers();
            if (winner > 0)
            {
                // change pages
                GameInfo.GAMEINFO.Winner = winner;
                SceneManager.LoadScene("EndGame");
            }
        }
    }

    public void addCondition(GameCondition gc)
    {
        conditions.Add(gc);
    }

    private void playerWin(int playerNum)
    {
        playerList[playerNum] = 1;
    }

    private void playerLose(int playerNum)
    {
        playerList[playerNum] = -1;
    }

    // return 0 if no one has won
    // return playerNumber
    private int checkPlayers()
    {
        //check if any player has won
        /// or if everyone else has lost
        int numRemaining = 0;
        int remaining = -1;
        for (int i = 0; i < playerList.Count && numRemaining < 2; i++)
        {
            if (playerList[i] == 1) return i+1;
            if (playerList[i] == 0)
            {
                remaining = i+1;
                numRemaining++;
            }
        }

        if (numRemaining == 1)
            return remaining;
        return 0;
    }

    public override string ToString()
    {
        string ret = "";
        foreach (GameCondition gc in conditions)
        {
            ret += gc.ToString() + "\n";
        }
        ret += "~\n";
        return ret;
    }
}
