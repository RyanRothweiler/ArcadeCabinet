using UnityEngine;
using System.Collections;

public class BossItem_SCR : MonoBehaviour
{

	/* Publics */

	
	/* Privates */

    // Constructor
    public BossItem_SCR ()
    {

    }

    // Use this item, however it is intended to be used, will be different and unique for every item.
    // Also this contains some unique behavior for how the boss should act before and after using this item.
    public virtual void Use(LowLevelAI_SCR bossBrain) { }
}