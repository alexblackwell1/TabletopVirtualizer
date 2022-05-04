using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameElement //: MonoBehaviour
{
    // fields
    private string name;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public Dictionary<string, Vector2> locations = new Dictionary<string, Vector2>();
    public Sprite image;


    // Constructor
    public GameElement(string n)
    {
        name = n;
    }

    // locations SETTER
    public void setLocations(Dictionary<string,Vector2> lst) { locations = lst; }
    // locations GETTER
    public Dictionary<string, Vector2> getLocations() { return locations; }
    // image SETTER
    public void setImage(Sprite _image) { image = _image; }
    // image GETTTER
    public Sprite getImage() { return image; }

    public override string ToString()
    {
        return name;
    }
}
