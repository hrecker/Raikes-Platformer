using UnityEngine;

public class HealthPickupBox : CollisionBox
{
    void OnTriggerStay2D(Collider2D other)
    {
        Hurtbox otherHurtbox = other.GetComponent<Hurtbox>();
        if (other.isTrigger && otherHurtbox != null && otherHurtbox.harmType == HitboxHarmType.PLAYER)
        {
            IMessenger otherMessenger = otherHurtbox.getMessenger();
            if(otherMessenger != null)
            {
                otherMessenger.Invoke(Message.HEALTH_PICKUP, new object[] { this });
            }
        }
    }

    public void DestroyPickup()
    {
        Destroy(transform.parent.gameObject);
    }
}
