using UnityEngine;
using System.Collections;

public class DamagePlayer_SCR : MonoBehaviour {

    /* Publics */
    public float stunTime;

    /* Privates */

	// Use this for initialization
	void Start () 
    {

	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            coll.gameObject.GetComponent<PlayerHealth_SCR>().TakeDamage();
            coll.gameObject.GetComponent<PlayerMovement_SCR>().Stun(stunTime);
        }
    }
}
