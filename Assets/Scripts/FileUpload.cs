using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using System.IO;
using System.Globalization;
using System.Linq;

public class FileUpload : MonoBehaviour
{
    public GameObject button;
    private Sprite selectedImage;
    public String buttonName;

    void Start()
    {
        //button = GameObject.Find("Canvas").transform.Find(buttonName).gameObject;
    }

    public void loadImage(string imageOf)
    {
        selectedImage = openExplorer(imageOf);
        if (selectedImage == null) return;
        switch (imageOf) 
        {
            case "Boards":
                button.GetComponent<Image>().sprite = selectedImage;
                button.GetComponentInChildren<Text>().enabled = false;
                GameInfo.GAMEINFO.setBoard(selectedImage);
                break;
            case "GameElements":
                button.GetComponent<Image>().sprite = selectedImage;
                button.GetComponentInChildren<Text>().enabled = false;
                GameInfo.GAMEINFO.curImg = selectedImage;
                //ConstructedElement.ELEM_SETUP.setElemImage(selectedImage);
                break;
        }
    }

    public Sprite openExplorer(string imageOf)
    {
        //Open file explorer and look for jpg/png file types
        string filePath = EditorUtility.OpenFilePanel("Game Board", "", "jpg,png");
        //If file path exists
        if (filePath.Length != 0)
        {
            Debug.Log(filePath);
            string[] splitF = filePath.Split('/');
            string fname = splitF[splitF.Length - 1];
            GameInfo.GAMEINFO.BoardName = fname;
            //If the file is not already present in the the directory
            if (!File.Exists("Assets/Resources/" + imageOf + "/" + fname))
            {
                //Copy the image into the Boards folder with its name
                File.Copy(filePath, "Assets/Resources/"+ imageOf + "/" + fname);
                
            }
            return convertToSprite("Assets/Resources/" + imageOf + "/" + fname);
        }
        return null;
    }

    // Code from https://forum.unity.com/threads/generating-sprites-dynamically-from-png-or-jpeg-files-in-c.343735/
    private Sprite convertToSprite(string filePath)
    {
        Texture2D SpriteTexture = LoadTexture(filePath);
        Sprite NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), 100.0f, 0, SpriteMeshType.FullRect);
        return NewSprite;
    }

    private Texture2D LoadTexture(string FilePath)
    {
        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);
            if (Tex2D.LoadImage(FileData))
            {
                return Tex2D;
            }
        }
        return null;
    }

    public void uploadGameInfo(string filename)
    {
        // init gameinfo
        string[] lines = System.IO.File.ReadAllLines(filename);
        int c = 0;
Debug.Log(lines[c]);
        GameInfo.GAMEINFO.GameName = lines[c++];
Debug.Log(lines[c]);
        GameInfo.GAMEINFO.NumPlayers = int.Parse(lines[c++]);
Debug.Log(lines[c]);
        // TODO: board image
        if (lines[c] != " " && File.Exists("Assets/Resources/Boards/" +lines[c]))
        {
            GameInfo.GAMEINFO.setBoard(convertToSprite("Assets/Resources/Boards/" + lines[c]));
        }
        else
        {
            GameInfo.GAMEINFO.setBoard(convertToSprite("Assets/Resources/Boards/wood-600x480.jpg"));
        }
        c++;
        GameInfo.GAMEINFO.HasDeckOfCards = bool.Parse(lines[c++]);
        GameInfo.GAMEINFO.HasHandOfCards = bool.Parse(lines[c++]);
        GameInfo.GAMEINFO.HandSize = int.Parse(lines[c++]);
        // GameElements
        while (lines[c] != "~")
            GameInfo.GAMEINFO.Elements.Add(new GameElement(lines[c++]));
        c++; // get past '~'
        // HandLocations
        while (lines[c] != "element")
        {
            string name = lines[c++];
            HandLocation hl = new HandLocation(lines[c++], int.Parse(lines[c++]), int.Parse(lines[c++]));
            float x = float.Parse(lines[c++], CultureInfo.InvariantCulture);
            float y = float.Parse(lines[c++], CultureInfo.InvariantCulture);
            float z = float.Parse(lines[c++], CultureInfo.InvariantCulture);
            hl.boardLocation = new Vector3(x, y, z);
            // get condition/action pairs
            while (lines[c] != "~")
            {
                // Condition
                List<string> _ifI = lines[c++].Split(',').ToList();
                CardCondition cc = new CardCondition(_ifI, bool.Parse(lines[c++]));
                // Action
                List<CardAction> ca = new List<CardAction>();
                while (lines[c] != "~")
                {
                    ca.Add(new CardAction(lines[c++].Split(',').ToList()));
                }
                hl.addCondActPair(cc, ca);
                c++; // get past '~'
            }
            c++; // get past '~'
            GameInfo.GAMEINFO.HandLocations.Add(name, hl);
            GameInfo.GAMEINFO.StartingAmount.Add(int.Parse(lines[c++]));
        }
        c++; // get past 'element'
        // ElementLocations
        while (lines[c] != "card")
        {
            c++;
        }
        c++; // get past 'card'
        // CardLocations
        while (lines[c] != "end of locations")
        {
            string name = lines[c++];
            CardLocation cl = new CardLocation(lines[c++], int.Parse(lines[c++]), int.Parse(lines[c++]), bool.Parse(lines[c++]));
            float x = float.Parse(lines[c++], CultureInfo.InvariantCulture);
            float y = float.Parse(lines[c++], CultureInfo.InvariantCulture);
            float z = float.Parse(lines[c++], CultureInfo.InvariantCulture);
            cl.boardLocation = new Vector3(x, y, z);
            // get condition/action pairs
            while (lines[c] != "~")
            {
                // Condition
                List<string> _ifI = lines[c++].Split(',').ToList();
                CardCondition cc = new CardCondition(_ifI, bool.Parse(lines[c++]));
                // Action
                List<CardAction> ca = new List<CardAction>();
                while (lines[c] != "~")
                {
                    ca.Add(new CardAction(lines[c++].Split(',').ToList()));
                }
                cl.addCondActPair(cc, ca);
                c++; // get past '~'
            }
            c++; // get past '~'
            GameInfo.GAMEINFO.CardLocations.Add(name, cl);
            GameInfo.GAMEINFO.StartingAmount.Add(int.Parse(lines[c++]));
        }
        c++; // get past 'end of locations'
        // WinCondition
        WinCondition wc = new WinCondition();
        while (lines[c] != "~")
        {
            wc.addCondition(new GameCondition(lines[c++], lines[c++]));
        }
        GameInfo.GAMEINFO.WinConditions = wc;
    }

    public void importGame()
    {
        //Open file explorer and look for jpg/png file types
        string filePath = EditorUtility.OpenFilePanel("Game Info", "", "wme");
        //If file path exists
        if (filePath.Length != 0)
        {
            Debug.Log(filePath);
            string[] splitF = filePath.Split('/');
            string fname = splitF[splitF.Length - 1];

            //If the file is not already present in the the directory
            if (!File.Exists("Assets/Games/" + fname))
            {
                //Copy the image into the Boards folder with its name
                File.Copy(filePath, "Assets/Games/" + fname);
            }
            uploadGameInfo(filePath);
        }
        return;
    }
}
