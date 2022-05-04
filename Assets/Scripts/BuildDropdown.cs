using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildDropdown : MonoBehaviour
{

    public Dropdown dropdown;
    public Dropdown altDropdown;
    public string type;
    // Start is called before the first frame update
    void Start()
    {
        dropdown.ClearOptions();
        Dropdown.OptionData elemOption = new Dropdown.OptionData();
        elemOption.text = null;
        dropdown.options.Add(elemOption);
        switch (type)
        {
            case "element":
                foreach (GameElement i in GameInfo.GAMEINFO.Elements)
                {
                    elemOption = new Dropdown.OptionData();
                    elemOption.text = i.Name;
                    dropdown.options.Add(elemOption);
                }
                if (GameInfo.GAMEINFO.HasDeckOfCards)
                {
                    Dropdown.OptionData cardOption = new Dropdown.OptionData();
                    cardOption.text = "Cards";
                    dropdown.options.Add(cardOption);
                }
                if (GameInfo.GAMEINFO.HasHandOfCards)
                {
                    Dropdown.OptionData cardOption = new Dropdown.OptionData();
                    cardOption.text = "Card Hand";
                    dropdown.options.Add(cardOption);
                }
                break;
            case "location":
                foreach (var i in ConstructedLocation.LOC_SETUP.TempLocNames)
                {
                    //string[] curLocName = i.Key.Split('_');              
                    elemOption = new Dropdown.OptionData();
                    elemOption.text = i.Key;//curLocName[1] + "_" + curLocName[0];
                    dropdown.options.Add(elemOption);
                    
                }
                break;

        }
        
    }

    public void checkIfCardOnClick()
    {
        
        dropdown.ClearOptions();
        Dropdown.OptionData elemOption = new Dropdown.OptionData();
        elemOption.text = null;
        dropdown.options.Add(elemOption);
        //If the selected location belongs to cards then display card options
        //string[] curSelectedLoc = altDropdown.options[altDropdown.value].text.Split('_');
        //if (ConstructedLocation.LOC_SETUP.TempLocNames[curSelectedLoc[1]+"_"+curSelectedLoc[0]] == "Cards")
        if (ConstructedLocation.LOC_SETUP.TempLocNames[altDropdown.options[altDropdown.value].text] == "Cards") 
        {
            elemOption = new Dropdown.OptionData();
            elemOption.text = "Draw and play to";
            dropdown.options.Add(elemOption);

            /*
            elemOption = new Dropdown.OptionData();
            elemOption.text = "Compare to card at";
            dropdown.options.Add(elemOption);
            */
        } else
        {
            elemOption = new Dropdown.OptionData();
            elemOption.text = "Move to location A";
            dropdown.options.Add(elemOption);
        }
    }

    public void checkIfCardLoc()
    {
       /* dropdown.ClearOptions();
        Dropdown.OptionData elemOption = new Dropdown.OptionData();
        elemOption.text = null;
        dropdown.options.Add(elemOption);
        foreach (var i in ConstructedLocation.LOC_SETUP.TempLocNames)
        {
            string cur = ConstructedLocation.LOC_SETUP.TempLocNames[altDropdown.options[altDropdown.value].text+"_player1"];
            //string altSelected = altDropdown.options[altDropdown.value].text;
   
            if (cur == i.Value)// && altSelected[altSelected.Length-1] == i.Key[i.Key.Length-1])
            {
                elemOption = new Dropdown.OptionData();
                elemOption.text = i.Key.Split('_')[0];
                dropdown.options.Add(elemOption);
            }
        }*/
    }

}
