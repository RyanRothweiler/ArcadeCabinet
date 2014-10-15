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
        StartCoroutine(telegraph(lowBrain));
    }

    // Telegraph that we're using this item before using it
    private IEnumerator telegraph(LowLevelAI_SCR lowBrain)
    {
        // Scale up
        Vector3 orig = this.transform.localScale;
        Vector3 target = this.transform.localScale * 2;
        bool going = true;
        while (going)
        {
            yield return new WaitForFixedUpdate();

            this.transform.localScale = Vector3.Lerp(this.transform.localScale, target, Time.deltaTime * 10);
            
            if ((this.transform.localScale.x) > target.x - 0.1)
            {
                going = false;
            }
        }

        // Scale down
        going = true;
        while (going)
        {
            yield return new WaitForFixedUpdate();

            this.transform.localScale = Vector3.Lerp(this.transform.localScale, orig, Time.deltaTime * 10);

            if ((this.transform.localScale.x) < orig.x + 0.1)
            {
                going = false;
            }
        }

        StartCoroutine(actualUse(lowBrain));
        yield return new WaitForSeconds(0);
    }

    private IEnumerator actualUse(LowLevelAI_SCR lowBrain)
    {
        beingUsed = true;

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

        if (!shooting)
        {
            beingUsed = false;
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
        laserSight.transform.localScale = new Vector3(1, hitDist / (transform.localScale.x), 1);

        yield return new WaitForSeconds(1f);

        laserSight.SetActive(false);
        laserLaser.SetActive(true);
        laserLaser.transform.localScale = new Vector3(1, hitDist / (transform.localScale.x), 1);

        yield return new WaitForSeconds(1);

        laserSight.SetActive(false);
        laserLaser.SetActive(false);

        shooting = false;

        beingUsed = false;
    }
}
