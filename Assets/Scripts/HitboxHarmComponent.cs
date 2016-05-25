using System;
using UnityEngine;

public class HitboxHarmComponent: MonoBehaviour
{
	public HitboxHarmType performHarmType;
	public HitboxHarmType receiveHarmType;

	public bool canHarm(HitboxHarmComponent comp) {
		return (this.performHarmType & comp.receiveHarmType) != 0;
	}

	public bool canReceiveHarm(HitboxHarmComponent comp) {
		return (this.receiveHarmType & comp.performHarmType) != 0;
	}

}

public static class HitboxHarmMonoBehaviourExtension {

	public static HitboxHarmComponent getHitboxHarmComponent(this Behaviour a) {
		HitboxHarmComponent aHarm = a.GetComponent<HitboxHarmComponent>();
		if (aHarm == null) {
			aHarm = a.GetComponentInParent<HitboxHarmComponent> ();
		}
		if (aHarm == null) {
			aHarm = a.GetComponentInChildren<HitboxHarmComponent> ();
		}
		return aHarm;
	}

	public static bool canHarm(this Behaviour a, Behaviour b) {
		HitboxHarmComponent aHarm = a.getHitboxHarmComponent ();
		HitboxHarmComponent bHarm = b.getHitboxHarmComponent ();
		//For now, we return false, in case a Hitbox/Hurtbox object
		//collides with a collider not attached to a player/enemy/projectile etc.
		if (aHarm == null) {
//			throw new NullReferenceException ($"Object ({a}) does not have a HitboxHarmComponent!");
			return false;
		} else if (bHarm == null) {
//			throw new NullReferenceException ($"Object ({b}) does not have a HitboxHarmComponent!");
			return false;
		}
		return aHarm.canHarm (bHarm);
	}

	public static bool canReceiveHarm(this Behaviour a, Behaviour b) {
		HitboxHarmComponent aHarm = a.getHitboxHarmComponent ();
		HitboxHarmComponent bHarm = b.getHitboxHarmComponent ();
		if (aHarm == null || bHarm == null) {
			return false;
		}
		return aHarm.canReceiveHarm (bHarm);
	}

}