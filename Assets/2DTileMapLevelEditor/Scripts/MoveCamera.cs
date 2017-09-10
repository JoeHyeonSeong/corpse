// Credit to damien_oconnell from http://forum.unity3d.com/threads/39513-Click-drag-camera-movement
// for using the mouse displacement for calculating the amount of camera movement and panning code.


// Slimmed down to only feature panning (zooming done in alternative way)

using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour 
{
	//
	// VARIABLES
	//

	public float panSpeed = 4.0f;		// Speed of the camera when being panned

	private Vector3 mouseOrigin;	// Position of cursor when mouse dragging starts
	private bool isPanning;		// Is the camera being panned?

	//
	// UPDATE
	//

	void Update () 
	{
        const int moveSpeed = 20;
        Vector3 moveDelta=Vector3.zero;
        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.UpArrow))
            {
            moveDelta += Vector3.up;
            isPanning = true;
        }
        if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
        {
            moveDelta += Vector3.left;
            isPanning = true;
        }
        if (Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.DownArrow))
        {
            moveDelta += Vector3.down;
            isPanning = true;
        }
        if (Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))
        {
            moveDelta += Vector3.right;
            isPanning = true;
        }

		// Move the camera on it's XY plane
		if (isPanning)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(moveDelta*moveSpeed);

			Vector3 move = new Vector3(pos.x * panSpeed, pos.y * panSpeed, 0);
			transform.Translate(move, Space.Self);
		}
	}
}