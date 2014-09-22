using UnityEngine;
using System.Collections;

public class PlayerTouchMovement_SCR : MonoBehaviour 
{

	/* Publics */

	
	/* Privates */
    private int lookingTouchId = 1000;
    private int movingTouchId = 1000;
    private Vector3 touchOffset = Vector3.zero;
    
	
	void Start () 
	{
	
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

            // No touches set yet
            if (lookingTouchId == 1000 && movingTouchId == 1000)
            {
                // Set touch
                if (Vector3.Distance(this.transform.position, worldMousePos) < 2)
                {
                    movingTouchId = touch.fingerId;
                }
                else
                {
                    lookingTouchId = touch.fingerId;
                }
                return;
            }

            // Check set touches

            if (touch.fingerId == movingTouchId)
            {
                TouchMove(worldMousePos);
            }

            if (touch.fingerId == lookingTouchId)
            {
                TouchLookAt(worldMousePos);
            }
        }

        // Reset touches
        if (Input.touchCount == 0)
        {
            touchOffset = Vector3.zero;
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