using UnityEngine;
using System.Collections;

public class PlayerMovement_SCR : MonoBehaviour 
{
    /* Publics */
    public float movementSpeed;

    public GameObject bulletPrefab;

    /* Privates */
    private God_SCR god;
    private PlayerTouchMovement_SCR pTouchMovement;

	// Use this for initialization
	void Start () 
    {
        god = GameObject.Find("God").GetComponent<God_SCR>();
        pTouchMovement = this.GetComponent<PlayerTouchMovement_SCR>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (god.platform == "Editor")
        {
            PointAtMouse();
            CheckMovement();

            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        if (god.platform == "Ios")
        {
            pTouchMovement.CustomUpdate();
        }
	}

    // Points the player at the mouse or touch
    public void PointAtMouse()
    {
        Vector3 worldMousePos = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(0);
        worldMousePos.z = 0;
        this.transform.LookAt(worldMousePos);
    }

    // Move the player around
    void CheckMovement()
    {
        Vector3 newPos = new Vector3(this.transform.position.x + (Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime), this.transform.position.y + (Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime), 0);
        this.transform.position = newPos;
    }
}
