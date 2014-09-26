using UnityEngine;
using System.Collections;

public class PlayerMovement_SCR : MonoBehaviour 
{
    /* Publics */
    public float movementSpeed;

    public GameObject bulletPrefab;

    /* Privates */
    private God_SCR god;
    private PlayerTouchControls_SCR pTouchMovement;
    private PlayerShooting_SCR pShooting;

    private bool stunned = false;

    private float maxMovementSpeed;

	// Use this for initialization
	void Start () 
    {
        god = GameObject.Find("God").GetComponent<God_SCR>();
        pTouchMovement = this.GetComponent<PlayerTouchControls_SCR>();
        pShooting = this.GetComponent<PlayerShooting_SCR>();

        maxMovementSpeed = movementSpeed;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!stunned)
        {
            // Slower when aiming
            if (pShooting.aiming)
            {
                movementSpeed = maxMovementSpeed / 2.5f;
            }
            else
            {
                movementSpeed = maxMovementSpeed;
            }

            if (god.platform == "Editor")
            {
                PointAtMouse();
                CheckMovement();

                this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }

            if (god.platform == "Ios")
            {
                pTouchMovement.CustomUpdate();
            }
        }
	}

    // Points the player at the mouse or touch
    public void PointAtMouse()
    {
        Vector3 worldMousePos = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(0);
        Vector3 dir = worldMousePos - this.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle -= 90;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Move the player around
    void CheckMovement()
    {
        Vector3 newPos = new Vector3(this.transform.position.x + (Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime), this.transform.position.y + (Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime), 0);
        this.transform.position = newPos;
    }

    // Stuns the player for the given seconds
    public void Stun(float time)
    {
        stunned = true;
        StartCoroutine(ResetStun(time));
    }

    IEnumerator ResetStun(float time)
    {
        yield return new WaitForSeconds(time);
        stunned = false;
    }
}
