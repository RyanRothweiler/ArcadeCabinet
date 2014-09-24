using UnityEngine;
using System.Collections;

public class PlayerShooting_SCR : MonoBehaviour 
{
    /* Publics */
    public bool aiming;
    public Gun_SCR equippedGun;

    /* Privates */
    private GameObject aimer;

	// Use this for initialization
	void Start () 
    {
        aimer = this.transform.FindChild("Aimer").gameObject;
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Shooting
	    if (Input.GetMouseButton(0) && aiming)
        {
			Vector3 worldMousePos = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(0);
			worldMousePos.z = 0;
            equippedGun.Shoot(worldMousePos);
        }

        // Aiming
        if (Input.GetMouseButton(1))
        {
            Vector3 worldMousePos = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(0);
            worldMousePos.z = 0;
            aimer.transform.position = worldMousePos;

            aiming = true;
            aimer.SetActive(true);
        }
        else
        {
            aiming = false;
            aimer.SetActive(false);
        }
	}
}
