using UnityEngine;
using System.Collections;

[System.Serializable]
public class Node_SCR  
{
    /* Publics */
    public bool walkable;
    public Rect rectangle;
    public Color visColor;

    public Node_SCR parent;

    public int gridLocX;
    public int gridLocY;

    public float fScore;
    public float gScore;

    public bool open;
    public bool opened;

    /* Privates */


    // Constructor
    public Node_SCR(bool walkable, Rect rectangle, int x, int y)
    {
        this.walkable = walkable;
        this.rectangle = rectangle;
        this.gridLocX = x;
        this.gridLocY = y;

        fScore = 0;
        gScore = 0;

        opened = false;
        open = true;

        if (walkable)
        {
            visColor = Color.white;
        }
        else
        {
            visColor = Color.red;
        }
    }

    public void Reinitialize()
    {
        fScore = 0;
        gScore = 0;

        opened = false;
        open = true;

        parent = null;
    }
}
