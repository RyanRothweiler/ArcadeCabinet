using UnityEngine;
using System.Collections;

public class DeathTimer : MonoBehaviour {

    public float secondsAlive;

	// Use this for initialization
	void Start () 
    {
        Destroy(this.gameObject, secondsAlive);
	}
}
