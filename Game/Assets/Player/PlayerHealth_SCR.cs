using UnityEngine;
using System.Collections;

public class PlayerHealth_SCR : MonoBehaviour 
{
    /* Publics */
    public int health;

    public void TakeDamage()
    {
        health--;
    }
}
