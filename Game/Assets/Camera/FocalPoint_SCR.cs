using UnityEngine;
using System.Collections;

public class FocalPoint_SCR : MonoBehaviour 
{

	/* Publics */

	
	/* Privates */
    private GameObject player;
    private GameObject aimer;
	
	void Start () 
	{
        player = GameObject.Find("Player");
        aimer = player.transform.FindChild("Aimer").gameObject;
	}
	
	void Update () 
	{
        this.transform.position = Vector3.Lerp(this.transform.position, player.transform.position, Time.deltaTime * 6);
	}
}