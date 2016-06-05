using UnityEngine;

public class Hitbox : CollisionBox
{
    public ColliderBoxType boxType; // what type of collisions does this hitbox detect
    public HitboxHarmType harmType;

    void OnTriggerEnter2D(Collider2D other)
    {
        Hurtbox otherHurtbox = other.GetComponent<Hurtbox>();
        if (other.isTrigger && otherHurtbox != null && objectMessenger != null &&
            (boxType == ColliderBoxType.ANY || otherHurtbox.boxType == ColliderBoxType.ANY || boxType == otherHurtbox.boxType) &&
			CanHarm(otherHurtbox))
        {
            objectMessenger.Invoke(Message.HIT_OTHER, null);
        }
    }
		
	public bool CanHarm(Hurtbox hurtbox)
    {
		return (harmType & hurtbox.harmType) != 0;
	}

}
