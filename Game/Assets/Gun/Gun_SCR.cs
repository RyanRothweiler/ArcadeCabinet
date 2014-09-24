using UnityEngine;
using System.Collections;

public class Gun_SCR : MonoBehaviour 
{

	/* Publics */
    public GameObject bulletPreFab;

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
    public void Shoot(Vector3 shootAt)
    {
        if (automatic && shootWaitCanShoot)
        {
            // Create bullet
            GameObject obj = GameObject.Instantiate(bulletPreFab, this.transform.position, Quaternion.identity) as GameObject;

            // Add spread
            Vector3 newDirection = new Vector3(shootAt.x + Random.RandomRange(-spread, spread), shootAt.y + Random.RandomRange(-spread, spread), 0);
            obj.transform.LookAt(newDirection);

            // Knock Back
            this.transform.parent.position = this.transform.position + (this.transform.parent.forward * -1 * knockBack);

            shootWaitCanShoot = false;
            StartCoroutine(ResetShootWait());
        }
        
        if (!automatic && triggerUp)
        {
            // Create bullet
            GameObject obj = GameObject.Instantiate(bulletPreFab, this.transform.position, Quaternion.identity) as GameObject;

            // Add spread
            Vector3 newDirection = new Vector3(shootAt.x + Random.RandomRange(-spread, spread), shootAt.y + Random.RandomRange(-spread, spread), 0);
            obj.transform.LookAt(newDirection);

            // Knock Back
            this.transform.parent.position = this.transform.position + (this.transform.parent.forward * -1 * knockBack);

            triggerUp = false;
        }
    }

    IEnumerator ResetShootWait()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        shootWaitCanShoot = true;
    }
}