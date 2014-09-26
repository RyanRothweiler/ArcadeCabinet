using UnityEngine;
using System.Collections;

public class ShieldItem_SCR : BossItem_SCR 
{
	/* Publics */
    public GameObject stunner;

	/* Privates */

	
	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}

    // Slams the shield on the grould stunning and damaging the player
    public override void Use()
    {
        Instantiate(stunner, this.transform.position, Quaternion.identity);
    }
}