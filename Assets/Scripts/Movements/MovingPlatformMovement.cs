using System;
using UnityEngine;

public class MovingPlatformMovement: MonoBehaviour
{
	public Vector2[] waypoints;
	public float speed = 1.0f;
	public bool reversePath = true;
	private int waypointIndex = 0;
	private Vector2 CurrentWaypoint {
		get { return waypoints [waypointIndex]; }
	}
	private Vector2 NextWaypoint {
		get {
			return waypoints [IndexOfNextWaypoint ()];
		}
	}
	private bool goingBackwards = false;
	private System.Collections.Generic.IList<GameObject> objectsStandingOn;

	public void Start()
	{
		objectsStandingOn = new System.Collections.Generic.List<GameObject> ();
	}

	public void Update()
	{
		Vector2 offset = new Vector2 ();
		Vector2 delta = new Vector2 ();
		if (HasReachedWaypoint ()) {
			//To jump to a waypoint, we need to set the offset vector
			//correctly so it moves the platform AND the objects
			//standing on it correctly.
			offset = NextWaypoint - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
			delta = offset;

			if (goingBackwards) {
				waypointIndex--;
			} else {
				waypointIndex++;
			}
			if (reversePath) {
				if (waypointIndex == waypoints.Length - 1) {
					goingBackwards = true;
				} else if (waypointIndex == 0) {
					goingBackwards = false;
				}
			} else {
				waypointIndex = waypointIndex % waypoints.Length;
			}

		} else {
			offset = OffsetVector();
			delta = offset * speed * Time.deltaTime;
		}
		Vector3 delta3D = new Vector3 (delta.x, delta.y);
		gameObject.transform.position += delta3D;
		foreach (GameObject standingOn in objectsStandingOn) {
			standingOn.transform.position += delta3D;
		}
	}

	private bool HasReachedWaypoint()
	{
		//We multiply by Time.deltaTime to make sure we only jump to the waypoint if
		//it will be reached in the distance crossed in a single frame, and we multiply
		//by a constant (1.5) to make sure floating point errors don't happen.
		Vector2 nextWaypoint = NextWaypoint;
		float distance = Vector2.Distance (gameObject.transform.position, nextWaypoint);
		float epsilon = speed * Time.deltaTime * 2.0f;
		return distance < epsilon;
	}

	private int IndexOfNextWaypoint()
	{
		return 0;
		if (reversePath) {
			if (goingBackwards) {
				return waypointIndex - 1;
			} else {
				return waypointIndex + 1;
			}
		} else {
			return (waypointIndex + 1) % waypoints.Length;
		}
	}

	private Vector2 OffsetVector()
	{
		Vector2 offset = NextWaypoint - CurrentWaypoint;
		offset.Normalize ();
		return offset;
	}

	public void ObjectLandedOn(GameObject obj)
	{
		objectsStandingOn.Add (obj);
	}

	public void ObjectJumpedOff(GameObject obj)
	{
		objectsStandingOn.Remove (obj);
	}

}

