using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ExportGame : MonoBehaviour
{
    string exported;

    public void Export()
    {
        exported += game_info();
        exported += elements();
        exported += locations();
        exported += conditions();
    }

    private string game_info()
    {
        string ret = GameInfo.GAMEINFO.GameName + "\n";
        ret += GameInfo.GAMEINFO.NumPlayers + "\n";
        // TODO: board image
        ret += GameInfo.GAMEINFO.BoardName + "\n";
        ret += GameInfo.GAMEINFO.HasDeckOfCards + "\n";
        ret += GameInfo.GAMEINFO.HasHandOfCards + "\n";
        ret += GameInfo.GAMEINFO.HandSize + "\n";
        return ret;
    }

    private string elements()
    {
        string ret = "";
        foreach (GameElement element in GameInfo.GAMEINFO.Elements)
        {
            // TODO (in GameElement toString) : Element image path
            ret += element.ToString() + "\n";
        }
        ret += "~\n";
        return ret;
    }

    private string locations()
    {
        int startAmt = 0;
        string ret = "";
        //Hand locations
        foreach (KeyValuePair<string, HandLocation> entry in GameInfo.GAMEINFO.HandLocations)
        {
            ret += entry.Key + "\n";
            ret += entry.Value.ToString();
            ret += GameInfo.GAMEINFO.StartingAmount[startAmt++] + "\n";
        }
        // element locations
        ret += "element\n";
        foreach (KeyValuePair<string, ElementLocation> entry in GameInfo.GAMEINFO.ElementLocations)
        {
            ret += entry.Key + "\n";
            ret += entry.Value.ToString();
        }
        ret += "card\n";
        // card locations
        foreach (KeyValuePair<string, CardLocation> entry in GameInfo.GAMEINFO.CardLocations)
        {
            ret += entry.Key + "\n";
            ret += entry.Value.ToString();
            ret += GameInfo.GAMEINFO.StartingAmount[startAmt++] + "\n";
        }
        ret += "end of locations\n";
        return ret;
    }

    private string conditions()
    {
        return GameInfo.GAMEINFO.WinConditions.ToString();
    }

    public override string ToString()
    {
        return exported;
    }

    public void write()
    {
        Export();
        string filename = "./Assets/Games/" + GameInfo.GAMEINFO.GameName + ".wme";
        File.WriteAllText(filename, exported);
    }
}
