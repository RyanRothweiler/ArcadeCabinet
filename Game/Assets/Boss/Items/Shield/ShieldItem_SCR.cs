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
        StartCoroutine(actualUse(bossBrain));
    }
    private IEnumerator actualUse(LowLevelAI_SCR bossBrain)
    {
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
        this.transform.parent.GetComponent<HighLevelAI_SCR>().makeDecision = true;
    }
}