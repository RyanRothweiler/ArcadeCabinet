using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LowLevelAI_SCR : MonoBehaviour 
{
    /* Publics */
    public God_SCR god;
    public bool moving;

    /* Privates */
    private Pathfinder_SCR pathfinder;
    private BossBody_SCR body;

    private GameObject target;
    private Vector3 targetLastPos;

	// Use this for initialization
	void Start () 
    {
        god = GameObject.Find("God").GetComponent<God_SCR>();
        pathfinder = god.GetComponent<Pathfinder_SCR>();
        body = this.GetComponent<BossBody_SCR>();
        moving = false;
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    // Instantly stop pathfinding
    public void StopMoving()
    {
        moving = false;
        StopAllCoroutines();
    }

    // Wrapper thing for moving
    public void MoveTo(Vector3 target)
    {
        StartCoroutine(actualMove(target));
    }

    // Actually does the moving
    private IEnumerator actualMove(Vector3 target)
    {
        moving = true;

        // Get the path to the target
        List<Node_SCR> pathToTarget = pathfinder.GetPath(target, this.transform.position);

        // Follow steps to the target
        for (var i = pathToTarget.Count - 1; i > 1; i--)
        {
            // Wait until the boss gets to the next path point
            while (Vector3.Distance(this.transform.position, new Vector3(pathToTarget[i].rectangle.x, pathToTarget[i].rectangle.y, 0)) > 0.1)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(pathToTarget[i].rectangle.x, pathToTarget[i].rectangle.y, 0), body.movementSpeed / 100);
                yield return new WaitForSeconds(0.01f);
            }
        }

        moving = false;
    }
}
