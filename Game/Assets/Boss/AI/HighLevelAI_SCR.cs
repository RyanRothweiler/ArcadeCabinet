using UnityEngine;
using System.Collections;

public class HighLevelAI_SCR : MonoBehaviour 
{
    /* Publics */
     
    // Makes a decision on what to do next when true
    public bool makeDecision = true;


    /* Privates */

    private BossBody_SCR body;
    private LowLevelAI_SCR lowBrain;

	// Use this for initialization
	void Start () 
    {
        body = this.GetComponent<BossBody_SCR>();
        lowBrain = this.GetComponent<LowLevelAI_SCR>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (makeDecision)
        {
            MakeDecision();
        }
	}


    public void MakeDecision()
    {
        makeDecision = false;

        // Choose an item at random to use
        int useIndex = Random.Range(0, body.hands.Count);
        body.hands[useIndex].Use(lowBrain);
        Debug.Log("Using " + body.hands[useIndex].name);
    }
}
