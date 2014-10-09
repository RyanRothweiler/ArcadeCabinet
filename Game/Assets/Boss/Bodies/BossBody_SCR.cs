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

    public bool takingDamage;

    public float movementSpeed;
	
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
    }

    public void TakeDamate(float amount)
    {
        currHealth -= amount;
        takingDamage = true;
        StartCoroutine(ResetTakingDamage());
    }

    IEnumerator ResetTakingDamage()
    {
        yield return new WaitForSeconds(0.5f);
        takingDamage = false;
    }

    void UpdateHealth()
    {
        healthBar.transform.localScale = new Vector3(currHealth / maxHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}