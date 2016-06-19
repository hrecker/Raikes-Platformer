using System;
using UnityEngine;

public class PowerupBlockMessenger: MonoBehaviour, IMessenger
{
	private ObjectSpawner objectSpawner;

	void Start()
	{
		objectSpawner = GetComponent<ObjectSpawner> ();
	}

	public void Invoke(Message msg, object[] args) {
		switch (msg) {
		case Message.HIT_BY_OTHER:
			objectSpawner.SpawnObject ();
			//Delay the destruction so the RigidBody2D object
			//has the opportunity to cause the player to hit the block.
			Destroy (this.gameObject, 0.0333f);
			break;
		}
	}
}

