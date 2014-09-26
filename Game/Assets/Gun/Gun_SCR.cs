using UnityEngine;
using System.Collections;

public class Gun_SCR : MonoBehaviour 
{

	/* Publics */
    public GameObject bulletPreFab;

    public float damage;
    public float spread;
    public bool automatic;
    public float timeBetweenShots;
    public float knockBack;


    public int clipSize;
    public int bulletsInClip;
    public float reloadTime;


	/* Privates */
    private bool triggerUp = true;
    private bool shootWaitCanShoot = true;

	
	void Start () 
	{

	}
	
	void Update () 
	{
	    if (Input.GetMouseButtonUp(0))
        {
            triggerUp = true;
        }
	}

    // Shoot the gun
    public virtual void Shoot(Vector3 shootAt)
    {
        if (automatic && shootWaitCanShoot)
        {
            // Create bullet
            GameObject obj = GameObject.Instantiate(bulletPreFab, this.transform.position, Quaternion.identity) as GameObject;
            obj.GetComponent<Bullet_SCR>().damage = damage;

            // Add spread
            Vector3 newPos = new Vector3(shootAt.x + Random.RandomRange(-spread, spread), shootAt.y + Random.RandomRange(-spread, spread), 0);
            Vector3 dir = newPos - this.transform.parent.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle -= 90;
            obj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
             

            // Knock Back
            dir = shootAt - this.transform.parent.position;
            dir.Normalize();
            this.transform.parent.position = this.transform.position + (dir * -1 * knockBack);

            shootWaitCanShoot = false;
            StartCoroutine(ResetShootWait());
        }
        
        if (!automatic && triggerUp)
        {
            // Create bullet
            GameObject obj = GameObject.Instantiate(bulletPreFab, this.transform.position, Quaternion.identity) as GameObject;
            obj.GetComponent<Bullet_SCR>().damage = damage;

            // Add spread
            Vector3 newPos = new Vector3(shootAt.x + Random.RandomRange(-spread, spread), shootAt.y + Random.RandomRange(-spread, spread), 0);
            Vector3 dir = newPos - this.transform.parent.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle -= 90;
            obj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Knock Back
            dir = shootAt - this.transform.parent.position;
            dir.Normalize();
            this.transform.parent.position = this.transform.position + (dir * -1 * knockBack);

            triggerUp = false;
        }
    }

    IEnumerator ResetShootWait()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        shootWaitCanShoot = true;
    }
}