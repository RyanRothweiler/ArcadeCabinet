using UnityEngine;
using System.Collections;

public class PlayerItem : MonoBehaviour 
{

    /* Publics */


    /* Privates */


    // Constructor
    public PlayerItem ()
    {

    }

    // Use this item, however it is intended to be used, will be different and unique for every item.
    public virtual void Use() { }
}