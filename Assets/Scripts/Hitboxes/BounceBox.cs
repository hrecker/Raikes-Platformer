using UnityEngine;

public class BounceBox : CollisionBox
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Hurtbox otherHurtbox = other.GetComponent<Hurtbox>();
        if (active && other.isTrigger && otherHurtbox != null && objectMessenger != null &&
            (otherHurtbox.boxType == ColliderBoxType.ANY || otherHurtbox.boxType == ColliderBoxType.BOUNCE))
        {
            objectMessenger.Invoke(Message.BOUNCE, null);
        }
    }
}
