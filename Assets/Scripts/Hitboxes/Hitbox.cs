using UnityEngine;

public class Hitbox : CollisionBox
{
    public ColliderBoxType boxType; // what type of collisions does this hitbox detect
    public HitboxHarmType harmType;
    public bool affectsPlatforms = false;

    public void OnTriggerEnter2D(Collider2D other)
    {
        Hurtbox otherHurtbox = other.GetComponent<Hurtbox>();
        if (active && other.isTrigger && otherHurtbox != null && otherHurtbox.IsActive() && objectMessenger != null &&
            (boxType == ColliderBoxType.ANY || otherHurtbox.boxType == ColliderBoxType.ANY || boxType == otherHurtbox.boxType) &&
			CanHarm(otherHurtbox))
        {
            objectMessenger.Invoke(Message.HIT_OTHER, new object[] { boxType });
        }
    }

    public bool CanHarm(Hurtbox hurtbox)
    {
		return (harmType & hurtbox.harmType) != 0;
	}

}
