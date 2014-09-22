using UnityEngine;
using System.Collections;

public class PlayerMovement_SCR : MonoBehaviour 
{
    /* Publics */
    public float movementSpeed;

    public GameObject bulletPrefab;

    /* Privates */
    private God_SCR god;

	private Vector3 touchOffset = Vector3.zero;
	public int lookTouchId = 1000;
	public int moveTouchId = 1000;

	// Use this for initialization
	void Start () 
    {
	    // Find god
        god = GameObject.Find("God").GetComponent<God_SCR>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        PointAtInput(this.gameObject);
        CheckMovement();

        this.GetComponent<Rigidbody>().velocity = Vector3.zero;

		// Resetting when nothing touching
		if (Input.touchCount == 0)
		{
			touchOffset = Vector3.zero;
			lookTouchId = 1000;
			moveTouchId = 1000;
		}
	}

    // Points the player at the mouse or touch
    public void PointAtInput(GameObject objToPoint)
    {
        // Point at mouse
        if (god.platform == "Editor")
        {
            Vector3 worldMousePos = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(0);
            worldMousePos.z = 0;
            objToPoint.transform.LookAt(worldMousePos);
        }

        // Point at touch
        if (god.platform == "Ios" && Input.touchCount > 0)
        {
			// Init touch id
			if (lookTouchId == 1000)
			{
				lookTouchId = Input.GetTouch(0).fingerId;
			}
			Vector3 worldMousePos = Camera.main.ScreenPointToRay(new Vector3(Input.GetTouch(Input.touchCount - 1).position.x, Input.GetTouch(Input.touchCount - 1).position.y, 0)).GetPoint(0);
            worldMousePos.z = 0;
            objToPoint.transform.LookAt(worldMousePos);
        }
    }

    // Move the player around
    void CheckMovement()
    {
        // Move with keys
        if (god.platform == "Editor")
        {
            Vector3 newPos = new Vector3(this.transform.position.x + (Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime), this.transform.position.y + (Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime), 0);
            this.transform.position = newPos;
        }

        // Move with touch
        if (god.platform == "Ios" && Input.touchCount > 0)
		{
			// Get world position
			Vector3 worldMousePos = Camera.main.ScreenPointToRay(Input.GetTouch(Input.touchCount - 1).position).GetPoint(0);
			worldMousePos.z = 0;

			if (Vector3.Distance(this.transform.position, worldMousePos) < 2)
			{
				// Init touch offset
				if (touchOffset == Vector3.zero)
				{
					touchOffset = this.transform.position - worldMousePos;
				}

				// Move with touch
				this.transform.position = worldMousePos + touchOffset;
			}
        }
    }
}
