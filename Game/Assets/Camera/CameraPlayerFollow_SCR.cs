using UnityEngine;
using System.Collections;

public class CameraPlayerFollow_SCR : MonoBehaviour 
{

	/* Publics */

	
	/* Privates */
    private GameObject focalPoint;
    private GameObject player;
	
	void Start () 
	{
        player = GameObject.Find("Player");
        focalPoint = GameObject.Find("CameraFocalPoint");
	}
	
	void Update () 
	{
        // Follow the focal point
        Vector3 newPos = focalPoint.transform.position;
        newPos.z = -10;
        this.transform.position = newPos;
	}
}