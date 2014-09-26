using UnityEngine;
using System.Collections;

public class LaserItem_SCR : BossItem_SCR 
{

    /* Publics */
    public GameObject laserLaser;
    public GameObject laserSight;

    /* Privates */
    private GameObject player;
    private bool readyToShoot = true;

    void Start()
    {
        player = GameObject.Find("Player");

        laserSight.SetActive(false);
        laserLaser.SetActive(false);
    }

    public override void Use()
    {
        //if (readyToShoot)
        //{
            readyToShoot = false;
            StartCoroutine(ShootLaser());
        //}
    }

    IEnumerator ShootLaser()
    {
        //// Point the laser at the player
        Vector3 dir = player.transform.position - this.transform.position;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //angle -= 90;
        //this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Find the position for the laser to hit
        Ray2D ray = new Ray2D(this.transform.position, dir * 5);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        float hitDist = Vector3.Distance(this.transform.position, hit.point);

        //laserSight.SetActive(true);
        //laserSight.transform.localScale = new Vector3(1, hitDist, 1);

        yield return new WaitForSeconds(1.5f);

        //laserSight.SetActive(false);
        //laserLaser.SetActive(true);
        //laserLaser.transform.localScale = new Vector3(1, hitDist, 1);

        //yield return new WaitForSeconds(1);

        //laserSight.SetActive(false);
        //laserLaser.SetActive(false);
        //readyToShoot = true;

    }
}
