using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid_SCR : MonoBehaviour 
{
    /* Publics */
    public int gridResolution;
    public List<Node_SCR> openNodes = new List<Node_SCR>();

    /* Private */


	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    // Visualize the grid and nodes
    void OnDrawGizmosSelected()
    {
        // --- Visualize the grid
        Gizmos.color = Color.white;

        float gridSpace = this.transform.localScale.x / (gridResolution + 1);
        Vector3 topLeft = new Vector3(this.transform.position.x - (this.transform.localScale.x / 2), this.transform.position.y + (this.transform.localScale.y / 2), 0);

        // Horizontal lines
        for (var i = 0; i < gridResolution + 1; i++)
        {
            Vector3 leftPoint = new Vector3(topLeft.x, topLeft.y - (i * gridSpace), 0);
            Gizmos.DrawLine(leftPoint, leftPoint + new Vector3(this.transform.localScale.x, 0, 0));
        }

        // Vertical lines
        for (var i = 0; i < gridResolution + 1; i++)
        {
            Vector3 topPoint = new Vector3(topLeft.x + (i * gridSpace), topLeft.y, 0);
            Gizmos.DrawLine(topPoint, topPoint - new Vector3(0, this.transform.localScale.y, 0));
        }

        // --- Visualize the nodes
        for (var i = 0; i < openNodes.Count; i++)
        {
            Gizmos.color = openNodes[i].visColor;
            Gizmos.DrawCube(openNodes[i].rectangle.center, new Vector3(openNodes[i].rectangle.width / 2, openNodes[i].rectangle.width / 2, openNodes[i].rectangle.width / 2));
        }
    }

    // Generates new nodes
    public void GenerateNodes()
    {
        openNodes.Clear();

        float gridSpace = this.transform.localScale.x / (gridResolution + 1);

        // Create nodes
        for (var x = 1; x < gridResolution + 2; x++)
        {
            for (var y = 1; y < gridResolution + 2; y++)
            {
                float rectLeft = (this.transform.position.x + (this.transform.localScale.x / 2)) - (x * (gridSpace));
                float rectRight = (this.transform.position.y + (this.transform.localScale.y / 2)) - (y * (gridSpace));
                openNodes.Add(new Node_SCR(true, new Rect(rectLeft, rectRight, gridSpace, gridSpace)));
            }
        }


        // Mark the not walkable ones
        for (var i = 0; i < openNodes.Count; i++)
        {
            Rect nodeRect = openNodes[i].rectangle;
            if (Physics2D.Linecast(nodeRect.center, new Vector3(nodeRect.x, nodeRect.y, 0), 1 << 11))
            {
                openNodes[i].walkable = false;
                openNodes[i].visColor = Color.red;
            }
            if (Physics2D.Linecast(nodeRect.center, new Vector3(nodeRect.xMax, nodeRect.yMax, 0), 1 << 11))
            {
                openNodes[i].walkable = false;
                openNodes[i].visColor = Color.red;
            }
            if (Physics2D.Linecast(nodeRect.center, new Vector3(nodeRect.x, nodeRect.yMax, 0), 1 << 11))
            {
                openNodes[i].walkable = false;
                openNodes[i].visColor = Color.red;
            }
            if (Physics2D.Linecast(nodeRect.center, new Vector3(nodeRect.xMax, nodeRect.y, 0), 1 << 11))
            {
                openNodes[i].walkable = false;
                openNodes[i].visColor = Color.red;
            }
        }


        Debug.Log("Nodes Generated");
    }

    // Removes all nodes
    public void ClearNodes()
    {
        openNodes.Clear();
        Debug.Log("Nodes Cleared");
    }
}
