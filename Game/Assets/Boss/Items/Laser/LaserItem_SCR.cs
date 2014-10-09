using UnityEngine;
using System.Collections;

public class LaserItem_SCR : BossItem_SCR 
{

    /* Publics */
    public GameObject laserLaser;
    public GameObject laserSight;

    /* Privates */
    private GameObject player;
    public bool shooting;

    void Start()
    {
        player = GameObject.Find("Player");

        laserSight.SetActive(false);
        laserLaser.SetActive(false);

        shooting = false;
    }

    public override void Use(LowLevelAI_SCR lowBrain)
    {
        StartCoroutine(actualUse(lowBrain));
    }

    private IEnumerator actualUse(LowLevelAI_SCR lowBrain)
    {
        // Try to get in line of sight
        bool looking = true;
        int lookingCount = 0;
        while (looking)
        {
            yield return new WaitForSeconds(0);
            lookingCount++;

            if (!lowBrain.moving && !shooting)
            {
                // Pick a random place to move to
                Vector3 target = new Vector3(this.transform.parent.position.x + Random.Range(-20, 20), this.transform.parent.position.y + Random.Range(-20, 20), 0);
                lowBrain.MoveTo(target);
            }

            // If can see player then shoot the laser
            Vector3 dir = player.transform.position - this.transform.position;
            Ray2D ray = new Ray2D(this.transform.position, dir * 5);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 1000);

            if (hit.collider.name == "Player" && !shooting)
            {
                looking = false;
                StartCoroutine(ShootLaser());
                lowBrain.StopMoving();
            }

            // If cycled too many times kick out and do something else
            if (lookingCount > Random.Range(5,20))
            {
                Debug.Log("Using something else");
                looking = false;
            }
        }

        this.transform.parent.GetComponent<HighLevelAI_SCR>().makeDecision = true;
    }

    IEnumerator ShootLaser()
    {
        shooting = true;

        // Point the laser at the player
        Vector3 dir = player.transform.position - this.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle -= 90;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Find the position for the laser to hit
        Ray2D ray = new Ray2D(this.transform.position, dir * 5);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 1000, 1 << 11);
        float hitDist = Vector3.Distance(this.transform.position, hit.point);

        laserSight.SetActive(true);
        laserSight.transform.localScale = new Vector3(1, hitDist/2.6f, 1);

        yield return new WaitForSeconds(1.5f);

        laserSight.SetActive(false);
        laserLaser.SetActive(true);
        laserLaser.transform.localScale = new Vector3(1, hitDist / 2.6f, 1);

        yield return new WaitForSeconds(1);

        laserSight.SetActive(false);
        laserLaser.SetActive(false);

        shooting = false;

    }
}
