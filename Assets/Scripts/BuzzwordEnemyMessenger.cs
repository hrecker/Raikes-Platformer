using System;
using UnityEngine;

public class BuzzwordEnemyMessenger: MonoBehaviour, IMessenger
{
	public void Start() {

	}

	public void Invoke(String message, object[] args) {
		switch (message) {
		case "HitByOther":
			Destroy (gameObject);
			break;
		default:
			break;
		}
	}

}
