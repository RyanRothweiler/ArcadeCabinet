using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerItemControls : MonoBehaviour
{

    /* Publics */
    public List<PlayerItem> hands = new List<PlayerItem>();


    /* Privates */


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check for item usage
        if (Input.GetButtonDown("PlayerUse"))
        {
            UseItem();
        }
    }

    // Use the equipped item
    private void UseItem()
    {
        Debug.Log("use");
    }
}
