using UnityEngine;
using System.Collections;

public class Gun_SCR : MonoBehaviour 
{

	/* Publics */
    public GameObject bulletPreFab;

    public float spread;
    public bool automatic;
    public float timeBetweenShots;


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

            triggerUp = false;
        }
    }

    IEnumerator ResetShootWait()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        shootWaitCanShoot = true;
    }
}