using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LocationHandler : MonoBehaviour
{
    public ConstructedLocation locInfo;
    public DragAndDrop DaD;
    public Text textField;
   

    private static int locCount;
    private string curElem;
    private string curLoc;
    // Start is called before the first frame update
    void Awake()
    {
        curElem = locInfo.TempLocNames.ElementAt(0).Value;
        if (curElem == null)
        {
            curElem = "Cards";
        }
        //curElem = locInfo.TempLocNames.ElementAt(0).Value;
        curLoc = locInfo.TempLocNames.ElementAt(0).Key;
        locCount = 0;
        textField.text = "Click to create location for " + curLoc;
    }


    public int getLocationCords(Vector3 lV)
    {
        locCount++;
        if (curElem == "Cards")
        {
            //GameInfo.GAMEINFO.CardLocations.Add(curLoc, new Vector2(lV.x, lV.y));
            CardLocation cl = GameInfo.GAMEINFO.CardLocations[curLoc];
            cl.boardLocation = lV;

        }
        else if (curElem == "Card Hand")
        {
            HandLocation h1 = GameInfo.GAMEINFO.HandLocations[curLoc];
            h1.boardLocation = lV;
        }
        else
        {
            ElementLocation e1 = GameInfo.GAMEINFO.ElementLocations[curLoc];
            e1.boardLocation = lV;
            //GameElement cur = locInfo.BuiltElements.Find(x => x.Name == curElem);
            //cur.getLocations().Add(curLoc, new Vector2(lV.x, lV.y));
        }

        if (locCount < locInfo.TempLocNames.Count) {
            curElem = locInfo.TempLocNames.ElementAt(locCount).Value;
            curLoc = locInfo.TempLocNames.ElementAt(locCount).Key;
            textField.text = "Click to create location for " + curLoc;
            return locCount;
        }
        GameInfo.GAMEINFO.Elements = locInfo.BuiltElements;
        return -1;
    }
}
