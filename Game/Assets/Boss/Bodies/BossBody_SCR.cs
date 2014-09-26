using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossBody_SCR : MonoBehaviour
{

	/* Publics */
    public float currHealth;
    public float maxHealth;
    public List<BossItem_SCR> hands = new List<BossItem_SCR>();
    public GameObject healthBar;
	
	/* Privates */

    private bool canUse = true;

    // Constructor
    public BossBody_SCR ()
    {

    }

    public void Start()
    {
        for (var i = 0; i < hands.Count; i++)
        {
            hands[i].transform.parent = this.transform;
        }
    }

    public void Update()
    {
        UpdateHealth();

        // testing
        if (canUse)
        {
            canUse = false;
            hands[1].Use();
            StartCoroutine(UseReset());
        }

    }

    IEnumerator UseReset()
    {
        yield return new WaitForSeconds(0);
        canUse = true;
    }

    public void TakeDamate(float amount)
    {
        currHealth -= amount;
    }

    void UpdateHealth()
    {
        healthBar.transform.localScale = new Vector3(currHealth / maxHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}