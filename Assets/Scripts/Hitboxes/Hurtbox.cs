using UnityEngine;

public class Hurtbox : CollisionBox
{
    public ColliderBoxType boxType; // what type of collisions does this hurtbox detect
    public HitboxHarmType harmType;

    void OnTriggerEnter2D(Collider2D other)
    {
        Hitbox otherHitbox = other.GetComponent<Hitbox>();
        if (active && other.isTrigger && otherHitbox != null && otherHitbox.IsActive() && objectMessenger != null &&
            (boxType == ColliderBoxType.ANY || otherHitbox.boxType == ColliderBoxType.ANY || boxType == otherHitbox.boxType) &&
			canReceiveHarm(otherHitbox))
        {
			if (otherHitbox.instantKill) {
				objectMessenger.Invoke (Message.INSTANT_KILL, new object[] { boxType });
			} else {
				objectMessenger.Invoke (Message.HIT_BY_OTHER, new object[] { boxType, otherHitbox.damage });
			}
        }
    }

    public bool canReceiveHarm(Hitbox hitbox)
    {
		return (harmType & hitbox.harmType) != 0;
	}
}
