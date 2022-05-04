using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;

public class DragAndDrop : MonoBehaviour
{
    public GameObject elemImage;
    public SceneChanger sc;
    public LocationHandler lh;

    //public string curElem;
    //public string curLoc;


    bool canMove;
    bool dragging;
    Collider2D collider;
    void Start()
    {
        //Debug.Log("CurElem = " + curElem);
        string curElem = ConstructedLocation.LOC_SETUP.TempLocNames.ElementAt(0).Value;
       
        if (curElem == "Cards")
        {
            Sprite cardBack = Resources.Load<Sprite>("GameElements/cardBack");
            elemImage.GetComponent<Image>().sprite = cardBack;
        }
        else if (curElem == "Card Hand")
        {
            Sprite hand = Resources.Load<Sprite>("GameElements/handImage");
            elemImage.GetComponent<Image>().sprite = hand;
            elemImage.gameObject.transform.localScale = new Vector3((float)1, (float).5, 1);
        }
        else
        {
            elemImage.GetComponent<Image>().sprite = GameInfo.GAMEINFO.Elements.Find(x => x.Name == curElem).getImage();
            //elemImage.GetComponent<Image>().sprite = GameInfo.GAMEINFO.Elements.Find(x => x.Name == curElem).getImage();//ConstructedElement.ELEM_SETUP.getElemImage();
        }
        collider = GetComponent<Collider2D>();
        canMove = false;
        dragging = false;

    }

    GameObject go;

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            if (ConstructedLocation.LOC_SETUP.PlacingCard)
            {
                //GameInfo.GAMEINFO.CardLocations = ConstructedElement.ELEM_SETUP.getCardLocations();
                ConstructedLocation.LOC_SETUP.PlacingCard = false;
            }
            //sc.LoadNextScreen("4-Element");
        }

        else if (Input.GetMouseButtonUp(0))
        {
            canMove = false;
            dragging = false;
            // send the position
            //Debug.Log("Location: "+go.transform.position.x+" "+ go.transform.position.y);
            int c = lh.getLocationCords(go.transform.position);
            //if all elements placed load next screen
            if (c < 0)
            {
                sc.LoadNextScreen("6-LocationBehavior");
            } 
            else
            {
                string curElem = ConstructedLocation.LOC_SETUP.TempLocNames.ElementAt(c).Value;
                if (curElem == "Cards")
                {
                    elemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("GameElements/cardBack");
                    //elemImage.gameObject.transform.localScale = new Vector3((float).9, (float).9, 1);
                }
                else if (curElem == "Card Hand")
                {
                    elemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("GameElements/handImage");
                    elemImage.gameObject.transform.localScale = new Vector3((float)1, (float).5, 1);
                }
                else
                {
                    elemImage.GetComponent<Image>().sprite = GameInfo.GAMEINFO.Elements.Find(x => x.Name == curElem).getImage();
                }
                
            }            
        }
        else if (dragging)
        {
            go.transform.position = mousePos;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            go = Instantiate(elemImage);
            go.transform.SetParent(GameObject.Find("elem_img").transform, false);

            if (collider == Physics2D.OverlapPoint(mousePos))
            {
                canMove = true;
            }
            else
            {
                canMove = false;
            }
            if (canMove)
            {
                dragging = true;
            }


        }
    }
}