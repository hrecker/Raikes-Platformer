using System;
using UnityEngine;

public class MovingPlatformMessenger: MonoBehaviour, IMessenger
{

	private MovingPlatformMovement movement;

	public void Start()
	{
		movement = GetComponent<MovingPlatformMovement> ();
	}

	public void Invoke(Message msg, object[] args)
	{
		switch (msg) {
		case Message.PLATFORM_LANDED_ON:
			movement.ObjectLandedOn (((Collider2D)args [0]).gameObject.transform.parent.gameObject);
			break;
		case Message.PLATFORM_JUMPED_OFF_OF:
			movement.ObjectJumpedOff (((Collider2D)args [0]).gameObject.transform.parent.gameObject);
			break;
		default:
			break;
		}
	}

}

