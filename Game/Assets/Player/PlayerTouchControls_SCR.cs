using UnityEngine;
using System.Collections;

public class PlayerTouchControls_SCR : MonoBehaviour 
{

	/* Publics */

	
	/* Privates */
	private int lookingTouchId = 1000;
	private int movingTouchId = 1000;
    private Vector3 touchOffset = Vector3.zero;

	private PlayerShooting_SCR pShooting;
    
	
	void Start () 
	{
		pShooting = this.GetComponent<PlayerShooting_SCR>();
	}
	
	void Update () 
	{
	}

    // Called once per frame is using these controls
    public void CustomUpdate()
    {
        foreach (Touch touch in Input.touches)
        {
            // Get world touch
            Vector3 worldMousePos = Camera.main.ScreenPointToRay(touch.position).GetPoint(0);
            worldMousePos.z = 0;

            SetTouchIds(touch, worldMousePos);
            CheckSetTouchIds(touch, worldMousePos);

			// Touch Shooting
			if (Input.touchCount == 3)
			{
				pShooting.Shoot(worldMousePos);
			}
        }

        // Reset touches
        if (Input.touchCount == 0)
        {
            touchOffset = Vector3.zero;
			lookingTouchId = 1000;
			movingTouchId = 1000;
        }
    }

    // Set touch ids
    void SetTouchIds(Touch touch, Vector3 worldTouchPos)
    {
        // No touches set yet
        if (lookingTouchId == 1000 && movingTouchId == 1000)
        {
            // Set touch
            if (Vector3.Distance(this.transform.position, worldTouchPos) < 2)
            {
                movingTouchId = touch.fingerId;
            }
            else
            {
                lookingTouchId = touch.fingerId;
            }
            return;
        }

        // New movement touch
        if (movingTouchId == 1000 && lookingTouchId != touch.fingerId)
        {
            movingTouchId = touch.fingerId;
			return;
        }

        // New looking touch
        if (lookingTouchId == 1000 && movingTouchId != touch.fingerId)
        {
            lookingTouchId = touch.fingerId;
			return;
        }
    }

    // Act on the set touch ids
    void CheckSetTouchIds(Touch touch, Vector3 worldTouchPos)
    {
        if (touch.fingerId == movingTouchId)
        {
            TouchMove(worldTouchPos);
        }

        if (touch.fingerId == lookingTouchId)
        {
            TouchLookAt(worldTouchPos);
        }
    }

    // Points the player at the mouse or touch
    void TouchLookAt(Vector3 worldTouch)
    {
        this.transform.LookAt(worldTouch);
    }

    // Move the player around
    void TouchMove(Vector3 worldTouch)
    {
        if (Vector3.Distance(this.transform.position, worldTouch) < 2)
        {
            // Init touch offset
            if (touchOffset == Vector3.zero)
            {
                touchOffset = this.transform.position - worldTouch;
            }

            // Move with touch
            this.transform.position = worldTouch + touchOffset;
        }
    }
}