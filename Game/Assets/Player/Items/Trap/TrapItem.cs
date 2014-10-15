using UnityEngine;
using System.Collections;

public class TrapItem : MonoBehaviour 
{
    /* Publics */


    /* Privates */
    private GameObject outliner;

	// Use this for initialization
	void Start () 
    {
        outliner = this.transform.FindChild("TrapOutline").gameObject;
	}
	
	// Update is called once per frame
	void Update () 
    {
        outliner.transform.Rotate(new Vector3(0.1f, 0, 0));
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log(coll.gameObject.name);
    }
}
