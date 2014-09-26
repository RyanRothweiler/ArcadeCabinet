using UnityEngine;
using System.Collections;

public class Bullet_SCR : MonoBehaviour 
{
    /* Publics */
    public float movementSpeed;
    public GameObject bulletExplosion;
    public float damage;

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
    void OnCollisionEnter2D(Collision2D coll)
    {
        // Damage what this hit
        if (coll.gameObject.tag == "Boss")
        {
            coll.gameObject.GetComponent<BossBody_SCR>().TakeDamate(damage);
        }

        GameObject obj = GameObject.Instantiate(bulletExplosion, coll.contacts[0].point, this.transform.rotation) as GameObject;
        obj.transform.Rotate(new Vector3(90, 0, 0));
        Destroy(this.gameObject);
    }

    // Move the bullet without letting it pass through things
    void MoveBullet()
    {
        Vector3 newPos = this.transform.position + (this.transform.up * movementSpeed * Time.deltaTime);

        Ray ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Vector3.Distance(this.transform.position, newPos)))
        {
            this.transform.position = hit.point;
        }
        else
        {
            this.transform.position = newPos;
        }
    }
}