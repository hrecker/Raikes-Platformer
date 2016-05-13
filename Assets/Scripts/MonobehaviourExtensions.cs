using System;
using UnityEngine;

public static class MonobehaviourExtensions
{
	public static IMessenger getMessenger(this MonoBehaviour behaviour) {
		IMessenger objectMessenger = behaviour.GetComponent<IMessenger>();
		if (objectMessenger == null)
		{
			objectMessenger = behaviour.GetComponentInParent<IMessenger>();
		}
		if (objectMessenger == null)
		{
			objectMessenger = behaviour.GetComponentInChildren<IMessenger>();
		}
		return objectMessenger;
	}
}
