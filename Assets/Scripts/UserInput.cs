using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UserInput : MonoBehaviour
{
    public string string_tmp;
    public Vector2[] loc_tmp;
    public Sprite sprite_tmp;
    public ConstructedLocation cur;

    public void getInputField(string field) {
        GameObject child;
        switch (field)
        {
            case "gamename":
                child = gameObject.transform.GetChild(0).gameObject;
                //Debug.Log(child.GetComponent<InputField>().text);
                GameInfo.GAMEINFO.GameName = child.GetComponent<InputField>().text;
                Debug.Log(GameInfo.GAMEINFO.GameName);
                break;
            case "num_players":
                child = gameObject.transform.GetChild(1).gameObject;
                GameInfo.GAMEINFO.NumPlayers = Int32.Parse(child.GetComponent<Dropdown>().options[child.GetComponent<Dropdown>().value].text);
                Debug.Log(GameInfo.GAMEINFO.NumPlayers);
                break;
            case "element":
                child = gameObject.transform.GetChild(0).gameObject;
                GameInfo.GAMEINFO.curElem = child.GetComponent<InputField>().text;
                //GameInfo.GAMEINFO.createGameElement(child.GetComponent<InputField>().text);
                //Debug.Log(GameInfo.GAMEINFO.elements[elements.Capacity-1].name);
                break;
            case "cards":
                child = gameObject.transform.GetChild(1).gameObject;
                GameInfo.GAMEINFO.HasDeckOfCards = child.GetComponent<Toggle>().isOn;
                cur.PlacingCard = child.GetComponent<Toggle>().isOn;
                Debug.Log(child.GetComponent<Toggle>().isOn);
                break;
            case "amount":
                child = gameObject.transform.GetChild(4).gameObject;
                var temp = Int32.Parse(child.GetComponent<InputField>().text);
                if (temp is int)
                {
                    cur.StartingAmount = temp;
                }
                else
                {
                    cur.StartingAmount = -1;
                }
                //Int32.Parse(child.GetComponent<InputField>().text);
                Debug.Log(cur.StartingAmount);
                break;
            case "location":
                child = gameObject.transform.GetChild(0).gameObject;
                cur.LocName = child.GetComponent<InputField>().text;
                break;
            case "elem_type":
                child = gameObject.transform.GetChild(2).gameObject;
                cur.ElemType = child.GetComponent<Dropdown>().options[child.GetComponent<Dropdown>().value].text;
                //Debug.Log(cur.ElemType);
                break;
            case "loc1":
                child = gameObject.transform.GetChild(1).gameObject;
                cur.location1 = child.GetComponent<Dropdown>().options[child.GetComponent<Dropdown>().value].text;
                break;
            case "parser":
                child = gameObject.transform.GetChild(2).gameObject;
                cur.parseInput = child.GetComponent<InputField>().text;
                //cur.altLocations = child.GetComponent<InputField>().text.Split(',');
                break;
            case "handsize":
                child = gameObject.transform.GetChild(4).gameObject;
                int size;
                bool isInt = Int32.TryParse(child.GetComponent<InputField>().text, out size);
                if (isInt && size > 0)
                {
                    GameInfo.GAMEINFO.HasHandOfCards = true;
                    GameInfo.GAMEINFO.HandSize = size;
                }
                else
                {
                    GameInfo.GAMEINFO.HasHandOfCards = false;
                }
                break;
            case "wincon":
                child = gameObject.transform.GetChild(1).gameObject;
                GameInfo.GAMEINFO.curElem = child.GetComponent<InputField>().text;
                break;
        }
    }
}
