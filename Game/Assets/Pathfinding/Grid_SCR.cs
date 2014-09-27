using UnityEngine;
using System.Collections;

public class Grid_SCR : MonoBehaviour 
{
    /* Publics */
    public float gridResolution;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    // Visualize the grid
    void OnDrawGizmosSelected()
    {
        for (var i = 0; i < gridResolution; i++)
        {
            //Gizmos.DrawLine()
        }

        for (var i = 0; i < gridResolution; i++)
        {

        }
    }
}
