using UnityEngine;
using System.Collections;

public class LowLevelAI_SCR : MonoBehaviour 
{
    /* Publics */


    /* Privates */
    private Pathfinder_SCR pathfinder;

	// Use this for initialization
	void Start () 
    {
        pathfinder = GameObject.Find("God").GetComponent<Pathfinder_SCR>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void MoveTo(Vector3 target)
    {
        pathfinder.GetPath(target);
    }
}
