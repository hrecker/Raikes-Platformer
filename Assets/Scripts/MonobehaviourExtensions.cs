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

	public static T GetComponentInHierarchy<T>(this Behaviour behaviour) {
		T comp = behaviour.GetComponent<T> ();
		if (comp == null) {
			comp = behaviour.GetComponentInParent<T> ();
		}
		if (comp == null) {
			comp = behaviour.GetComponentInChildren<T> ();
		}
		return comp;
	}
}
