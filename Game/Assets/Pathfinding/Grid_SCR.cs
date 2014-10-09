using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid_SCR : MonoBehaviour 
{
    /* Publics */
    public int gridResolution;
    public Node_SCR[,] allNodes;
    public List<Node_SCR> openNodes = new List<Node_SCR>();

    /* Private */


	// Use this for initialization
	void Start () 
    {
        GenerateNodes();
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

        if (Application.isPlaying)
        {
            for (var x = 0; x <= gridResolution; x++)
            {
                for (var y = 0; y <= gridResolution; y++)
                {
                    Gizmos.color = allNodes[x, y].visColor;
                    Gizmos.DrawCube(allNodes[x, y].rectangle.center, new Vector3(allNodes[x, y].rectangle.width / 2, allNodes[x, y].rectangle.width / 2, allNodes[x, y].rectangle.width / 2));
                }
            }
        }
    }

    // Generates new nodes
    public void GenerateNodes()
    {
        allNodes = new Node_SCR[gridResolution + 1, gridResolution + 1];
        openNodes.Clear();

        float gridSpace = this.transform.localScale.x / (gridResolution + 1);

        // Create nodes
        for (var x = 0; x < gridResolution + 1; x++)
        {
            for (var y = 0; y < gridResolution + 1; y++)
            {
                float rectLeft = (this.transform.position.x + (this.transform.localScale.x / 2)) - ((x + 1) * (gridSpace));
                float rectRight = (this.transform.position.y + (this.transform.localScale.y / 2)) - ((y + 1) * (gridSpace));
                allNodes[x,y] = new Node_SCR(true, new Rect(rectLeft, rectRight, gridSpace, gridSpace), x, y);
            }
        }


        // Mark the not walkable ones
        for (var x = 0; x <= gridResolution; x++)
        {
            for (var y = 0; y <= gridResolution; y++)
            {
                Rect nodeRect = allNodes[x, y].rectangle;
                if (Physics2D.Linecast(nodeRect.center, new Vector3(nodeRect.x, nodeRect.y, 0), 1 << 11))
                {
                    allNodes[x, y].walkable = false;
                    allNodes[x, y].visColor = Color.red;
                }
                if (Physics2D.Linecast(nodeRect.center, new Vector3(nodeRect.xMax, nodeRect.yMax, 0), 1 << 11))
                {
                    allNodes[x, y].walkable = false;
                    allNodes[x, y].visColor = Color.red;
                }
                if (Physics2D.Linecast(nodeRect.center, new Vector3(nodeRect.x, nodeRect.yMax, 0), 1 << 11))
                {
                    allNodes[x, y].walkable = false;
                    allNodes[x, y].visColor = Color.red;
                }
                if (Physics2D.Linecast(nodeRect.center, new Vector3(nodeRect.xMax, nodeRect.y, 0), 1 << 11))
                {
                    allNodes[x, y].walkable = false;
                    allNodes[x, y].visColor = Color.red;
                }
            }
        }

        Debug.Log("Nodes Generated");
    }

    // Get things ready to generate another path
    public void ResetForRegeneration()
    {
        openNodes.Clear();

        for (var x = 0; x <= gridResolution; x++)
        {
            for (var y = 0; y <= gridResolution; y++)
            {
                allNodes[x, y].Reinitialize();
            }
        }
    }
}