using UnityEngine;
using System.Collections;

[System.Serializable]
public class Node_SCR  
{
    /* Publics */
    public bool walkable;
    public Rect rectangle;
    public Color visColor;

    /* Privates */


    // Constructor
    public Node_SCR(bool walkable, Rect rectangle)
    {
        this.walkable = walkable;
        this.rectangle = rectangle;


        if (walkable)
        {
            visColor = Color.white;
        }
        else
        {
            visColor = Color.red;
        }
    }
}
