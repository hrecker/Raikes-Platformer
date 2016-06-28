using System;
using UnityEngine;

public class HazardHitbox: MonoBehaviour
{
	public ColliderBoxType boxType; // what type of collisions does this hitbox detect
	public bool instantKill = true;
	private IMessenger objectMessenger;

	public void Start()
	{
		objectMessenger = GetComponent<IMessenger> ();
		if (objectMessenger == null) {
			objectMessenger = GetComponentInParent<IMessenger> ();
		}
		if (objectMessenger == null) {
			objectMessenger = GetComponentInChildren<IMessenger> ();
		}
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		Hurtbox otherHurtbox = other.GetComponent<Hurtbox>();
		if (other.isTrigger && otherHurtbox != null && otherHurtbox.IsActive() && objectMessenger != null)
		{
			objectMessenger.Invoke(Message.HIT_OTHER, new object[] { boxType });

			IMessenger otherObjectMessenger = other.GetComponent<IMessenger> ();
			if (otherObjectMessenger == null) {
				otherObjectMessenger = other.GetComponentInParent<IMessenger> ();
			}
			if (otherObjectMessenger == null) {
				otherObjectMessenger = other.GetComponentInChildren<IMessenger> ();
			}
			if (otherObjectMessenger != null) {
				//We choose an arbitrarily large value for damage
				//so that it is guaranteed to kill the object.
				int damage = (instantKill ? 1000 : 1);
				otherObjectMessenger.Invoke (Message.HIT_OTHER, new object[] { boxType, damage });
			}
		}
	}

}

