using UnityEngine;
using System.Collections;

public class ShieldItem_SCR : BossItem_SCR 
{
	/* Publics */
    public GameObject stunner;

	/* Privates */

	
	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}

    // Slams the shield on the grould stunning and damaging the player
    public override void Use(LowLevelAI_SCR bossBrain)
    {
        StartCoroutine(telegraph(bossBrain));
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
    private IEnumerator actualUse(LowLevelAI_SCR bossBrain)
    {
        beingUsed = true;

        bool looking = true;
        int lookingCount = 0;
        while (looking)
        {
            lookingCount++;
            // If cycled too many times kick out and do something else
            if (lookingCount > Random.Range(5,10))
            {
                Debug.Log("Using something else");
                looking = false;
            }

            // Stop moving to update player target pos
            bossBrain.StopMoving();

            // Not close to player, then move towards him
            if (Vector3.Distance(this.transform.position, bossBrain.god.player.transform.position) > 5)
            {
                if (!bossBrain.moving)
                {
                    bossBrain.MoveTo(bossBrain.god.player.transform.position);
                }
                yield return new WaitForSeconds(1f);
            }
            else
            {
                Instantiate(stunner, this.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1);
                looking = false;
            }
        }

        // Make new decision
        beingUsed = false;
        this.transform.parent.GetComponent<HighLevelAI_SCR>().makeDecision = true;
    }
}