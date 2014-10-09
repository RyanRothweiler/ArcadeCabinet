using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinder_SCR : MonoBehaviour
{

    /* Publics */
    public bool generatingPath;

    /* Privates */
    private Grid_SCR grid;


    void Start()
    {
        grid = GameObject.FindObjectOfType<Grid_SCR>();
    }

    // Returns a list of steps to get to the target.
    public List<Node_SCR> GetPath(Vector3 target, Vector3 start)
    {
        generatingPath = true;

        if (grid.allNodes.Length == 0)
        {
            Debug.Log("Please generate nodes... fool");
        }

        List<Node_SCR> path = new List<Node_SCR>();

        // Clean up anything that we did in the past
        grid.ResetForRegeneration();

        // Find the start and target nodes
        int startNodeX = 0;
        int startNodeY = 0;
        int targetNodeX = 0;
        int targetNodeY = 0;
        for (var x = 0; x <= grid.gridResolution; x++)
        {
            for (var y = 0; y <= grid.gridResolution; y++)
            {
                // Start node
                if (grid.allNodes[x, y].rectangle.Contains(start))
                {
                    grid.allNodes[x, y].visColor = Color.green;
                    startNodeX = x;
                    startNodeY = y;
                }
                // Target node
                if (grid.allNodes[x, y].rectangle.Contains(target))
                {
                    grid.allNodes[x, y].visColor = Color.green;
                    targetNodeX = x;
                    targetNodeY = y;
                }
            }
        }

        Node_SCR homeNode = grid.allNodes[startNodeX, startNodeY];
        grid.allNodes[startNodeX, startNodeY].open = false;
        grid.allNodes[startNodeX, startNodeY].opened = true;
        bool foundTarget = false;

        while (!foundTarget)
        {

            // --- Get open nodes around home adding them to the open list

            // North (maybe north, idk the actual orientation of the grid)
            CheckNode(homeNode, grid.allNodes[homeNode.gridLocX, homeNode.gridLocY + 1], new Vector2(targetNodeX, targetNodeY));

            // North East
            CheckNode(homeNode, grid.allNodes[homeNode.gridLocX + 1, homeNode.gridLocY - 1], new Vector2(targetNodeX, targetNodeY));

            // East
            CheckNode(homeNode, grid.allNodes[homeNode.gridLocX - 1, homeNode.gridLocY], new Vector2(targetNodeX, targetNodeY));

            // South East
            CheckNode(homeNode, grid.allNodes[homeNode.gridLocX - 1, homeNode.gridLocY - 1], new Vector2(targetNodeX, targetNodeY));

            // South
            CheckNode(homeNode, grid.allNodes[homeNode.gridLocX, homeNode.gridLocY - 1], new Vector2(targetNodeX, targetNodeY));

            // South West
            CheckNode(homeNode, grid.allNodes[homeNode.gridLocX - 1, homeNode.gridLocY + 1], new Vector2(targetNodeX, targetNodeY));

            // West
            CheckNode(homeNode, grid.allNodes[homeNode.gridLocX + 1, homeNode.gridLocY], new Vector2(targetNodeX, targetNodeY));

            // North West
            CheckNode(homeNode, grid.allNodes[homeNode.gridLocX + 1, homeNode.gridLocY + 1], new Vector2(targetNodeX, targetNodeY));


            // --- Calculate fScore of everything on the open list
            for (var i = 0; i < grid.openNodes.Count - 1; i++)
            {
                //F = G + H
                int g = (int)Vector2.Distance(homeNode.rectangle.center, grid.openNodes[i].rectangle.center);
                int h = (int)(Mathf.Abs(grid.openNodes[i].gridLocX - targetNodeX) + Mathf.Abs(grid.openNodes[i].gridLocY - targetNodeY)) * 5;
                grid.openNodes[i].gScore = g;
                grid.openNodes[i].fScore = g + h;
            }

            // Find the lowest fScore in the open list
            float lowest = 10000f;
            int index = 0;
            for (var i = 0; i < grid.openNodes.Count - 1; i++)
            {
                if (grid.openNodes[i].fScore < lowest)
                {
                    lowest = grid.openNodes[i].fScore;
                    index = i;
                }
            }


            // When we've run out of options
            if (grid.openNodes.Count == 0)
            {
                Debug.Log("Path not possible");
                return path;
            }

            // Close the Node with the lowest fScore and set it as home for the next round
            grid.allNodes[grid.openNodes[index].gridLocX, grid.openNodes[index].gridLocY].open = false;
            grid.allNodes[grid.openNodes[index].gridLocX, grid.openNodes[index].gridLocY].visColor = Color.blue;
            homeNode = grid.allNodes[grid.openNodes[index].gridLocX, grid.openNodes[index].gridLocY];
            grid.openNodes.RemoveAt(index);


            // Kickout when found location
            if (homeNode.gridLocX == targetNodeX && homeNode.gridLocY == targetNodeY)
            {
                foundTarget = true;
            }
        }

        // Create the final path
        bool pathing = true;
        Node_SCR nextNode = homeNode;
        while (pathing)
        {
            path.Add(nextNode);

            if (nextNode.parent == null)
            {
                pathing = false;
            }
            else
            {
                nextNode = nextNode.parent;
            }
        }

        generatingPath = false;
        return path;
    }

    private void CalculateScores(Node_SCR homeNode, int targetX, int targetY)
    {
        for (var i = 0; i < grid.openNodes.Count - 1; i++)
        {
            //F = G + H
            int g = (int)Vector2.Distance(homeNode.rectangle.center, grid.openNodes[i].rectangle.center);
            int h = (int)(Mathf.Abs(grid.openNodes[i].gridLocX - targetX) + Mathf.Abs(grid.openNodes[i].gridLocY - targetY));
            grid.openNodes[i].gScore = g;
            grid.openNodes[i].fScore = g + h;
        }
    }

    private void CheckNode(Node_SCR homeNode, Node_SCR nodeChecking, Vector2 targetLoc)
    {
        if (nodeChecking.walkable && nodeChecking.open)
        {
            if (!nodeChecking.opened)
            {
                grid.openNodes.Add(nodeChecking);
                grid.openNodes[grid.openNodes.Count - 1].parent = grid.allNodes[homeNode.gridLocX, homeNode.gridLocY];
                grid.openNodes[grid.openNodes.Count - 1].opened = true;
            }
            else if (nodeChecking.gScore < (int)Vector2.Distance(homeNode.rectangle.center, nodeChecking.rectangle.center))
            {
                homeNode = grid.allNodes[homeNode.gridLocX + 1, homeNode.gridLocY + 1];
                CalculateScores(homeNode, (int) targetLoc.x, (int) targetLoc.y);
            }
        }
    }
}
