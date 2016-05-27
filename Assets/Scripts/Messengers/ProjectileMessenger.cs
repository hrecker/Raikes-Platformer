using System;
using UnityEngine;

public class ProjectileMessenger: MonoBehaviour, IMessenger
{

	private ProjectileMovement movement;

	public void Start() {
		this.movement = this.GetComponent<ProjectileMovement> ();
	}

	public void Invoke(String message, object[] args) {
		switch (message) {
		case "HitOther":
			this.movement.Expire ();
			break;
		case "HitByOther":
			this.movement.Expire ();
			break;
		default:
			break;
		}
	}

}