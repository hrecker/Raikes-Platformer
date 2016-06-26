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
            objectMessenger.Invoke(Message.HIT_BY_OTHER, null);
        }
    }

    public bool canReceiveHarm(Hitbox hitbox)
    {
		return (harmType & hitbox.harmType) != 0;
	}
}
