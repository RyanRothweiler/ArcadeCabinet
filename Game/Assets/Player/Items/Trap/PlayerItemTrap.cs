using UnityEngine;
using System.Collections;

public class PlayerItemTrap : PlayerItem 
{
    /* Publics */
    public GameObject trapFab;

    /* Privates */

    public override void Use()
    {
        GameObject.Instantiate(trapFab, this.transform.position, Quaternion.identity);
    }
}
