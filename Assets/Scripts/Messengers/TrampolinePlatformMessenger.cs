using System;
using UnityEngine;

public class TrampolinePlatformMessenger: MonoBehaviour, IMessenger
{
	private TrampolinePlatformMovement movement;

	public void Start() {
		this.movement = this.GetComponent<TrampolinePlatformMovement>();
	}

	public void Invoke(Message msg, object[] args) {

		switch (msg) {
		case Message.PLATFORM_LANDED_ON:
			if (args [0] is Behaviour) {
				Behaviour behaviour = ((Behaviour)args [0]);
				this.GetComponentInHierarchy<IMessenger> ();
				IMessenger argMessenger = behaviour.GetComponentInHierarchy<IMessenger> ();
				argMessenger.Invoke (Message.LANDED_ON_TRAMPOLINE_PLATFORM, new object[] { this.movement.bounciness, this.movement.framesToJumpOnTrampoline });
			}
			break;
		default:
			break;
		}
	}

}

