using UnityEngine;
using System.Collections;

public class Bullet_SCR : MonoBehaviour 
{
    /* Publics */
    public float movementSpeed;
    public GameObject bulletExplosion;

    /* Privates */


	// Use this for initialization
	void Start () 
    {
        Destroy(this.gameObject, 3);
	}
	
	// Update is called once per frame
	void Update () 
    {
        MoveBullet();
	}

    // Hitting something
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Wall")
        {
            GameObject obj = GameObject.Instantiate(bulletExplosion, coll.contacts[0].point, this.transform.rotation) as GameObject;
            obj.transform.Rotate(new Vector3(0, 180, 0));
            Destroy(this.gameObject);
        }
    }

    // Move the bullet without letting it pass through things
    void MoveBullet()
    {
        Vector3 newPos = this.transform.position + (this.transform.forward * movementSpeed * Time.deltaTime);
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Vector3.Distance(this.transform.position, newPos)))
        {
            this.transform.position = hit.point;
        }
        else
        {
            this.transform.position = this.transform.position + (this.transform.forward * movementSpeed * Time.deltaTime);
        }
    }
}