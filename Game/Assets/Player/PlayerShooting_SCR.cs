using UnityEngine;
using System.Collections;

public class PlayerShooting_SCR : MonoBehaviour 
{
    /* Publics */
    public GameObject bulletPreFab;

    /* Privates */
    //private God_SCR god;

	// Use this for initialization
	void Start () 
    {
        // Find god
        //god = GameObject.Find("God").GetComponent<God_SCR>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (Input.GetMouseButtonDown(0))
        {
			Vector3 worldMousePos = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(0);
			worldMousePos.z = 0;
            Shoot(worldMousePos);
        }
	}

    // Shoot the gun
    public void Shoot(Vector3 shootAt)
    {
        GameObject obj = GameObject.Instantiate(bulletPreFab, this.transform.position, Quaternion.identity) as GameObject;
		obj.transform.LookAt(shootAt);
    }
}
