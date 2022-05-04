using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConstructedElement : MonoBehaviour
{ 
/*    private static ConstructedElement _ELEM_SETUP;
    public static ConstructedElement ELEM_SETUP
    {
        get
        {
            if (_ELEM_SETUP == null)
            {
                _ELEM_SETUP = GameObject.FindObjectOfType<ConstructedElement>();
            }
            return _ELEM_SETUP;
        }
    }

    private static bool placingCard = false;
    public bool PlacingCard
    {
        get { return placingCard; }
        set { placingCard = value; }
    }
    public static string elemName = null;
    public static Sprite elemSprite = null;
    public static Dictionary<string, Vector2> elemLocation = new Dictionary<string, Vector2>();

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "4-Element") 
            setInputFields();
    }

    private static bool hasDeck = false;
    public bool HasDeck
    {
        get { return hasDeck; }
        set { hasDeck = value; }
    }
    
    // Start is called before the first frame update
    public void setElemName(string en)
    {
        elemName = en;
    }

    public void setElemImage(Sprite es)
    {
        elemSprite = es;
    }
    public Sprite getElemImage()
    {
        return elemSprite;
    }

    // Create the element if all fields filled in on button press
    public void buildElem()
    {
        if (elemName != null && elemSprite != null)
        {
            GameInfo.GAMEINFO.createGameElement(elemName, elemSprite);
            elemName = null;
            elemSprite = null;
            Debug.Log("Element created!");
            setInputFields();
        } 
        else
        {
            Debug.Log("Fields cannot be empty");
        }

    }

    private void setInputFields()
    {
        GameObject.Find("name_txtInput").GetComponent<InputField>().text = elemName;
        GameObject.Find("image_btn").GetComponent<Button>().GetComponent<Image>().sprite = elemSprite;
    }*/
}
